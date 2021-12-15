using AutoMapper;
using PracticeJob.BL.Contracts;
using PracticeJob.Core.DTO;
using PracticeJob.Core.Security;
using PracticeJob.DAL.Entities;
using PracticeJob.DAL.Repositories.Contracts;

namespace PracticeJob.BL.Implementations
{
    public class StudentBL : IStudentBL
    {
        public IStudentRepository StudentRepository { get; set; }
        public IPasswordGenerator PwdGenerator { get; set; }
        public IMapper Mapper { get; set; }

        public StudentBL(IStudentRepository StudentRepository, IPasswordGenerator PwdGenerator, IMapper Mapper)
        {
            this.StudentRepository = StudentRepository;
            this.PwdGenerator = PwdGenerator;
            this.Mapper = Mapper;
        }
        public StudentDTO Login(AuthDTO authDTO)
        {
            authDTO.Password = PwdGenerator.Hash(authDTO.Password);
            var loginData = Mapper.Map<AuthDTO, Student>(authDTO);
            var student = Mapper.Map<Student, StudentDTO>(StudentRepository.Login(loginData));
            return student;
        }
        public StudentDTO Get(int studentId)
        {
            var student = Mapper.Map<Student, StudentDTO>(StudentRepository.Get(studentId));
            return student;
        }

        public StudentDTO Create(AuthDTO authDTO)
        {
            authDTO.Password = PwdGenerator.Hash(authDTO.Password);

            var student = Mapper.Map<AuthDTO, Student>(authDTO);

            if (!StudentRepository.Exists(student))
            {
                var s = Mapper.Map<Student, StudentDTO>(StudentRepository.Create(student));
                s.Password = null;
                return s;
            }

            return null;
        }

        public StudentDTO Update(StudentDTO studentDTO)
        {
            var student = Mapper.Map<StudentDTO, Student>(studentDTO);
            var updStudent = Mapper.Map<Student, StudentDTO>(StudentRepository.Update(student));
            return updStudent;
        }
    }
}
