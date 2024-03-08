using System.Reflection;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using AutoMapper;
using Application.Interfaces.Servicios;
using Application.Services;
using Application.Interfaces.Services;

namespace Application
{
    public static class ServiceExtensions
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            
            services.AddTransient<IUsuariosServices, UsuarioServices>();
            services.AddTransient<IClientesService, ClientesServices>();
            services.AddTransient<IConsultorService, ConsultorService>();
            services.AddTransient<IProyectosService, ProyectosServices>();
            services.AddTransient<ITareasService, TareasServices>();
            services.AddTransient<IAsignacionService, AsignacionService>();
        }
    }
}

