using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CertVault.Models.DBModels
{
    public class Client
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int ClientId { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

        [Required]
        [Display(Name = "Created At")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime CreatedAt { get; set; }

        
        [Display(Name = "Updated By")]
        public string UpdatedBy { get; set; }

       
        [Display(Name = "Updated At")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime UpdatedAt { get; set; }

        public virtual ICollection<Certificate> Certificates { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual ApplicationUser ApplicationUsers { get; set; }

        [ForeignKey("UpdatedBy")]
        public virtual ApplicationUser ApplicationUsers2 { get; set; }
    }
}