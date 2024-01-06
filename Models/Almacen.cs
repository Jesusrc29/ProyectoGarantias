using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectGarantia.Models
{
    [Table("Almacenes")]
    public class Almacen
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Codigo")]
        public string Codigo { get; set; }

        [Column("Nombre")]
        public string Nombre { get; set; }

    }

}
