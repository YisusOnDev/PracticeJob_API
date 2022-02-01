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
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using PracticeJob.Core.Email;
using Microsoft.AspNetCore.Http.Features;

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
            services.AddMvc();

            // JWT
            services.AddTransient<ITokenService, TokenService>();
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration["JWTSettings:Secret"]))
                };
            });
            services.AddAuthorization(config =>
            {
                var defaultAuthorizationPolicyBuilder = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme);
                defaultAuthorizationPolicyBuilder = defaultAuthorizationPolicyBuilder.RequireAuthenticatedUser();
                config.DefaultPolicy = defaultAuthorizationPolicyBuilder.Build();
            });

            // Bypass Multipart
            services.Configure<FormOptions>(o => {
                o.ValueLengthLimit = int.MaxValue;
                o.MultipartBodyLengthLimit = int.MaxValue;
                o.MemoryBufferThreshold = int.MaxValue;
            });


            // Enable CORS in our API
            string[] exposedHeaders = { "Authorization" };
            services.AddCors(o => {
                o.AddPolicy("AllowSetOrigins", options =>
                {
                    options.WithOrigins("http://localhost:8080");
                    options.AllowAnyHeader();
                    options.AllowAnyMethod();
                    options.AllowCredentials();
                    options.WithExposedHeaders(exposedHeaders);
                });
            });

            AddSwagger(services);

            // FIX JSON OVERFLOW
            services.AddControllers().AddNewtonsoftJson(x => x.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            // AUTO MAPPER
            services.AddAutoMapper(cfg => cfg.AddProfile(new AutomapperProfile()));

            // EF DB CONTEXT
            services.AddDbContext<PracticeJobContext>(opts => opts.UseMySql(Configuration["ConnectionStrings:PracticeJobDB"], ServerVersion.AutoDetect(Configuration["ConnectionStrings:PracticeJobDB"])));

            // INTERFACE - CLASS SCOPES
            services.AddScoped<IStudentBL, StudentBL>();
            services.AddScoped<IStudentRepository, StudentRepository>();
            services.AddScoped<ICompanyBL, CompanyBL>();
            services.AddScoped<ICompanyRepository, CompanyRepository>();
            services.AddScoped<IPasswordGenerator, PasswordGenerator>();
            services.AddScoped<IProvinceRepository, ProvinceRepository>();
            services.AddScoped<IProvinceBL, ProvinceBL>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IFPBL, FPBL>();
            services.AddScoped<IFPRepository, FPRepository>();
            services.AddScoped<IJobOfferBL, JobOfferBL>();
            services.AddScoped<IJobOfferRepository, JobOfferRepository>();
            services.AddScoped<IJobApplicationBL, JobApplicationBL>();
            services.AddScoped<IJobApplicationRepository, JobApplicationRepository>();
            services.AddScoped<IEmailSender, EmailSender>();
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

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = "JWT Authorization header using the Bearer scheme (Example: 'Bearer 12345abcdef')",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement
                    {
                        {
                            new OpenApiSecurityScheme
                            {
                                Reference = new OpenApiReference
                                {
                                    Type = ReferenceType.SecurityScheme,
                                    Id = "Bearer"
                                }
                            },
                            Array.Empty<string>()
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

            app.UseRouting();

            app.UseStaticFiles();

            app.UseAuthorization();
            app.UseAuthentication();

            // app.UseHttpsRedirection();

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "PracticeJob API V1");
            });


            app.UseCors("AllowSetOrigins");

            
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

