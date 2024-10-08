using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Cafe.Models
{
    public class About
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
    }
}
