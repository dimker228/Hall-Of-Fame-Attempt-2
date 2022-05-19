using Hall_Of_Fame.Services;
using Hall_Of_Fame.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerUI;


namespace Hall_Of_Fame
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            
            var connection = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<WorkerContext>(options => options.UseSqlServer(connection));
            var optionsBuilder = new DbContextOptionsBuilder<WorkerContext>();

            try
            {
                var options = optionsBuilder
                    .UseSqlServer(connection)
                    .Options;
                using var db = new WorkerContext(options);
                db.Database.Migrate();
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при попытки выполнить Sql запрос:{ex}");
            }

            services.AddMvc();
            services.AddControllers();
            //Настройка Swagger
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Hall OF Fame", Version = "v1" });
            });

            services.AddScoped<ISkillService, SkillService>();

            services.AddScoped<IPersonService, PersonService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            //Подключение swagger
            app.UseSwagger();
           
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
                c.DocExpansion(DocExpansion.None);
            });

            app.UseHttpsRedirection();
            
            app.UseRouting();
            
            app.UseAuthorization();

            app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

        }
    }
}
