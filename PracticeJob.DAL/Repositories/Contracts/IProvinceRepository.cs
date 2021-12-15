using System.Collections.Generic;
using PracticeJob.DAL.Entities;

namespace PracticeJob.DAL.Repositories.Contracts
{
    public interface IProvinceRepository
    {
        List<Province> GetAll();
        Province Get(int id);
    }
}
