using RmR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RmR.ViewModels
{
    class ResumeClientViewModel
    {
        public IEnumerable<Resume> Resumes { get; set; }
        public IEnumerable<Client> Clients { get; set; }
    }
}
