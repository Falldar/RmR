using RmR.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RmR.ViewModels
{
   public class ResumeExpertViewModel
    {
        public IEnumerable<Resume> Resumes { get; set; }
        public IEnumerable<Expert> Experts { get; set; }
    }
}
