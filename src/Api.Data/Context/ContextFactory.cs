using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Api.Data.Context
{

    public class ContextFactor : IDesignTimeDbContextFactory<MyContext>
    {
        public MyContext CreateDbContext(string[] args)
        {
            //Usando para criar as Migrações
            var connectionString = "Server=localhost;Port=3306;Database=dbAPI;Uid=root;Pwd=Autoglass@2020";
            var optionsBuider = new DbContextOptionsBuilder<MyContext>();
            optionsBuider.UseMySql(connectionString);
            return new MyContext(optionsBuider.Options);
        }
    }
}
