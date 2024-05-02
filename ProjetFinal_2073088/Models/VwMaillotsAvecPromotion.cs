using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetFinal_2073088.Models
{
    [Keyless]
    public partial class VwMaillotsAvecPromotion
    {
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
        [StringLength(50)]
        [Unicode(false)]
        public string? NomPromotion { get; set; }
        [Column(TypeName = "decimal(5, 2)")]
        public decimal? PourcentageReduction { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateDebut { get; set; }
        [Column(TypeName = "datetime")]
        public DateTime? DateFin { get; set; }
        [Column("Prix concurrents", TypeName = "decimal(10, 2)")]
        public decimal? PrixConcurrents { get; set; }
    }
}
