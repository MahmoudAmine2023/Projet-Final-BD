using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetFinal_2073088.Models
{
    [Table("Maillot", Schema = "Maillots")]
    public partial class Maillot
    {
        public Maillot()
        {
            Concurrents = new HashSet<Concurrent>();
        }

        [Key]
        [Column("MaillotID")]
        public int MaillotId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string NomEquipe { get; set; } = null!;
        [StringLength(10)]
        [Unicode(false)]
        public string? Taille { get; set; }
        [StringLength(20)]
        [Unicode(false)]
        public string? Couleur { get; set; }
        public int? Annee { get; set; }
        [StringLength(10)]
        public string? EstPremium { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal Prix { get; set; }
        [Column("PromotionID")]
        public int? PromotionId { get; set; }

        [ForeignKey("PromotionId")]
        [InverseProperty("Maillots")]
        public virtual Promotion? Promotion { get; set; }
        [InverseProperty("Maillot")]
        public virtual ICollection<Concurrent> Concurrents { get; set; }
    }
}
