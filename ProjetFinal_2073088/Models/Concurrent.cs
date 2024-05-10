using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetFinal_2073088.Models
{
    [Table("Concurrent", Schema = "Concurrents")]
    public partial class Concurrent
    {
        [Key]
        [Column("ConcurentID")]
        public int ConcurentId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Nom { get; set; } = null!;
        [StringLength(100)]
        [Unicode(false)]
        public string? Adresse { get; set; }
        [StringLength(15)]
        [Unicode(false)]
        public string? Telephone { get; set; }
        [Column("MaillotID")]
        public int? MaillotId { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? PrixVendu { get; set; }

        [ForeignKey("MaillotId")]
        [InverseProperty("Concurrents")]
        public virtual Maillot? Maillot { get; set; }
    }
}
