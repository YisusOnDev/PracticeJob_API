using System.Collections.Generic;
using PracticeJob.Core.DTO;

namespace PracticeJob.BL.Contracts
{
    public interface IProvinceBL
    {
        ProvinceDTO Get(int id);
        List<ProvinceDTO> GetAll();
    }
}
