using ApplicationCore.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace RecipeAPI.Models
{
    public class RecipeModel
    {
        public int RecipeId { get; set; }

        [Required]
        [StringLength(50)]
        public string Name { get; set; }

        public string Instruction { get; set; }

        [Required]
        [StringLength(50)]
        public string Category { get; set; }



    }
}
