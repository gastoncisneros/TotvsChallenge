using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using TotvsChallenge.DataAccess;
using Swashbuckle.AspNetCore.Swagger;
using TotvsChallenge.Business.Services.Interface;
using TotvsChallenge.Business.Services.Service;
using TotvsChallenge.DataAccess.Repository.Interface;
using TotvsChallenge.DataAccess.Repository.Repo;
using TotvsChallenge.Domain.Options;
using TotvsChallenge.Business.DataValidation;
using FluentValidation.AspNetCore;

namespace TotvsChallente
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
            services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))).AddOptions();
            services.AddMvc(opt =>
            {
                //Agrego Fluent Validation
                opt.Filters.Add(typeof(ValidationActionFilter));
            }).AddFluentValidation(fvc => fvc.RegisterValidatorsFromAssemblyContaining<Startup>()); ;
            
            services.Configure<MonedasOptions>(Configuration.GetSection("MonedasOptions"));

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Totvs Challenge API", Version = "v1" });
                c.CustomSchemaIds(x => x.FullName);
                c.DescribeAllEnumsAsStrings();
            });

            #region Services
            services.AddScoped<IMonedaService, MonedaService>();
            #endregion

            #region Repository
            services.AddScoped<ITransaccionRepository, TransaccionRepository>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swaggerDoc, httpReq) =>
                {
                    swaggerDoc.Host = httpReq.Host.Value;
                    swaggerDoc.BasePath = "/";
                    swaggerDoc.Schemes = new List<string>() { "https", "http" };
                });
            });

            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Totvs Challenge API");
            });

            app.UseMvc();
        }
    }
}
