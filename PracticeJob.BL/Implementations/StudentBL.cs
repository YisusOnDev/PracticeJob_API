using AutoMapper;
using PracticeJob.BL.Contracts;
using PracticeJob.Core.DTO;
using PracticeJob.Core.Email;
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
        public IEmailSender EmailSender { get; set; }

        public StudentBL(IStudentRepository StudentRepository, IPasswordGenerator PwdGenerator, IMapper Mapper, IEmailSender EmailSender)
        {
            this.StudentRepository = StudentRepository;
            this.PwdGenerator = PwdGenerator;
            this.Mapper = Mapper;
            this.EmailSender = EmailSender;
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

        public void ConfirmEmailSend(StudentDTO studentDTO)
        {
            var confirmCode = StudentRepository.Generate2FACode(studentDTO.Email);
            EmailSender.SendConfirmationMail(studentDTO.Email, confirmCode);
        }

        public StudentDTO ValidateEmail(StudentDTO studentDTO, string code)
        {
            var student = Mapper.Map<StudentDTO, Student>(studentDTO);
            return Mapper.Map<Student, StudentDTO>(StudentRepository.ValidateEmail(student, code));
        }
        public bool ResetPasswordSend(string email)
        {
            var accountExists = StudentRepository.EmailRegistered(email);
            if (accountExists)
            {
                var confirmCode = StudentRepository.Generate2FACode(email);
                EmailSender.SendPasswordReset(email, confirmCode);
                return true;
            }
            return false;
        }

        public bool UpdatePassword(PasswordResetDTO passwordReset)
        {
            passwordReset.Password = PwdGenerator.Hash(passwordReset.Password);
            var newPasswordReset = Mapper.Map<PasswordResetDTO, PasswordReset>(passwordReset);
            var passwordReseted = StudentRepository.UpdatePassword(newPasswordReset);
            return passwordReseted;
        }
        public StudentDTO SetProfileImage(int studentId, string fileName)
        {
            var student = Mapper.Map<Student, StudentDTO>(StudentRepository.SetProfileImage(studentId, fileName));
            return student;
        }
    }
}
