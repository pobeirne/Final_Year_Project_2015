using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using LuckyMe.CMS.WebAPI.Attributes;

namespace LuckyMe.CMS.WebAPI.Extensions
{
    public class Propertycontainer
    {
        public string Facebookfield { get; set; }
        public string Facebookparent { get; set; }
        public PropertyInfo FacebookMappedProperty { get; set; }
    }


    public static class DynamicExtension
    {
        public static T ToStatic<T>(object dynamicObject)
        {
            var entity = Activator.CreateInstance<T>();

            var properties = dynamicObject as IDictionary<string, object>;

            if (properties == null)
                return entity;
            var destFbMappingProperties = (from PropertyInfo property in entity.GetType().GetProperties()
                where property.GetCustomAttributes(typeof (FacebookMapping), true).Length > 0
                select property).ToList();

            var propertyLookup = (from pi in destFbMappingProperties
                from attribute in pi.GetCustomAttributes(typeof (FacebookMapping))
                let facebookMapAttribute = attribute as FacebookMapping
                where facebookMapAttribute != null
                select new Propertycontainer
                {
                    Facebookfield = facebookMapAttribute.GetName(),
                    Facebookparent = facebookMapAttribute.Parent,
                    FacebookMappedProperty = pi
                }).ToList();


            foreach (var entry in properties)
            {
                var entry1 = entry;
                var matchedResults =
                    propertyLookup.Where(x => x.Facebookparent == entry1.Key || x.Facebookfield == entry1.Key);

                foreach (var destinationPropertyInfo in matchedResults)
                {
                    object mappedValue = null;
                    if (entry.Value.GetType().Name == "JsonObject")
                    {
                        mappedValue = FindMatchingChildPropertiesRecursively(entry, destinationPropertyInfo);


                        if (mappedValue == null &&
                            destinationPropertyInfo.Facebookfield == entry.Key)
                            mappedValue = entry.Value;
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(destinationPropertyInfo.Facebookparent) &&
                            destinationPropertyInfo.Facebookfield == entry.Key)
                            mappedValue = entry.Value;
                    }


                    if (mappedValue == null) continue;
                    destinationPropertyInfo.FacebookMappedProperty.SetValue(entity,
                        destinationPropertyInfo.FacebookMappedProperty.PropertyType.Name == "DateTime"
                            ? DateTime.Parse(mappedValue.ToString())
                            : mappedValue, null);
                }
            }
            return entity;
        }

        private static object FindMatchingChildPropertiesRecursively(
            KeyValuePair<string, object> entry,
            Propertycontainer destinationPropertyInfo)
        {
            var childproperties = entry.Value as IDictionary<string, object>;


            var mappedValue = (from KeyValuePair<string, object> item in childproperties
                where item.Key == destinationPropertyInfo.Facebookfield
                select item.Value).FirstOrDefault();


            if (mappedValue != null) return mappedValue;
            if (childproperties == null) return null;
            foreach (var item in childproperties.Where(item => item.Value.GetType().Name == "JsonObject"))
            {
                mappedValue = FindMatchingChildPropertiesRecursively(item, destinationPropertyInfo);
            }

            return mappedValue;
        }
    }
}