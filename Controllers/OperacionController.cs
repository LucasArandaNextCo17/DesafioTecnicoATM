using ATM;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using static System.Collections.Specialized.BitVector32;

public class OperacionController : Controller
{
    private readonly CajeroAutomaticoDBContext _context;

    public OperacionController(CajeroAutomaticoDBContext context)
    {
        _context = context;
    }

    public IActionResult Balance(int? tarjetaId)
    {
        if (tarjetaId == null)
        {
            // Manejo si no se proporciona tarjetaId
            ViewBag.ErrorMessage = "Se requiere un ID de tarjeta.";
            return View("Error");
        }

        // Buscar la tarjeta en la base de datos
        var tarjeta = _context.Tarjetas.FirstOrDefault(t => t.Id == tarjetaId);

        if (tarjeta != null)
        {
            // Si la tarjeta es encontrada, pasar los datos a la vista
            return View(tarjeta);
        }
        else
        {
            // Si la tarjeta no es encontrada, mostrar mensaje de error
            ViewBag.ErrorMessage = "Tarjeta no encontrada.";
            return View("Error");
        }
    }


    public IActionResult Retiro(int? tarjetaId)
    {
        if (tarjetaId == null)
        {
            // Manejar el caso en el que no se proporciona tarjetaId
            ViewBag.ErrorMessage = "Se requiere un ID de tarjeta.";
            return View("Error");
        }

        // Buscar la tarjeta en la base de datos
        var tarjeta = _context.Tarjetas.FirstOrDefault(t => t.Id == tarjetaId);

        if (tarjeta != null)
        {
            // Si la tarjeta es encontrada, pasar los datos a la vista de retiro
            ViewBag.TarjetaId = tarjetaId;
            return View();
        }
        else
        {
            // Si la tarjeta no es encontrada, mostrar mensaje de error
            ViewBag.ErrorMessage = "Tarjeta no encontrada.";
            return View("Error");
        }
    }

    [HttpPost]
    public IActionResult Retiro(int tarjetaId, decimal cantidad)
    {
        // Verificar si la cantidad de retiro es válida
        if (cantidad <= 0)
        {
            ViewBag.ErrorMessage = "La cantidad de retiro debe ser mayor que cero.";
            return View("Error");
        }

        // Buscar la tarjeta en la base de datos (ya se ha buscado en el método GET)
        var tarjeta = _context.Tarjetas.FirstOrDefault(t => t.Id == tarjetaId);

        if (tarjeta != null)
        {
            // Verificar si el saldo es suficiente para el retiro
            if (cantidad <= tarjeta.Balance)
            {
                using (var transaction = _context.Database.BeginTransaction())
                {
                    try
                    {
                        // Actualizar el saldo de la tarjeta
                        tarjeta.Balance -= cantidad;
                        _context.SaveChanges();

                        // Agregar la operación de retiro a la base de datos
                        _context.Operaciones.Add(new Operacione { Idtarjeta = tarjetaId, FechaHora = DateTime.Now, CodigoOperacion = "Retiro", MontoRetirado = cantidad });
                        _context.SaveChanges();

                        // Confirmar la transacción
                        transaction.Commit();

                        // Redirigir al reporte de operación
                        return RedirectToAction("ReporteOperacion", new { tarjetaId, cantidad });
                    }
                    catch (Exception)
                    {
                        // Si ocurre un error, deshacer la transacción
                        transaction.Rollback();
                        ViewBag.ErrorMessage = "Se produjo un error al procesar la transacción.";
                        return View("Error");
                    }
                }
            }
            else
            {
                // Mostrar mensaje de error por saldo insuficiente
                ViewBag.ErrorMessage = "Saldo insuficiente para realizar el retiro.";
                return View("Error");
            }
        }
        else
        {
            // Mostrar mensaje de error si la tarjeta no es encontrada
            ViewBag.ErrorMessage = "Tarjeta no encontrada.";
            return View("Error");
        }
    }



    public IActionResult ReporteOperacion(int tarjetaId, decimal cantidad)
    {
        // Buscar la tarjeta en la base de datos
        var tarjeta = _context.Tarjetas.FirstOrDefault(t => t.Id == tarjetaId);

        if (tarjeta != null)
        {
            // Pasar los datos a la vista para mostrar el reporte de operación
            ViewBag.Cantidad = cantidad;
            ViewBag.Saldo = tarjeta.Balance;
            return View();
        }
        else
        {
            // Si la tarjeta no es encontrada, mostrar mensaje de error
            ViewBag.ErrorMessage = "Tarjeta no encontrada.";
            return View("Error");
        }
    }
}

