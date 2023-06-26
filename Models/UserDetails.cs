using System.ComponentModel.DataAnnotations;

namespace StudentManagementASPDotNet.Models;

public class UserDetails
{
    [Key]
    public string id { get; set; }
    
    public string username { get; set; }
    public string email { get; set; }
    public string password { get; set; }
    public string role { get; set; }
    
    public bool isdelete { get; set; }
}