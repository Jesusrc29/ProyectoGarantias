using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectGarantia.Models
{
    [Table("DetalleLoteModelo")]
    public class DetalleLoteModelo
    {
            [Key]
            public int Id { get; set; }
            [ForeignKey("Lote")]
            public int LoteId { get; set; }
            public Lote Lote { get; set; }
            [Column("FechaOtorgada")]
        public DateOnly FechaOtorgada { get; set; }
        [Column("NombreCliente")]
            public string NombreCliente { get; set; }
            [Column("CentroCosto")]
            public string CentroCosto { get; set; }
            [Column("CantidadGarantias")]
            public int CantidadGarantias { get; set; }
            [Column("GVDocOriginal")]
            public bool GVDocOriginal { get; set; }
            [Column("GVCopiaRevision")]
            public bool GVCopiaRevision { get; set; }
            [Column("GVTraspaso")]
            public bool GVTraspaso { get; set; }
            [Column("GHEscritura")]
            public bool GHEscritura { get; set; }
            [Column("GHAvaluo")]
            public bool GHAvaluo { get; set; }
            [Column("GHRevisionIP")]
            public bool GHRevisionIP { get; set; }
            [Column("ValorGarantia")]
            public decimal ValorGarantia { get; set; }
            [Column("Contrato")]
            public bool Contrato { get; set; }
        [Column("NumContrato")]
        public string? NumContrato { get; set; }
        [Column("Pagare")]
            public bool Pagare { get; set; }
        [Column("NumPagare")]
        public string? NumPagare { get; set; }
        [Column("NumeroCorrelativo")]
            public string NumeroCorrelativo { get; set; }
            [Column("NombreAsesor")]
            public string NombreAsesor { get; set; }
            [Column("FechaEnvio")]
        public DateOnly FechaEnvio { get; set; }
        [Column("NombreCreador")]
            public string NombreCreador { get; set; }
        [Column("NumPrestamo")]
        public string? NumPrestamo { get; set; }
        [Column("Estado")]
        public EstadoPrestamo Estado { get; set; }


        public enum EstadoPrestamo
        {
            Creado,
            Aprobado,
            Rechazado
        }


        public List<Garantia> Garantias { get; set; }
    }
}
