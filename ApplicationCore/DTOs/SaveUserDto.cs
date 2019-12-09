using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;
using System.Text;

namespace ApplicationCore.DTOs
{
    public class SaveUserDto
    {
        [Key]
        [Required]
        public int ID { get; set; }
        [Required]
        [StringLength(45, MinimumLength = 3, ErrorMessage = "FirstName must be greater than 3")]
        [Column(TypeName = "nvarchar")]
        public string FirstName { get; set; }
        [Required]
        [StringLength(45, MinimumLength = 3, ErrorMessage = "LastName must be greater than 3")]
        [Column(TypeName = "nvarchar")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Please input email")]
        [Column(TypeName = "nvarchar")]
        public string Mail { get; set; }

        [Required(ErrorMessage = "Please input password")]
        [Column(TypeName = "nvarchar")]
        public string Password { get; set; }

        [Required(ErrorMessage = "Please input birth")]
        [Column(TypeName = "datetime2")]
        public string Birth { get; set; }

        [Column(TypeName = "nvarchar")]
        public string Phone { get; set; }

        [Column(TypeName = "nvarchar")]
        public string Address { get; set; }

        [Column(TypeName = "nvarchar")]
        public string AvtPath { get; set; }

        [Column(TypeName = "nvarchar")]
        public string Note { get; set; }

        [Required]
        [Range(0, 1, ErrorMessage = "Status must 0 or 1")]
        [Column(TypeName = "smallint")]
        public int Status { get; set; }

        [Column(TypeName = "smallint")]
        public int Level { get; set; }
    }
}
