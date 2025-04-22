using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AG.Data.Entities
{
    public class BaseEntity
    {
        [Key]
        [Required]
        public Guid Id { get; set; }
    }
}
