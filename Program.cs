using Controle_de_Transporte_FrontEnd.Service.Interface;
using Controle_de_Transporte_FrontEnd.Service;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Controle_de_Transporte_FrontEnd.Repository.Interface;
using Controle_de_Transporte_FrontEnd.Repository;

namespace Controle_de_Transporte_FrontEnd
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configuração da API base URL
            builder.Configuration.AddJsonFile("appsettings.json");
            var configuration = builder.Configuration;

            // Add services to the container.
            builder.Services.AddControllersWithViews();



            // Configurar a URL base da API
            string apiBaseUrl = configuration.GetValue<string>("AppSettings:ApiBaseUrl");

            builder.Services.AddHttpClient("ApiHttpClient", client =>
            {
                client.BaseAddress = new Uri(apiBaseUrl);
            });

            // Adicionar seu serviço
            builder.Services.AddTransient<ICargoRepository, CargoRepository>();
            builder.Services.AddTransient<ICargoService, CargoService>();
            builder.Services.AddTransient<IDepartamentoRepository, DepartamentoRepository>();
            builder.Services.AddTransient<IDepartamentoService, DepartamentoService>();
            builder.Services.AddTransient<IFuncionariosRepository, FuncionariosRepository>();
            builder.Services.AddTransient<IFuncionariosService, FuncionariosService>();
            builder.Services.AddTransient<IInstituicaoRepository, InstituicaoRepository>();
            builder.Services.AddTransient<IInstituicaoService, InstituicaoService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            await app.RunAsync();
        }
    }
}
