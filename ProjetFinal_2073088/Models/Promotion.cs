using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetFinal_2073088.Models
{
    [Table("Promotion", Schema = "Promotions")]
    public partial class Promotion
    {
        public Promotion()
        {
            Achats = new HashSet<Achat>();
            Maillots = new HashSet<Maillot>();
        }

        [Key]
        [Column("PromotionID")]
        public int PromotionId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Nom { get; set; } = null!;
        [Column(TypeName = "decimal(5, 2)")]
        public decimal PourcentageReduction { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateDebut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateFin { get; set; }

        [InverseProperty("Promotion")]
        public virtual ICollection<Achat> Achats { get; set; }
        [InverseProperty("Promotion")]
        public virtual ICollection<Maillot> Maillots { get; set; }
    }
}
