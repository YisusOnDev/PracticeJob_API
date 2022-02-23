using PracticeJob.Core.DTO;
using System.Collections.Generic;

namespace PracticeJob.BL.Contracts
{
    public interface IPrivateMessageBL
    {
        List<PrivateMessageDTO> GetAllUnread(int studentId);
        bool Send(PrivateMessageDTO pmDTO);
        bool SetAsRead(int pmId);   
    }
}
