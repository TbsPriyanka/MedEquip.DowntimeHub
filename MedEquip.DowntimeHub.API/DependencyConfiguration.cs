using MedEquip.DowntimeHub.BAL.Interfaces;
using MedEquip.DowntimeHub.BAL.Services;
using MedEquip.DowntimeHub.Common.JwtHelper;
using MedEquip.DowntimeHub.Common.SqlHelper;
using MedEquip.DowntimeHub.DAL.Interfaces;
using MedEquip.DowntimeHub.DAL.Repository;

namespace MedEquip.DowntimeHub.API
{
    public static class DependencyConfiguration
    {
        public static void RegisterServices(this IServiceCollection services)
        {
            // SqlHelper 
            services.AddScoped<ISqlHelper, SqlHelper>();

            // JwtTokenHelper
            services.AddScoped<JwtTokenHelper>();

            // Auth
            services.AddScoped<ILoginRepository, LoginRepository>();
            services.AddScoped<ILoginService, LoginService>();

            // User
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IUserService, UserService>();

            // Role
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IRoleService, RoleService>();

            // Organization
            services.AddScoped<IOrganizationRepository, OrganizationRepository>();
            services.AddScoped<IOrganizationService, OrganizationService>();
        }
    }
}
