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
            var user = mapper.Map<AuthDTO, Student>(authDTO);
            var student = mapper.Map<Student, StudentDTO>(studentRepository.Login(user));
            return student;
        }

        public StudentDTO Create(AuthDTO authDTO)
        {
            authDTO.Password = passwordGenerator.Hash(authDTO.Password);

            var user = mapper.Map<AuthDTO, Student>(authDTO);
            user.ProvinceId = 1;

            if (!studentRepository.Exists(user))
            {
                var u = mapper.Map<Student, StudentDTO>(studentRepository.Create(user));
                u.Password = null;
                return u;
            }

            return null;
        }
    }
}
