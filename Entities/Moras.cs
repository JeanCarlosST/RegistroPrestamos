using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroPrestamos.Entities
{
    public class Moras
    {
        [Key]
        public int MoraID { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }

        [ForeignKey("MoraID")]
        public virtual List<MorasDetalle> Detalle { get; set; }

        public Moras()
        {
            Fecha = DateTime.Now;
            Detalle = new List<MorasDetalle>();
        }
    }


    public class MorasDetalle
    {
        [Key]
        public int MoraDetalleID { get; set; }
        public int MoraID { get; set; }
        public int PrestamoID { get; set; }
        public decimal Valor { get; set; }

        public MorasDetalle(int moraID, int prestamoID, decimal valor)
        {
            MoraID = moraID;
            PrestamoID = prestamoID;
            Valor = valor;
        }
    }
}