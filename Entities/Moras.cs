using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace RegistroPrestamos.Entities
{
    public class Moras
    {
        [Key]
        public int MoraId { get; set; }
        public DateTime Fecha { get; set; }
        public decimal Total { get; set; }

        [ForeignKey("MoraId")]
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
        public int MoraDetalleId { get; set; }
        public int MoraId { get; set; }
        public int PrestamoId { get; set; }
        public decimal Valor { get; set; }

        public MorasDetalle(int moraId, int prestamoId, decimal valor)
        {
            MoraId = moraId;
            PrestamoId = prestamoId;
            Valor = valor;
        }
    }
}