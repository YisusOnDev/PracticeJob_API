using System;
using System.Collections.Generic;
using System.Text;

namespace PracticeJob.Core.Common
{
    public class CustomUtils
    {
        public string Generate4DigitCode()
        {
            return new Random().Next(1000, 9999).ToString();
        }
    }
}
