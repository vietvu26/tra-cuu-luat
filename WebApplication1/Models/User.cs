using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
// using SQLite;
namespace WebApplication1.Models
{
    public class User
    {
        //[PrimaryKey, AutoIncrement]
        [Key] public int ID { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string PassWord { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;
        public int Mobile { get; set; }
        public string Role { get; set; } = string.Empty;
    }
}
