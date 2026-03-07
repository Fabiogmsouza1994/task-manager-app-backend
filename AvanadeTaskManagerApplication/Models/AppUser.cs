using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;
using System.Reflection.Metadata;
namespace AvanadeTaskManagerApplication.Models;

public class AppUser : IdentityUser
{
    [PersonalData]
    [Column(TypeName="nvarchar(150)")]
    public string FullName { get; set; }
}
