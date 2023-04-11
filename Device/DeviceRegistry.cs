﻿using System.Diagnostics;

namespace MILAV.API.Device
{
    public class DeviceRegistry
    {
        private static readonly Dictionary<string, Type> deviceDriverToType = new Dictionary<string, Type>();

        /// <summary>
        /// Slow
        /// </summary>
        public static void Initialize()
        {
            // Only continue if there are no entries already in the registry
            if (deviceDriverToType.Count > 0)
            {
                return;
            }

            // For each type that is castable to IDevice 
            foreach (var type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes()).Where(type => typeof(AbstractDevice).IsAssignableFrom(type) && !type.IsAbstract))
            {
                var attribute = (DeviceAttribute?)Attribute.GetCustomAttribute(type, typeof(DeviceAttribute));
                // True for types that have the DeviceAttribute
                if (attribute != null)
                {
                    // This is probably not necessary, but JSON deserialization sometimes behaves weirdly with contructors
                    if (type.GetConstructor(Array.Empty<Type>()) != null)
                    {
                        Debug.Print("FOUND '{0}' DEVICE DEFINTION FOR ID: '{1}'", attribute.driver);
                        deviceDriverToType[attribute.driver] = type;
                    }
                }
            }
        }

        public static bool TryGet(string? id, out Type? o)
        {
            if (id == null)
            {
                o = null;
                return false;
            }

            return deviceDriverToType.TryGetValue(id, out o);
        }
    }
}
