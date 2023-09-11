﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProyectGarantia.Models
{
    [Table("Clientes")]
    public class Cliente
    {
        [Key]
        [Column("Id")]
        public int Id { get; set; }

        [Column("Nombre")]
        public string Nombre { get; set; }

        public List<DetalleLote> DetallesLote { get; set; }
    }

}
