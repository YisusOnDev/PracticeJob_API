using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using PracticeJob.BL;
using PracticeJob.BL.Contracts;
using PracticeJob.BL.Implementations;
using PracticeJob.DAL.Entities;
using PracticeJob.DAL.Repositories.Contracts;
using PracticeJob.DAL.Repositories.Implementations;
using Microsoft.OpenApi.Models;
using PracticeJob.Core.Security;
using PracticeJob.Core.AutomapperProfiles;

namespace PracticeJob.API
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            // Enable CORS in our API
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy",
                    builder => builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

            AddSwagger(services);

            services.AddAutoMapper(cfg => cfg.AddProfile(new AutomapperProfile()));

            services.AddDbContext<TestApiBeContext>(opts => opts.UseMySql(Configuration["ConnectionStrings:TestApiDB"], ServerVersion.AutoDetect(Configuration["ConnectionStrings:TestApiDB"])));

            //Aquí las inyecciones: Interfaz - Clase
            services.AddScoped<IUserBL, UserBL>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IPasswordGenerator, PasswordGenerator>();
        }

        private void AddSwagger(IServiceCollection services)
        {
            services.AddSwaggerGen(options =>
            {
                var groupName = "v1";

                options.SwaggerDoc(groupName, new OpenApiInfo
                {
                    Title = $"PracticeJob {groupName}",
                    Version = groupName,
                    Description = "PracticeJob API",
                    Contact = new OpenApiContact
                    {
                        Name = "PracticeJob API",
                        Email = string.Empty,
                        Url = new Uri("https://yisus.dev/"),
                    }
                });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PracticeJob API V1");
            });

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

