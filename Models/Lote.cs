using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectGarantia.Models
{
    [Table("Lotes")]
    public class Lote
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("NumeroCorrelativo")]
        public string NumeroCorrelativo { get; set; }

        [Column("Estado")]
        public EstadoLote Estado { get; set; }

        [Column("FechaCreacion")]
        public DateOnly FechaCreacion { get; set; }

        [Column("NombreCreador")]
        public string NombreCreador { get; set; }

        [Column("FechaDesde", TypeName = "Date")]
        public DateTime FechaDesde { get; set; }
        [Column("FechaHasta", TypeName = "Date")]
        public DateTime FechaHasta { get; set; }
        public List<DetalleLoteModelo>? DetallePrestamo { get; set; }
    }

    public enum EstadoLote
    {
        Enviado,
        EnCurso,
        Recibido,
        Aprobado,
        Rechazado,
        EnFirma
    }
}
