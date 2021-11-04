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
    public class UserBL : IUserBL
    {
        public IUserRepository userRepository { get; set; }
        public IPasswordGenerator passwordGenerator { get; set; }
        public IMapper mapper { get; set; }

        public UserBL(IUserRepository userRepository, IPasswordGenerator passwordGenerator, IMapper mapper)
        {
            this.userRepository = userRepository;
            this.passwordGenerator = passwordGenerator;
            this.mapper = mapper;
        }
        public bool Login(UserDTO userDTO)
        {
            userDTO.Password = passwordGenerator.Hash(userDTO.Password);
            var user = mapper.Map<UserDTO, User>(userDTO);

            return userRepository.Login(user);
        }

        public UserDTO Create(UserDTO userDTO)
        {
            userDTO.Password = passwordGenerator.Hash(userDTO.Password);

            var user = mapper.Map<UserDTO, User>(userDTO);

            if (!userRepository.Exists(user))
            {
                var u = mapper.Map<User, UserDTO>(userRepository.Create(user));
                u.Password = null;
                return u;
            }

            return null;
        }
    }
}
