using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Autofac;
using Autofac.Integration.Mvc;
using TechStudioTest.DataAccess;

namespace TechStudioTest
{
    public class DiModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            base.Load(builder);

            builder.RegisterControllers(typeof(MvcApplication).Assembly);            
            builder.RegisterType<CompanyContext>().InstancePerRequest();
            builder.RegisterType<EmployeeRepository>().As<IEmployeeRepository>();
        }
    }
}