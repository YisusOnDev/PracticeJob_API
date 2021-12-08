﻿using System;
using System.Collections.Generic;
using System.Text;
using PracticeJob.Core.DTO;
using PracticeJob.DAL.Entities;

namespace PracticeJob.BL.Contracts
{
    public interface IFPBL
    {
        List<FPDTO> GetAll();
        FPDTO Get(int id);
    }
}
