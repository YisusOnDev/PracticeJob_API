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
        public PracticeJobContext _context { get; set; }
        public ProvinceRepository(PracticeJobContext context)
        {
            this._context = context;
        }
        public List<Province> Get()
        {
            var provinces = _context.Provinces.ToList();

            List<Province> provinceList = new List<Province>();

            foreach (Province p in provinces)
            {
                provinceList.Add(p);
            }
            
            return provinceList;
        }
    }
}
