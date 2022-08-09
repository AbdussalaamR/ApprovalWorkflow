using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MEMOJET.Context;
using MEMOJET.Implementations.Repository;
using MEMOJET.Implementations.Service;
using MEMOJET.Implementations.Service.EMailService;
using MEMOJET.Interfaces.Repository;
using MEMOJET.Interfaces.Service;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace MEMOJET
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
            services.AddDbContext<ApplicationContext>(options =>
                options.UseMySQL(Configuration.GetConnectionString("ApplicationContext")));
            
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();
            
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleService, RoleService>();

            services.AddScoped<IResponsibilityCentreRepo, RespoCentreRepo>();
            services.AddScoped<IRespoCentreService, RespoCentreService>();

            services.AddScoped<IApprovalRepo, ApprovalRepo>();
            services.AddScoped<IApprovalService, ApprovalService>();

            services.AddScoped<IFormRepo, FormRepo>();
            services.AddScoped<IFormService, FormService>();

            services.AddScoped<ICommentRepo, CommentRepo>();
            
            services.AddScoped<IUploadedDocRepo, UploadedDocRepo>();
            services.AddScoped<IuploadedDocService, UploadedDocService>();

            services.AddScoped<IUserformRepo, UserFormRepo>();
            
            services.AddScoped<IMailService, MailService>();

            services.AddCors(a => a.AddPolicy("CorsPolicy", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
                
            services.AddHttpContextAccessor();
            services.AddControllers();
            services.AddSwaggerGen(c => { c.SwaggerDoc("v1", new OpenApiInfo {Title = "MEMOJET", Version = "v1"}); });

            var key = "This is the key to user authorization";
            services.AddSingleton<IJWTAuthenticationManager>(new JWTAuthenticationManager(key));
            
            services.AddAuthentication(x =>
                {
                    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                })
                .AddJwtBearer(x =>
                {
                    x.RequireHttpsMetadata = false;
                    x.SaveToken = true;
                    x.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };

                });
        
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "MEMOJET v1"));
            }

            app.UseHttpsRedirection();
            //app.UseStaticFiles();
            app.UseCors("CorsPolicy");
            app.UseRouting();
            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });
        }
    }
}