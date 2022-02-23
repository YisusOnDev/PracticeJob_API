using AutoMapper;
using PracticeJob.BL.Contracts;
using PracticeJob.Core.DTO;
using PracticeJob.DAL.Entities;
using PracticeJob.DAL.Repositories.Contracts;
using System.Collections.Generic;

namespace PracticeJob.BL.Implementations
{
    public class PrivateMessageBL : IPrivateMessageBL
    {
        public IPrivateMessageRepository PrivateMessageRepository { get; set; }
        public IMapper Mapper { get; set; }

        public PrivateMessageBL(IPrivateMessageRepository privateMessageRepository, IMapper Mapper)
        {
            this.PrivateMessageRepository = privateMessageRepository;
            this.Mapper = Mapper;
        }

        public List<PrivateMessageDTO> GetAllUnread(int studentId)
        {
            List<PrivateMessage> unreadMessages = PrivateMessageRepository.GetAllUnread(studentId);
            List<PrivateMessageDTO> unreadMessagesDTO = Mapper.Map<List<PrivateMessage>, List<PrivateMessageDTO>>(unreadMessages);
            return unreadMessagesDTO;
        }

        public bool Send(PrivateMessageDTO pmDTO)
        {
            PrivateMessage pm = Mapper.Map<PrivateMessageDTO, PrivateMessage>(pmDTO);
            bool succesfull = PrivateMessageRepository.Create(pm);
            return succesfull;
        }

        public bool SetAsRead(int pmId)
        {
            bool successfull = PrivateMessageRepository.SetAsRead(pmId);
            return successfull;
        }
    }
}
