
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroPrestamos.Entities
{
    public class Prestamo {
        [Key]
        public int PrestamoID { get; set; }
        public DateTime Fecha { get; set; }
        public string Concepto { get; set; }
        public float Monto { get; set; }
        public float Balance { get; set; }
        public int PersonaID { get; set; }

    }
}