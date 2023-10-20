﻿using Desafio_Balta_IBGE.Domain.Atributes;
using Desafio_Balta_IBGE.Shared.Entities;
using Desafio_Balta_IBGE.Shared.Exceptions;
using Desafio_Balta_IBGE.Shared.ValueObjects;
using System.Reflection;

namespace Desafio_Balta_IBGE.Shared.Extensions
{
    public static class ValueObjectExtensions
    {
        public static void CheckPropertiesIsNull<T>(this T obj) where T : ValueObject
        {
            var properties = typeof(T)
                .GetProperties()
                .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(IfNullAttribute)))
                .ToList();

            #region Alteração
            //var properties = typeof(T)
            //    .GetProperties()
            //    .Where(x => x.CustomAttributes.Any(y => y.AttributeType == typeof(IfNullAttribute)) && x.GetValue(obj).Equals(null))
            //    .ToList();
            #endregion

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

                    var assembly = thisAssembly.GetReferencedAssemblies().Where(x => x.Name.Contains("ValueObjectExtensions")).FirstOrDefault();

                    string namespaceName = thisAssembly.GetName().Name.ToString().Replace("-", "_");

                    string name = $"{namespaceName}.Extensions.ValueObjectExtensions";

                    var method = thisAssembly.GetType(name)!.GetMethods().FirstOrDefault(x => x.Name.Equals("CheckPropertiesIsNull"));

                    var generic = method.MakeGenericMethod(valueObjectProperty.GetType());

                    generic.Invoke(obj, new object[] { valueObjectProperty });

                }
                else if (property.PropertyType.BaseType == typeof(Entity))
                {
                    var entityProperty = property.GetValue(obj)!;

                    var thisAssembly = Assembly.GetExecutingAssembly();

                    var assembly = thisAssembly.GetReferencedAssemblies().Where(x => x.Name.Contains("EntityExtensions")).FirstOrDefault();

                    string namespaceName = thisAssembly.GetName().Name.ToString().Replace("-", "_");

                    string name = $"{namespaceName}.Extensions.EntityExtensions";

                    var method = thisAssembly.GetType(name)!.GetMethods().FirstOrDefault(x => x.Name.Equals("CheckPropertiesIsNull"));

                    var generic = method.MakeGenericMethod(entityProperty.GetType());

                    generic.Invoke(obj, new object[] { entityProperty });
                }
            }
        }
    }
}