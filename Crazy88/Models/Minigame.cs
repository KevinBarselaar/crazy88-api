using System.ComponentModel.DataAnnotations;

namespace Crazy88Test.Models
{
    public class Minigame
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int MaxScore { get; set; }
        public int MinScore { get; set; }
        public string CardImage { get; set; }
        public string QRValue { get; set; }
        public string Location { get; set; }
        public bool Active { get; set; }
    }

    public class Minigames
    {
        public Minigame[] minigames;
    }
}