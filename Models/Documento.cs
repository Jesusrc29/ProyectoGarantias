using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectGarantia.Models
{
    [Table("Documento")]
    public class Documento
    {
        [Key]
        [Column("Id")]
        public int DocumentoId { get; set; }

        [Column("DetalleLoteModeloId")]
        public int DetalleLoteModeloId { get; set; }

        [ForeignKey("Id")]
        public virtual DetalleLoteModelo DetalleLoteModelo { get; set; }

        [Column("NombreDocumento")]
        public string NombreDocumento { get; set; }

        [Column("NombreOriginal")]
        public string NombreOriginal { get; set; }

        [Column("TipoDocumento")]
        public string TipoDocumento { get; set; }
    }
}
