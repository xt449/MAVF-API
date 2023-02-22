using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace MILAV.API.Device
{
    public class DeviceRegistry
    {
        private static readonly Dictionary<(DeviceType, string), Type> deviceTypeAndIdToType = new Dictionary<(DeviceType, string), Type>();

        /// <summary>
        /// Slow
        /// </summary>
        public static void Initialize()
        {
            // Only continue if there are no entries already in the registry
            if (deviceTypeAndIdToType.Count > 0)
            {
                return;
            }

            // For each type that is castable to IDevice 
            foreach (var type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes()).Where(type => typeof(IDevice).IsAssignableFrom(type) && !type.IsAbstract))
            {
                var attribute = (DeviceAttribute)Attribute.GetCustomAttribute(type, typeof(DeviceAttribute));
                // True for types that have the DeviceAttribute
                if (attribute != null)
                {
                    // This is probably not necessary, but JSON deserialization sometimes behaves weirdly with contructors
                    if (type.GetConstructor(new Type[0]) != null)
                    {
                        Debug.Print("FOUND '{0}' DEVICE DEFINTION FOR ID: '{1}'", attribute.type, attribute.driver);
                        deviceTypeAndIdToType[(attribute.type, attribute.driver)] = type;
                    }
                }
            }
        }

        public static bool TryGet(DeviceType type, string id, out Type o)
        {
            return deviceTypeAndIdToType.TryGetValue((type, id), out o);
        }
    }
}
