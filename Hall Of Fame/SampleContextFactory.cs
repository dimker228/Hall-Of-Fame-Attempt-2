using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;


namespace Hall_Of_Fame
{
    public class SampleContextFactory : IDesignTimeDbContextFactory<WorkerContext>
    {
        public WorkerContext CreateDbContext(string[] args)
        {
            /// <summary>
            /// Класс, для созданния миграций
            /// </summary>           
            var optionsBuilder = new DbContextOptionsBuilder<WorkerContext>();

            // получаем конфигурацию из файла appsettings.json
            ConfigurationBuilder builder = new ConfigurationBuilder();
            builder.SetBasePath(Directory.GetCurrentDirectory());
            builder.AddJsonFile("appsettings.json");
            IConfigurationRoot config = builder.Build();

            // получаем строку подключения из файла appsettings.json
            string connectionString = config.GetConnectionString("DefaultConnection");
            optionsBuilder.UseSqlServer(connectionString, opts => opts.CommandTimeout((int)TimeSpan.FromMinutes(10).TotalSeconds));
            return new WorkerContext(optionsBuilder.Options);
        }
    }
}