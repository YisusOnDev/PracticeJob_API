using System.Linq;
using Microsoft.EntityFrameworkCore;
using PracticeJob.DAL.Entities;
using PracticeJob.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;

namespace PracticeJob.DAL.Repositories.Implementations
{
    public class PrivateMessageRepository : IPrivateMessageRepository
    {
        public PracticeJobContext DbContext { get; set; }
        public PrivateMessageRepository(PracticeJobContext context)
        {
            this.DbContext = context;
        }

        public List<PrivateMessage> GetAllUnread(int studentId)
        {
            return DbContext.PrivateMessages.
                Include(c => c.Company).
                ThenInclude(cp => cp.Province).
                Where(pm => pm.StudentId == studentId && pm.Read == false).ToList();
        }
        public bool Create(PrivateMessage pm)
        {
            // Set pm as not read
            pm.Read = false;
            DbContext.PrivateMessages.Add(pm);
            DbContext.SaveChanges();
            return true;
        }

        public bool SetAsRead(int pmId)
        {
            var pmFromDb = DbContext.PrivateMessages.SingleOrDefault(pm => pm.Id == pmId);
            if (pmFromDb != null)
            {
                pmFromDb.Read = true;
                DbContext.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
