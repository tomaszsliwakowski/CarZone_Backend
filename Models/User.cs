using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarZone_Backend.Models
{
    public class User : IdentityUser
    {
      public DateTime CreatedDate { get; set; }
      public bool IsAdmin { get; set; }

    }
}
