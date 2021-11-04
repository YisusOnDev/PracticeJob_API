using System;
using System.Collections.Generic;
using System.Text;
using PracticeJob.Core.DTO;

namespace PracticeJob.BL.Contracts
{
    public interface IUserBL
    {
        bool Login(UserDTO userDTO);
        UserDTO Create(UserDTO userDTO);
    }
}
