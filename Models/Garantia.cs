using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ProyectGarantia.Models
{
    [Table("Garantias")]
    public class Garantia
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [ForeignKey("DetalleLoteModeloId")]
        public int PrestamoId { get; set; }
        public DetalleLoteModelo DetalleLoteModelo { get; set; }

        [Column("CorrGarantia")]
        public string CorrGarantia { get; set; }

        [Column("NombreAval")]
        public string NombreAval { get; set; }

        [Column("MontoGarantia")]
        public double MontoGarantia { get; set; }

        [Column("DescripAval")]
        public string DescripAval { get; set; }

		[Column("Estado")]
		public EstadoGarantia Estado { get; set; }

	}


    public enum EstadoGarantia
    {
        Activa,
        Inactiva,
        EnDemanda,
        SalidaPrestamoDocumentacion,
        SalidaCambio,
        IngresoCambio,
        BajaCancelacion
    }
}
