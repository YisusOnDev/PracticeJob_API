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
        public List<Province> GetAll()
        {
            var provinces = _context.Provinces.ToList();
            return provinces;
        }

        public Province Get(int id)
        {
            return _context.Provinces.FirstOrDefault(p => p.Id == p.Id);
        }
    }
}
