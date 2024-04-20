using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.ViewModels.Usuarios
{
    public class ResetPasswordViewModel
    {
        [Required]
        public  string? Token { get; set; }
        [Required]
        public string? Password {  get; set; }
        [Required]
        public  string? ConfirmPassword { get; set; }
    }
}
