using System.Collections.Generic;
using PracticeJob.Core.DTO;

namespace PracticeJob.BL.Contracts
{
    public interface IFPBL
    {
        FPDTO Get(int id);
        List<FPDTO> GetAll();
    }
}
