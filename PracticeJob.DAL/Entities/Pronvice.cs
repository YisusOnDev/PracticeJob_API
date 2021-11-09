using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace PracticeJob.DAL.Entities
{
    public class Province
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
