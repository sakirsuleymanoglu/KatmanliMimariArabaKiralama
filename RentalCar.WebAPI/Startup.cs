using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using RentalCar.Business.Abstract;
using RentalCar.Business.Concrete;
using RentalCar.DataAccess.Abstract;
using RentalCar.DataAccess.Concrete.EntityFramework;
using Microsoft.OpenApi.Models;

namespace RentalCar.WebAPI
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
            services.AddCors();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("RentalCar", new OpenApiInfo // Bu k�s�mda d�k�manda kullan�lacak bilgileri tan�ml�yoruz.Versiyon,Ba�l�k,A��klama,Servis gibi bilgileri yazabiliriz.
                { //Burada dikkat edilmesi gereken konu yukar�da parametre olarak ge�irdi�imizi "ProductApi". Burada verdi�iniz de�er ile a�a��da configure i�erisinde swagger�n json dosyas�n�n pathini verirken kulland���m�z de�er ayn� olmal�
                    Version = "v1",
                    Title = "Product API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Metin Yorgun",
                        Email = "metinyorgun@outlook.com",
                        Url = new Uri("https://www.google.com"),
                    },
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

            app.UseCors(builder=>builder.WithOrigins("http://localhost:4200/").AllowAnyHeader());

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger(); // Projemize swagger kullanaca��m�z� s�yledik
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/RentalCar/swagger.json", "RentalCar"); 
            });
        }
    }
}
