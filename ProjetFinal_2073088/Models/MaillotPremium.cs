using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetFinal_2073088.Models
{
    [Keyless]
    [Table("MaillotPremium", Schema = "Maillots")]
    public partial class MaillotPremium
    {
        [Column("MaillotPremiumID")]
        public int MaillotPremiumId { get; set; }
        [StringLength(50)]
        public string? Nom { get; set; }
        public int? Numero { get; set; }
        [StringLength(50)]
        public string? Flocage { get; set; }
        [Column("MaillotID")]
        public int? MaillotId { get; set; }

        [ForeignKey("MaillotId")]
        public virtual Maillot? Maillot { get; set; }
    }
}
