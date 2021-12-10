﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.EntityFrameworkCore;
using PracticeJob.DAL.Entities;
using PracticeJob.DAL.Repositories.Contracts;

namespace PracticeJob.DAL.Repositories.Implementations
{
    public class FPRepository : IFPRepository
    {
        public PracticeJobContext DbContext { get; set; }
        public FPRepository(PracticeJobContext context)
        {
            this.DbContext = context;
        }
        public List<FP> GetAll()
        {
            return DbContext.FPs.Include(fp => fp.FPFamily).Include(fp => fp.FPGrade).ToList();
        }

        public FP Get(int id)
        {
            return DbContext.FPs.FirstOrDefault(p => p.Id == p.Id);
        }
    }
}
