using System.ComponentModel.DataAnnotations;

namespace Crazy88Test.Models
{
    public class Team
    {
        [Key]
        public int Id { get; set; }
        public string TeamName { get; set; }
        public int Score { get; set; }
        public string Photo { get; set; }
        public int Session { get; set; }
    }

    public class Teams
    {
        public Team[] teams;
    }
}
