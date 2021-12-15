using System.ComponentModel.DataAnnotations;

namespace PracticeJob.DAL.Entities
{
    public class FPGrade
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
