using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace QuoteManager.Dtos
{
    public class userDto
    {
        public int USERID { get; set; }
        [Required]
        public string USERNAME { get; set; }
        [Required]
        public string USERPASSWORD { get; set; }
        [Required]
        [Compare("USERPASSWORD",ErrorMessage = "Password not match!")]
        public string CONFIRMPASS { get; set; }
    }
}