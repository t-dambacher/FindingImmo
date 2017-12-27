using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace FindingImmo.Core.Domain.DataAccess.Mapping
{
    public static class ModelBuilderExtensions
    {
        private static IEnumerable<Type> GetMappingTypes(this Assembly assembly)
        {
            Type mappingInterface = typeof(IEntityTypeConfiguration<>), mappingClass = typeof(EntityMapping);
            return assembly.GetTypes().Where(x => !x.IsAbstract && mappingClass.IsAssignableFrom(x) && x.GetInterfaces().Any(y => y.GetTypeInfo().IsGenericType && y.GetGenericTypeDefinition() == mappingInterface));
        }

        public static void ApplyConfigurationFromAssembly(this ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationFromAssembly(Assembly.GetExecutingAssembly());
        }

        public static void ApplyConfigurationFromAssembly(this ModelBuilder modelBuilder, Assembly assembly)
        {
            IEnumerable<Type> mappingTypes = assembly.GetMappingTypes();
            foreach (EntityMapping mapping in mappingTypes.Select(Activator.CreateInstance).Cast<EntityMapping>())
            {
                mapping.Configure(modelBuilder);
            }
        }
    }
}
