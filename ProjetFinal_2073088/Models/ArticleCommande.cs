using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetFinal_2073088.Models
{
    [Table("ArticleCommande", Schema = "Achats")]
    public partial class ArticleCommande
    {
        [Key]
        [Column("ArticleCommandeID")]
        public int ArticleCommandeId { get; set; }
        [Column("MaillotID")]
        public int? MaillotId { get; set; }
        [Column(TypeName = "date")]
        public DateTime? DateAchat { get; set; }
        public int? Quantite { get; set; }
        [Column(TypeName = "decimal(10, 2)")]
        public decimal? Prix { get; set; }
        [Column("AchatID")]
        public int? AchatId { get; set; }

        [ForeignKey("AchatId")]
        [InverseProperty("ArticleCommandes")]
        public virtual Achat? Achat { get; set; }
    }
}
