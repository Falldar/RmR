﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RmR.Models
{
    public class Expert : User
    {
        public virtual ICollection<Resume> Resumes { get; set; }
    }
}
