using EntityLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace DataStructureTree.Models
{
    public class RegisterModel
    {
        [Required]
        public string FirstName { get; set; }


        [Required]
        public string LastName { get; set; }


        [Required]
        [DataType(DataType.EmailAddress)]
        [EmailAddress]
        public string Email { get; set; }


        [Required]
        [DataType(DataType.PhoneNumber)]
        [Phone]
        public string PhoneNumber { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }


        [Required]
        [DataType(DataType.Password)]
        [MinLength(5,ErrorMessage ="Şifreniz en az 5 karakterli olmalıdır.")]
        [Compare("Password",ErrorMessage = "Şifreniz eşleşmiyor.")] 
        public string RePassword { get; set; }


    }

}
