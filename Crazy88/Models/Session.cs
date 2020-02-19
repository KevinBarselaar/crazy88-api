using System;
using System.ComponentModel.DataAnnotations;

namespace Crazy88Test.Models
{
    public class Session
    {
        [Key]
        public int Id { get; set; }
        public int Duration { get; set; }
        public DateTime ExpiringDateTime { get; set; }
        public int PlayCode { get; set; }
    }
}
