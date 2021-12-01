using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using PracticeJob.BL.Contracts;
using PracticeJob.Core.DTO;
using PracticeJob.Core.Security;
using PracticeJob.DAL.Entities;
using PracticeJob.DAL.Repositories.Contracts;

namespace PracticeJob.BL.Implementations
{
    public class StudentBL : IStudentBL
    {
        public IStudentRepository studentRepository { get; set; }
        public IPasswordGenerator passwordGenerator { get; set; }
        public IMapper mapper { get; set; }

        public StudentBL(IStudentRepository studentRepository, IPasswordGenerator passwordGenerator, IMapper mapper)
        {
            this.studentRepository = studentRepository;
            this.passwordGenerator = passwordGenerator;
            this.mapper = mapper;
        }
        public StudentDTO Login(AuthDTO authDTO)
        {
            authDTO.Password = passwordGenerator.Hash(authDTO.Password);
            var loginData = mapper.Map<AuthDTO, Student>(authDTO);
            var student = mapper.Map<Student, StudentDTO>(studentRepository.Login(loginData));
            return student;
        }

        public StudentDTO Create(AuthDTO authDTO)
        {
            authDTO.Password = passwordGenerator.Hash(authDTO.Password);

            var student = mapper.Map<AuthDTO, Student>(authDTO);

            if (!studentRepository.Exists(student))
            {
                var s = mapper.Map<Student, StudentDTO>(studentRepository.Create(student));
                s.Password = null;
                return s;
            }

            return null;
        }

        public StudentDTO Update(StudentDTO studentDTO)
        {
            var student = mapper.Map<StudentDTO, Student>(studentDTO);
            var updStudent = mapper.Map<Student, StudentDTO>(studentRepository.Update(student));
            return updStudent;
        }
    }
}
