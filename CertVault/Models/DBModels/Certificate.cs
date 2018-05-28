using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace CertVault.Models.DBModels
{
    public class Certificate
    {
        [Key]
        [Column(Order = 0)]
        [Required]
        public string CertificateNumber { get; set; }

        [Key]
        [Column(Order = 1)]
        [Required]
        public int MemberID { get; set; }

        [Key]
        [Column(Order = 2)]
        [Required]
        [Display(Name = "Symbol ISIN")]
        public string SymbolIsin { get; set; }

        [Required]
        public long Volume { get; set; }

        [Required]
        public string Status { get; set; }

        [Required]
        [Display(Name = "Created At")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime CreatedAt { get; set; }

        [Required]
        [Display(Name = "Created By")]
        public string CreatedBy { get; set; }

       
        [Display(Name = "Apporved At")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> ApprovedAt { get; set; }

        
        [Display(Name = "Approved By")]
        public string ApprovedBy { get; set; }

     
        [Display(Name = "Withdraw Request At")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> WithdrawRequestAt { get; set; }

        
        [Display(Name = "Withdraw Request By")]
        public string WithdrawRequestBy { get; set; }

       
        [Display(Name = "Withdraw Apporved At")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:MM/dd/yyyy}")]
        public Nullable<System.DateTime> WithdrawApprovedAt { get; set; }

       
        [Display(Name = "Withdraw Apporved By")]
        public string WithdrawApprovedBy { get; set; }

        [Required]
        [Display(Name = "Updated At")]
        [DataType(DataType.DateTime)]
        [DisplayFormat(ApplyFormatInEditMode = false, DataFormatString = "{0:MM/dd/yyyy}")]
        public System.DateTime UpdatedAt { get; set; }

        
        public string Client { get; set; }
        public Nullable<int> ClientId { get; set; }

        [Display(Name = "Vault Name")]
        public Nullable<int> VaultID { get; set; }

        [ForeignKey("CreatedBy")]
        public virtual ApplicationUser ApplicationUsers { get; set; }

        [ForeignKey("ApprovedBy")]
        public virtual ApplicationUser ApplicationUsers2 { get; set; }

        [ForeignKey("WithdrawRequestBy")]
        public virtual ApplicationUser ApplicationUsers3 { get; set; }

        [ForeignKey("WithdrawApprovedBy")]
        public virtual ApplicationUser ApplicationUsers4 { get; set; }
        public virtual Vault Vaults { get; set; }
    }
}