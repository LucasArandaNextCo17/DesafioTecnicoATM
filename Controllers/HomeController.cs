
using ATM;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;

public class HomeController : Controller
{
    private readonly CajeroAutomaticoDBContext _context;

    public HomeController(CajeroAutomaticoDBContext context)
    {
        _context = context;
    }

    public IActionResult Index()
    {
        return View();
    }
    public IActionResult Inicio()
    {
        // Redirigir a la vista de inicio
        return RedirectToAction("Index", "Home");
    }

    [HttpPost]
    public IActionResult ValidarTarjeta(string numeroTarjeta)
    {
        // Buscar la tarjeta en la base de datos
        var tarjeta = _context.Tarjetas.FirstOrDefault(t => t.Numero == numeroTarjeta);

        if (tarjeta != null && !tarjeta.Bloqueada)
        {
            // Si la tarjeta es encontrada y no está bloqueada, redirigir al ingreso de PIN
            return RedirectToAction("IngresoPin", new { tarjetaId = tarjeta.Id });
        }
        else
        {
            // Si la tarjeta no es encontrada o está bloqueada, mostrar mensaje de error
            ViewBag.ErrorMessage = "Tarjeta no encontrada o bloqueada.";
            return View("Error");
        }
    }

    public IActionResult IngresoPin(int tarjetaId)
    {
        // Pasar el ID de la tarjeta a la vista para realizar la validación del PIN
        ViewBag.TarjetaId = tarjetaId;
        return View();
    }

    [HttpPost]
    public IActionResult ValidarPin(int tarjetaId, string pin)
    {
        var tarjeta2 = ViewBag.TarjetaId;
        // Buscar la tarjeta en la base de datos
        var tarjeta = _context.Tarjetas.FirstOrDefault(t => t.Id == tarjetaId && t.Pin == pin);

        if (tarjeta != null)
        {
            // Si el PIN es correcto, redirigir a la página de operaciones
            return RedirectToAction("Operaciones", new { TarjetaId = tarjetaId });
        }
        else
        {
            // Si el PIN es incorrecto, mostrar mensaje de error
            ViewBag.ErrorMessage = "PIN incorrecto. Intente de nuevo.";
            return View("Error");
        }
    }

    public IActionResult Operaciones( int TarjetaId)
    {
        ViewBag.TarjetaId = TarjetaId;
        return View();
    }
}
