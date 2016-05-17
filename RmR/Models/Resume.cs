﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RmR.Models
{
    public enum Status
    {
        Submit, Review, Complete
    }
    public class Resume
    {
        public int ResumeID { get; set; }
        [Required]
        [Display(Name="Resume Name")]
        public int ResumeName { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Uploaded Date")]
        public DateTime CreatedOn { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Completed Date")]
        public DateTime CompletedOn { get; set; }
        public Status Status { get; set; }
        [DisplayFormat(NullDisplayText ="No Description")]
        public string Description { get; set; }
        public virtual Expert Expert { get; set; }
        public virtual Client Client { get; set; }
    }
}