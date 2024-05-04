using System;
using System.Collections.Generic;

namespace ATM
{
    public partial class Operacione
    {
        public int Id { get; set; }
        public int Idtarjeta { get; set; }
        public DateTime FechaHora { get; set; }
        public string CodigoOperacion { get; set; } = null!;
        public decimal? MontoRetirado { get; set; }

        public virtual Tarjeta IdtarjetaNavigation { get; set; } = null!;
    }
}
