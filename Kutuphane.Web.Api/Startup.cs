using Kutuphane.Bussiness.Abstract;
using Kutuphane.Bussiness.Concrete;
using Kutuphane.DAL.Context;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kutuphane.Web.Api
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
			services.AddCors(options =>
			{
				options.AddPolicy("CorsPolicy",
					builder =>
					 // Servisi belirli bir adresten gelen taleplere ama
					 //builder.WithOrigins("http://localhost:3000","http://94.73.164.170:")
					 // Servisi tm taleplere ama
					 builder.AllowAnyOrigin()
					.AllowAnyMethod()
					.AllowAnyHeader());
			});




			services.AddControllers();
			services.AddSwaggerGen(c =>
			{
				c.SwaggerDoc("v1", new OpenApiInfo { Title = "Kutuphane.Web.Api", Version = "v1" });
			});

			//***
			services.AddDbContext<KutuphaneDbContext>(); //PL -> DAL
			services.AddScoped<IBookService, BookService>();
			services.AddScoped<ICategoryService, CategoryService>();
			services.AddScoped<ISubCategoryService, SubCategoryService>();


			//AddScoped
			//AddDbContext
			//AddSingleton

		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
		{
			if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
				app.UseSwagger();
				app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Kutuphane.Web.Api v1"));
			}

			app.UseCors("CorsPolicy");
			app.UseRouting();

			app.UseAuthorization();

			app.UseEndpoints(endpoints =>
			{
				endpoints.MapControllers();
			});
		}
	}
}
