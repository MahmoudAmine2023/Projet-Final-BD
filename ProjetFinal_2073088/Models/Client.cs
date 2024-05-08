using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ProjetFinal_2073088.Models
{
    [Table("Client", Schema = "Clients")]
    public partial class Client
    {
        public Client()
        {
            Achats = new HashSet<Achat>();
        }

        [Key]
        [Column("ClientID")]
        public int ClientId { get; set; }
        [StringLength(50)]
        [Unicode(false)]
        public string Nom { get; set; } = null!;
        [StringLength(100)]
        [Unicode(false)]
        public string? Adresse { get; set; }
        [StringLength(15)]
        [Unicode(false)]
        public string? Telephone { get; set; }
        [DisplayName("Courriel")]
        public byte[]? CourrielEncrypt { get; set; }

        [InverseProperty("Client")]
        public virtual ICollection<Achat> Achats { get; set; }
    }
}
