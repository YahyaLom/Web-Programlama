using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace HastaneOtomasyonASP.NET.Models
{
    public class ApplicationUser:IdentityUser
    {

        [Required]
        public int UserNo { get; set; }
        public string? Adres { get; set; }




    }
}
