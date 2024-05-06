using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetFinal_2073088.Models
{
    [Keyless]
    [Table("Courriel", Schema = "Clients")]
    public partial class Courriel
    {
        [Column("Courriel")]
        [StringLength(100)]
        public string? Courriel1 { get; set; }
    }
}
