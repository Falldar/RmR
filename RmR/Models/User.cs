using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RmR.Models
{
    public class User
    {
        public int ID { get; set; }
        [Required]
        [StringLength(50)]
        [Display(Name = "Full Name")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(60)]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [StringLength(80)]
        public string Email { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstName;
            }
        }
        public virtual ICollection<User> Users { get; set; }
        
    }
}
