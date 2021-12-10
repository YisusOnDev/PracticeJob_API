using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using PracticeJob.API;
using PracticeJob.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PracticeJob.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            /*
             *  Example for Inserting JobOffers
            using (var context = new PracticeJobContext())
            {
                var a1 = context.FPs.FirstOrDefault(p => p.Id == 1);
                var a2 = context.FPs.FirstOrDefault(p => p.Id == 2);

                context.AddRange(
                    new JobOffer
                    {
                        Name = "Al Pacino",
                        FPs = new List<FP> { a1, a2 }
                    }); ; ;

                context.SaveChanges();
                foreach (var offer in context.JobOffers.Include(a => a.FPs))
                {
                    Console.WriteLine($"\nJob Offer Name: {offer.Name}");

                    foreach (var fp in offer.FPs)
                    {
                        Console.WriteLine($"FP: " + fp.Name);
                    }
                }
            }
            */
            CreateHostBuilder(args).Build().Run();
            
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
