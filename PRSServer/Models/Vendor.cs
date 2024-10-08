﻿using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.Permissions;

namespace PRSServer.Models;

[Index("Id", IsUnique = true, Name = "Unique_Id")]

[Index("Code", IsUnique = true, Name = "Unique_Code")]

public class Vendor {

    public int Id { get; set; }
    

    [StringLength(30)]
    public string Code { get; set; } = string.Empty;

    [StringLength (30)] 
    public string Name { get; set; } = string.Empty;

    [StringLength(30)]
    public string Address { get; set; } = string.Empty;

    [StringLength (30)]
    public string City { get; set; } = string.Empty;

    [StringLength (2)]
    public string State { get; set; } = string.Empty;

    [StringLength (5)]
    public string Zip {  get; set; } = string.Empty;

    [StringLength (12)]
    public string? Phone { get; set; } 

    [StringLength (255)]
    public string? Email { get; set; } 





}