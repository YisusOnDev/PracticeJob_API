using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PracticeJob.DAL.Entities;
using PracticeJob.DAL.Repositories.Contracts;

namespace PracticeJob.DAL.Repositories.Implementations
{
    public class ProvinceRepository : IProvinceRepository
    {
        public PracticeJobContext DbContext { get; set; }
        public ProvinceRepository(PracticeJobContext context)
        {
            this.DbContext = context;
        }
        public List<Province> GetAll()
        {
            return DbContext.Provinces.ToList();
        }

        public Province Get(int id)
        {
            return DbContext.Provinces.FirstOrDefault(p => p.Id == p.Id);
        }
    }
}
