﻿using System.ComponentModel.DataAnnotations;

namespace PracticeJob.DAL.Entities
{
    public class Province
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
