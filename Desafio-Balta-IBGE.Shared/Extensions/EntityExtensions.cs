using Desafio_Balta_IBGE.Domain.Atributes;
using Desafio_Balta_IBGE.Shared.Entities;
using Desafio_Balta_IBGE.Shared.Exceptions;
using Desafio_Balta_IBGE.Shared.ValueObjects;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace Desafio_Balta_IBGE.Shared.Extensions
{
    public static class EntityExtensions
    {
        public static void CheckPropertiesIsNull<T>(this T obj) where T : Entity
        {
            var properties = typeof(T)
                .GetProperties()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(IfNullAttribute)))
                .ToList();

            foreach (var property in properties)
            {
                if (property.GetValue(obj) is null)
                {
                    var attribute = property.GetCustomAttribute(typeof(IfNullAttribute)) ?? throw new CustomAttributeNotDefineException($"Atributo para a propriedade não foi definido.");

                    var errorMessage = ((IfNullAttribute)attribute).GetErrorMessage();

                    InvalidParametersException.ThrowIfNull(property.Name, errorMessage);
                }
                else if (property.PropertyType.BaseType == typeof(ValueObject))
                {
                    var valueObjectProperty = property.GetValue(obj)!;

                    var thisAssembly = Assembly.GetExecutingAssembly();

                    //var assembly = thisAssembly.GetReferencedAssemblies().Where(x => x.Name.Contains("ValueObjectExtensions")).FirstOrDefault();
                    
                    //var namespaceName = thisAssembly.GetName().Name.ToString();


                    var method = thisAssembly.GetType("Desafio_Balta_IBGE.Shared.Extensions.ValueObjectExtensions")!.GetMethods().FirstOrDefault(x => x.Name.Equals("CheckPropertiesIsNull"));

                    var generic = method.MakeGenericMethod(valueObjectProperty.GetType());

                    generic.Invoke(obj, new object[] { valueObjectProperty });

                }
                else if (property.PropertyType.BaseType == typeof(Entity))
                {
                    var entityProperty = property.GetValue(obj)!;

                    Assembly thisAssembly = Assembly.GetExecutingAssembly();

                    var method = thisAssembly.GetType("Desafio_Balta_IBGE.Shared.Extensions.EntityExtensions")!.GetMethods().FirstOrDefault(x => x.Name.Equals("CheckPropertiesIsNull"));

                    var generic = method.MakeGenericMethod(entityProperty.GetType());

                    generic.Invoke(obj, new object[] { entityProperty });
                }
            }
        }
    }
}