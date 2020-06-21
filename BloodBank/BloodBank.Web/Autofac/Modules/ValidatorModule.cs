using Autofac;
using BloodBank.Logics.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BloodBank.Web.Autofac.Modules
{
    public class ValidatorModule:Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(typeof(IMyValidator).Assembly)
                .Where(t => typeof(IMyValidator).IsAssignableFrom(t))
                .AsImplementedInterfaces();
        }
    }
}
