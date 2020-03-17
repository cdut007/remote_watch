using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SiMay.Basic;
using SiMay.ServiceCore.Attributes;

namespace SiMay.ServiceCore.Extensions
{
    public static class ServiceTypeExtension
    {
        public static string[] GetServiceKey(this Type type)
        {
            var attrs = type.GetCustomAttributes(typeof(ServiceKeyAttribute), true).Cast<ServiceKeyAttribute>();
            return attrs.Select(c => c.Key).ToArray();
        }
    }
}
