using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroPrestamos.Entities
{
    public class Prestamos {
        [Key]
        public int PrestamoId { get; set; }
        public DateTime Fecha { get; set; }
        public string Concepto { get; set; }
        public float Monto { get; set; }
        public float Balance { get; set; }
        public int PersonaId { get; set; }
        public decimal Mora { get; set; }

        [ForeignKey("PrestamoId")]
        public virtual List<MorasDetalle> Detalle { get; set; }

        public Prestamos()
        {
            Mora = 0M;
            Detalle = new List<MorasDetalle>();
        }
    }
}