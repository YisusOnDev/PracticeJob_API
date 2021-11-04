using System;
using System.Collections.Generic;
using System.Text;
using PracticeJob.DAL.Entities;

namespace PracticeJob.DAL.Repositories.Contracts
{
    public interface IUserRepository
    {
        bool Login(User u);
        User Create(User u);
        bool Exists(User u);
    }
}
