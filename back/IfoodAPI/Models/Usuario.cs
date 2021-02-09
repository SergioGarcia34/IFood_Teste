using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace IfoodAPI.Models
{
    public class Usuario
    {
        [Required]
        string UserName { get; set; }
        [Required]
        string Password { get; set; }

    }
}
