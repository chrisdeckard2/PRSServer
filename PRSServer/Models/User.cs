using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography.X509Certificates;

namespace PRSServer.Models;

[Index("Id", IsUnique =true, Name = "Unique_Id")]

[Index("Username", IsUnique =true, Name = "Unique_Username")]

public class User {

    public int Id { get; set; }
    //1,1
    
    [StringLength(30)]
    public string Username { get; set; } = string.Empty;

    [StringLength(30)]
    public string Password { get; set; } = string.Empty;

    [StringLength (30)]
    public string Firstname { get; set; } = string.Empty;

    [StringLength (30)]
    public string Lastname { get; set; } = string.Empty;

    [StringLength (12)]
    public string? Phone {  get; set; } = string.Empty;

    [StringLength (255)]
    public string? Email { get; set; }

    public bool IsReviewer { get; set; }

    public bool IsAdmin {  get; set; }
    
   
   


}
