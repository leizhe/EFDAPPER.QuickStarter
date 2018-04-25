using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using ED.Application.ServiceImp;
using ED.Common;
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
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;
using Zxw.Framework.NetCore.Filters;

namespace ED.WebAPI
{
    public class Startup
    {

        public static ILoggerRepository repository { get; set; }
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            //初始化log4net
            repository = LogManager.CreateRepository("NETCoreRepository");
            Log4NetHelper.SetConfig(repository, "log4net.config");
        }
        
        public IConfiguration Configuration { get; }

        //public IContainer ApplicationContainer { get; private set; }

  

        //public IServiceProvider ConfigureServices(IServiceCollection services)
        // This method gets called by the runtime. Use this method to add services to the container.
        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            //services.AddDbContext<EntityFrameworkContext>(options =>
            //    options.UseSqlServer(Configuration.GetConnectionString("CommandDB")));
            //Global.CommandDB = Configuration.GetConnectionString("CommandDB");
            //Global.QueryDB = Configuration.GetConnectionString("QueryDB");
            //services.a
            //services.AddMvc().AddControllersAsServices(); 

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

            //// Create the container builder.
            //var builder = new ContainerBuilder();

            ////builder.RegisterApiControllers(Assembly.GetExecutingAssembly());

         
            //builder.Populate(services);
            
            //this.ApplicationContainer = builder.Build();

            //// Create the IServiceProvider based on the container.
            //return new AutofacServiceProvider(this.ApplicationContainer);
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
            IoCContainer.Register(typeof(DapperContext));//注册EF上下文
            IoCContainer.Register(typeof(EntityFrameworkContext));//注册EF上下文

            //var sdsa = Assembly.GetExecutingAssembly();
            //IoCContainer.Register(Assembly.GetExecutingAssembly());
            //(typeof(BaseService).Assembly)

            IoCContainer.Register(typeof(DapperRepositoryBase<>).Assembly, "QueryRepository");//注册仓储
            IoCContainer.Register(typeof(EntityFrameworkRepositoryBase<>).Assembly, "CommandRepository");//注册仓储
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
