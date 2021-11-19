using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using PracticeJob.BL.Contracts;
using PracticeJob.BL.Implementations;
using PracticeJob.DAL.Entities;
using PracticeJob.DAL.Repositories.Contracts;
using PracticeJob.DAL.Repositories.Implementations;
using Microsoft.OpenApi.Models;
using PracticeJob.Core.Security;
using PracticeJob.Core.AutomapperProfiles;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

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
            services.AddMvc();
            services.AddSession();

            services.AddTransient<ITokenService, TokenService>();
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JWTSettings:Secret"])),
                    ValidIssuer = Configuration["JWTSettings:Issuer"],
                    ValidAudience = Configuration["JWTSettings:Audience"],
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });

            // Enable CORS in our API
            services.AddCors(o => {
                o.AddPolicy("AllowSetOrigins", options =>
                {
                    options.WithOrigins("http://localhost:8080");
                    options.AllowAnyHeader();
                    options.AllowAnyMethod();
                    options.AllowCredentials();
                });
            });

            AddSwagger(services);

            services.AddAutoMapper(cfg => cfg.AddProfile(new AutomapperProfile()));

            services.AddDbContext<PracticeJobContext>(opts => opts.UseMySql(Configuration["ConnectionStrings:PracticeJobDB"], ServerVersion.AutoDetect(Configuration["ConnectionStrings:PracticeJobDB"])));

            // Interface and Class inyections
            services.AddScoped<IStudentBL, StudentBL>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ICompanyBL, CompanyBL>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IPasswordGenerator, PasswordGenerator>();
            services.AddScoped<IProvinceRepository, ProvinceRepository>();
            services.AddScoped<IProvinceBL, ProvinceBL>();
            services.AddScoped<ITokenService, TokenService>();
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

            app.UseSession();

            app.Use(async (context, next) =>
            {
                var token = context.Session.GetString("Token");
                if (!string.IsNullOrEmpty(token))
                {
                    context.Request.Headers.Add("Authorization", "Bearer " + token);
                }
                await next();
            });


            app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PracticeJob API V1");
            });

            app.UseRouting();

            app.UseCors("AllowSetOrigins");

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

