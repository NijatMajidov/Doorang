using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DoorangMVC.Core.Models
{
    public class Explore : BaseEntity
    {
        [Required]
        [StringLength(50)]
        public string Title { get; set; } = null!;
        [Required]
        [StringLength(50)]
        public string Subtitle { get; set; } = null!;
        [Required]
        [StringLength(100)]
        public string Description { get; set; } = null!;
        public string? ImageUrl { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
