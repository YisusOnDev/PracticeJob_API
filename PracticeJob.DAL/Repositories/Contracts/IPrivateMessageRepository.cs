using PracticeJob.DAL.Entities;
using System.Collections.Generic;

namespace PracticeJob.DAL.Repositories.Contracts
{
    public interface IPrivateMessageRepository
    {
        List<PrivateMessage> GetAllUnread(int studentId);
        bool Create(PrivateMessage pm);
        bool SetAsRead(int pmId);
    }
}
