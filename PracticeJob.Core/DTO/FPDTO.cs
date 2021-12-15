using PracticeJob.DAL.Entities;

namespace PracticeJob.Core.DTO
{
    public class FPDTO
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int FPGradeId { get; set; }
        public FPGrade FPGrade { get; set; }
        public int FPFamilyId { get; set; }
        public FPFamily FPFamily { get; set; }
    }
}
