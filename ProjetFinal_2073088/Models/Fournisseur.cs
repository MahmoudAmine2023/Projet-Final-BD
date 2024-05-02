using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetFinal_2073088.Models
{
    [Table("Fournisseur", Schema = "Fournisseurs")]
    public partial class Fournisseur
    {
        [Key]
        [Column("FournisseurID")]
        public int FournisseurId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Nom { get; set; } = null!;
        [StringLength(100)]
        [Unicode(false)]
        public string? Adresse { get; set; }
        [StringLength(15)]
        [Unicode(false)]
        public string? Telephone { get; set; }
    }
}
