using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace AuditApp.Data.Extensions
{
    internal static class ModelBuilderExtensions
    {
        public static ModelBuilder ApplyConfigurationFromAssembly(this ModelBuilder modelBuilder, Assembly assembly)
        {
            var configs = assembly
                .GetTypes()
                .Where(x => x.IsClass && !x.IsAbstract)
                .Select(x => (type: x, model: x.GetInterfaces()
                    .FirstOrDefault(t => t.IsGenericType && t.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>))
                    ?.GenericTypeArguments
                    .First()))
                .Where(x => x.model != null)
                .ToList();

            var method = typeof(ModelBuilder)
                .GetMethods()
                .FirstOrDefault(x => x.Name == nameof(ModelBuilder.ApplyConfiguration) &&
                                     x.GetParameters().FirstOrDefault()?.ParameterType.GetGenericTypeDefinition() == typeof(IEntityTypeConfiguration<>));
            foreach (var (config, model) in configs)
            {
                var instance = Activator.CreateInstance(config);
                var genericMethod = method.MakeGenericMethod(model);
                genericMethod.Invoke(modelBuilder, new[] { instance });
            }

            return modelBuilder;
        }
    }
}
