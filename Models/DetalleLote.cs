using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectGarantia.Models
{
    public class DetalleLote
    {

        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("LoteId")]
        public int LoteId { get; set; }
        [ForeignKey("LoteId")]
        public Lote Lote { get; set; }

        [Column("FechaOtorgada")]
        public DateOnly FechaOtorgada { get; set; }

        [Column("ClienteId")]
        public int ClienteId { get; set; }
        [ForeignKey("ClienteId")]
        public Cliente Cliente { get; set; }

        [Column("AgenciaId")]
        public int AgenciaId { get; set; }
        [ForeignKey("AgenciaId")]
        public Agencia Agencia { get; set; }

        [Column("NombreAsesor")]
        public string NombreAsesor { get; set; }

        [Column("FechaEnvio")]
        public DateOnly FechaEnvio { get; set; }

        [Column("CantidadGarantias")]
        public int CantidadGarantias { get; set; }


    }

}
