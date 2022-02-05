using Microsoft.AspNetCore.Http;
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

        public static bool ImageIsValid(IFormFile file)
        {
            List<string> ImageExtensions = new List<string> { ".JPG", ".JPEG", ".BMP", ".GIF", ".PNG" };
            if (ImageExtensions.Contains(System.IO.Path.GetExtension(file.FileName).ToUpper()))
            {
                return true;
            }
            return false;
        }
    }
}
