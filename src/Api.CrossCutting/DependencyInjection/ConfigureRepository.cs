using Api.Domain.Interface;
using Microsoft.Extensions.DependencyInjection;
using Api.Data.Repository;
using Api.Data.Context;
using Microsoft.EntityFrameworkCore;
using Api.Domain.Repository;
using Api.Data.Implementations;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));
            serviceCollection.AddScoped<IUserRepository, UserImplementation>();

            serviceCollection.AddDbContext<MyContext>(
                options => options.UseMySql("Server=localhost;Port=3306;Database=DataBase-API;Uid=root;Pwd=Autoglass@2020")
            );
        }
    }
}
