using Api.Domain.Interface;
using Microsoft.Extensions.DependencyInjection;
using Api.Data.Repository;
using Api.Data.Context;
using Microsoft.EntityFrameworkCore;

namespace Api.CrossCutting.DependencyInjection
{
    public class ConfigureRepository
    {
        public static void ConfigureDependenciesRepository(IServiceCollection serviceCollection)
        {
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(BaseRepository<>));

            serviceCollection.AddDbContext<MyContext>(
                options => options.UseMySql("Server=localhost;Port=3306;Database=DataBase-API;Uid=root;Pwd=Autoglass@2020")
            );
        }
    }
}
