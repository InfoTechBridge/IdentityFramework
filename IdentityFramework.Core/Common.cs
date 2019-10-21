using System;
using System.Reflection;
using System.ComponentModel;

namespace IdentityFramework.Core
{
    class Common
    {
        public static string GetEnumDescription(Enum value)
        {
            var desc = Resx.AppResources.ResourceManager.GetString(value.GetType().Name + value.ToString());
            return desc ?? value.ToString();

            //FieldInfo fi = value.GetType().GetField(value.ToString());
            //DescriptionAttribute[] attributes = (DescriptionAttribute[])fi.GetCustomAttributes(typeof(DescriptionAttribute), false);
            //return (attributes.Length > 0) ? attributes[0].Description : value.ToString();

        }
        
    }
}
