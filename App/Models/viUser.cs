using System;

namespace App.Models
{
    public class viUser : viStatusMessage
    {
        public int Id { get; set; }
        public string Login { get; set; }
        public string Token { get; set; }
        public string FIO { get; set; }
    }
}
