using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RegistroPrestamos.Entities
{
    public class Persona {
        [Key]
        public int PersonaID { get; set; }
        public string Nombres { get; set; }
        public float Balance { get; set; }
    }
}