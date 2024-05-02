using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetFinal_2073088.Models
{
    [Table("Achat", Schema = "Achats")]
    public partial class Achat
    {
        public Achat()
        {
            ArticleCommandes = new HashSet<ArticleCommande>();
        }

        [Key]
        [Column("AchatID")]
        public int AchatId { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateAchat { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? PrixTotal { get; set; }
        [Column("ClientID")]
        public int? ClientId { get; set; }
        [Column("PromotionID")]
        public int? PromotionId { get; set; }

        [ForeignKey("ClientId")]
        [InverseProperty("Achats")]
        public virtual Client? Client { get; set; }
        [ForeignKey("PromotionId")]
        [InverseProperty("Achats")]
        public virtual Promotion? Promotion { get; set; }
        [InverseProperty("Achat")]
        public virtual ICollection<ArticleCommande> ArticleCommandes { get; set; }
    }
}
