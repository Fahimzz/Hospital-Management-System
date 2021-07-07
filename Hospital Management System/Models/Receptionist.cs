//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Hospital_Management_System.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;

    public partial class Receptionist
    {
        [Required(ErrorMessage = "Please Put a Unique Name Which Starts With 'R'")]
        [MaxLength(5)]
        public string Username { get; set; }
        [Required(ErrorMessage = "Please Put A Name")]
        public string Rname { get; set; }
        [Required(ErrorMessage = "Please Put A Age")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Please Put A Mobile Number")]
        [MaxLength(11, ErrorMessage = "Give the Valid Number")]
        [MinLength(11, ErrorMessage = "Give the Valid Number")]
        public string Mobile { get; set; }
        [Required(ErrorMessage = "Please Put A Gender")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Please Put A Address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "Give A Password")]
        public string Password { get; set; }
        public string Type { get; set; }
       

        public virtual Login Login { get; set; }
    }
}
