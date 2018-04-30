using System;
using System.IO;
using ED.Application.ServiceImp;
using ED.Common.Filters;
using ED.Common.Helpers;
using ED.Common.IoC;
using ED.Common.Options;
using ED.Repositories.Core;
using ED.Repositories.Dapper;
using ED.Repositories.EntityFramework;
using log4net;
using log4net.Repository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace ED.WebAPI
{
    public class Startup
    {

        public static ILoggerRepository LoggerRepository { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //初始化log4net
            LoggerRepository = LogManager.CreateRepository("NETCoreRepository");
            Log4NetHelper.SetConfig(LoggerRepository, "log4net.config");
        }
        
        public IConfiguration Configuration { get; }
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {

            services.AddMvc(option =>
            {
                option.Filters.Add(new GlobalExceptionFilter());
            });

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "ToDo API",
                    Description = "A simple example ASP.NET Core Web API",
                    TermsOfService = "None",
                    Contact = new Contact { Name = "Shayne Boyer", Email = "", Url = "https://twitter.com/spboyer" },
                    License = new License { Name = "Use under LICX", Url = "https://example.com/license" }
                });
            });
            
            return InitIoC(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            //Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseBrowserLink();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
            }

            app.UseStaticFiles();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Index}/{id?}");
            });

            app.UseSwagger();

            // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.), specifying the Swagger JSON endpoint.
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");
            });
            

            //DbInitializer.Initialize(app.ApplicationServices);
        }


        private IServiceProvider InitIoC(IServiceCollection services)
        {
            var commandString = Configuration.GetConnectionString("CommandDB");
            var queryString = Configuration.GetConnectionString("QueryDB");
            var dbContextOption = new DbContextOption
            {
                CommandString = commandString,
                QueryString = queryString
            };
            IoCContainer.Register(Configuration);//注册配置
            IoCContainer.Register(dbContextOption);//注册数据库配置信息
            IoCContainer.Register(typeof(DapperContext));
            IoCContainer.Register(typeof(EntityFrameworkContext));
            IoCContainer.Register(typeof(DapperRepositoryBase<>).Assembly, "QueryRepository");//注册仓储
            IoCContainer.Register(typeof(EntityFrameworkRepositoryBase<>).Assembly, "CommandRepository");//注册仓储
            IoCContainer.Register(typeof(EntityFrameworkRepositoryBase<>), typeof(IEntityFrameworkCommandRepository<>));
            IoCContainer.Register(typeof(DapperRepositoryBase<>), typeof(IDapperQueryRepository<>));
            IoCContainer.Register(typeof(BaseService).Assembly, "Service");
            return IoCContainer.Build(services);
        }
    }

    //Code First
    public class DbContextFactory : IDesignTimeDbContextFactory<EntityFrameworkContext>
    {
        public EntityFrameworkContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();
            var builder = new DbContextOptionsBuilder<EntityFrameworkContext>();
            var commandString = configuration.GetConnectionString("CommandDB");
            builder.UseSqlServer(commandString);
            return new EntityFrameworkContext(builder.Options);
        }
    }
}
