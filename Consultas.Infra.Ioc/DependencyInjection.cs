using Consultas.Domain.Interfaces;
using Consultas.Infra.Data.Context;
using Consultas.Infra.Data.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace Consultas.Infra.Ioc
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructure (this IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                    o => o.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName));
            });

            services.AddScoped<IAppointmentRepository, AppointmentRepository>();

            services.AddScoped<IAvailabilitySlotRepository, AvailabilitySlotRepository>();

            services.AddScoped<IDoctorRepository, DoctorRepository>();

            services.AddScoped<IPatientRepository, PatientRepository>();

            services.AddScoped<ISpecialtyRepository, SpecialtyRepository>();

            services.AddScoped<IPersistenceWorkRepository, PersistenceWorkRepository>();

            return services;
        }
    }
}
