using Application.Interfaces.Repositories;
using Application.Interfaces.Repositorios;
using Application.Interfaces.Servicios;
using Application.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Contextos;
using Persistence.Repositorios;

namespace Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            #region "Contexts"
            if (configuration.GetValue<bool>("UseInMemoryDatabase"))
            {
                services.AddDbContext<ApplicationDbContext>(options => options.UseInMemoryDatabase("ApplicationDb"));
            }
            else
            {
                services.AddDbContext<ApplicationDbContext>(option =>
                {
                    option.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                        m => m.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
                });
            }
            #endregion

            #region "Repositories"
            services.AddTransient(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddTransient<IUsuariosRepository, UsuariosRepository>();
            services.AddTransient<IClientesRepository, ClientesRepository>();
            services.AddTransient<IConsultorRepository, ConsultorRepository>();
            services.AddTransient<IProyectosRepository, ProyectosRepository>();
            services.AddTransient<ITareasRepository, TareasRepository>();
            services.AddTransient<IAsignacionRepository, AsignacionRepository>();
            #endregion

        }
    }
}
