using System.Collections.Generic;
using PracticeJob.DAL.Entities;

namespace PracticeJob.DAL.Repositories.Contracts
{
    public interface IFPRepository
    {
        List<FP> GetAll();
        FP Get(int id);
    }
}
