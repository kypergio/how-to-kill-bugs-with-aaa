using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CloudStorage.Core.Model.DAL
{
    public class PokemonUpsertDAL
    {
        [Required]
        public IFormFile? Photo { get; set; }
        [Required]
        public string? Name { get; set; }
    }

    public class PokemonQueryDAL
    {
        public int Id { get; set; }
        public string? Photo { get; set; }
        public string? Name { get; set; }
    }
}
