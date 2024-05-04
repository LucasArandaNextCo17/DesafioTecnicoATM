using System;
using System.Collections.Generic;

namespace ATM
{
    public partial class Tarjeta
    {
        public Tarjeta()
        {
            Operaciones = new HashSet<Operacione>();
        }

        public int Id { get; set; }
        public string Numero { get; set; } = null!;
        public string Pin { get; set; } = null!;
        public bool Bloqueada { get; set; }
        public decimal Balance { get; set; }
        //public int IntentosFallidos { get; set; } // Nuevo campo para intentos fallidos

        public virtual ICollection<Operacione> Operaciones { get; set; }
    }
}