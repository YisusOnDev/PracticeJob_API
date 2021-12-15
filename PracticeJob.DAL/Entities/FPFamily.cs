using System.ComponentModel.DataAnnotations;

namespace PracticeJob.DAL.Entities
{
    public class FPFamily
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
