using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectGarantia.Models
{
    [Table("ContadorLotes")]
    public class ContadorLotes
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("CodAgencia")]
        public string CodAgencia { get; set; }

        [Column("Contador")]
        public int Contador { get; set; }
    }
}
