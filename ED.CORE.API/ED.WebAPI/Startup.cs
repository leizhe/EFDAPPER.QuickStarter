using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Autofac;
using Autofac.Extensions.DependencyInjection;
using ED.Application.ServiceImp;
using ED.Common;
using ED.Common.Helpers;
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
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Swashbuckle.AspNetCore.Swagger;

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

        //private IServiceProvider RegisterAutofac(IServiceCollection services)
        //{
        //    var assembly = this.GetType().GetTypeInfo().Assembly;
        //    var builder = new ContainerBuilder();
        //    var manager = new ApplicationPartManager();

        //    manager.ApplicationParts.Add(new AssemblyPart(assembly));
        //    manager.FeatureProviders.Add(new ControllerFeatureProvider());

        //    var feature = new ControllerFeature();

        //    manager.PopulateFeature(feature);




        //    builder.RegisterType<ApplicationPartManager>().AsSelf().SingleInstance();
        //    builder.RegisterTypes(feature.Controllers.Select(ti => ti.AsType()).ToArray()).PropertiesAutowired();

        //    //builder.RegisterAssemblyTypes(typeof(DapperRepositoryBase<>).Assembly)
        //    //    .Where(type => type.Name.EndsWith("QueryRepository")).SingleInstance()
        //    //    .AsImplementedInterfaces();

        //    //builder.RegisterAssemblyTypes(typeof(EntityFrameworkRepositoryBase<>).Assembly)
        //    //    .Where(type => type.Name.EndsWith("CommandRepository")).SingleInstance()
        //    //    .AsImplementedInterfaces();

        //    //builder.RegisterAssemblyTypes(typeof(BaseService).Assembly)
        //    //    .Where(type => type.Name.EndsWith("Service")).SingleInstance()
        //    //    .AsImplementedInterfaces();

        //    //builder.RegisterGeneric(typeof(EntityFrameworkRepositoryBase<>))
        //    //    .As(typeof(IEntityFrameworkCommandRepository<>)).AsImplementedInterfaces();

        //    //builder.RegisterGeneric(typeof(DapperRepositoryBase<>))
        //    //    .As(typeof(IDapperQueryRepository<>)).AsImplementedInterfaces();

        //    // Register dependencies, populate the services from
        //    // the collection, and build the container. If you want
        //    // to dispose of the container at the end of the app,
        //    // be sure to keep a reference to it as a property or field.
        //    //builder.RegisterType<MyType>().As<IMyType>();


        //    builder.Populate(services);



        //    //builder.RegisterType<AopInterceptor>();


        //    //builder.RegisterAssemblyTypes(assembly)
        //    //    .Where(type =>
        //    //        typeof(IDependency).IsAssignableFrom(type) && !type.GetTypeInfo().IsAbstract)
        //    //    .AsImplementedInterfaces()
        //    //    .InstancePerLifetimeScope()
        //    //    .EnableInterfaceInterceptors().InterceptedBy(typeof(AopInterceptor));


        //    this.ApplicationContainer = builder.Build();

        //    return new AutofacServiceProvider(this.ApplicationContainer);
        //}


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
            var connectionString = Configuration.GetConnectionString("CommandDB");
            var dbContextOption = new DbContextOption
            {
                ConnectionString = connectionString,
                ModelAssemblyName = "ED.Models.Command"
            };
            IoCContainer.Register(Configuration);//注册配置
            IoCContainer.Register(dbContextOption);//注册数据库配置信息
            IoCContainer.Register(typeof(EntityFrameworkContext));//注册EF上下文


          

            IoCContainer.Register(typeof(DapperRepositoryBase<>).Assembly);//注册仓储

            IoCContainer.Register(typeof(EntityFrameworkRepositoryBase<>).Assembly);//注册仓储
            //IoCContainer.Register("ED.Repositories.Query", "ED.Repositories.Core.Query");//注册仓储
            //IoCContainer.Register("ED.Repositories.Command", "ED.Repositories.Core.Command");//注册仓储
            //IoCContainer.Register("ED.Application.ServiceImp", "ED.Application.ServiceContract");//注册service
            return IoCContainer.Build(services);
        }
    }
}
