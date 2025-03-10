using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;
using System.Text.Json.Serialization;

namespace MAVF.API.Device.Driver
{
	public static class DriverRegistry
	{
		private static readonly Dictionary<string, Type> deviceDriverToType = new Dictionary<string, Type>();

		static DriverRegistry()
		{
			// For each type that is castable to IDevice 
			foreach (var type in AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes()).Where(type => typeof(IDriver).IsAssignableFrom(type) && !type.IsAbstract))
			{
				var attribute = (DriverAttribute?)Attribute.GetCustomAttribute(type, typeof(DriverAttribute), false);
				// True for types that have the DeviceAttribute
				if (attribute != null)
				{
					if (TypeHasValidConstructor(type))
					{
						Debug.Print($"FOUND VALID '{attribute.driver}' DRIVER DEFINTION");
						deviceDriverToType[attribute.driver] = type;
					}
					else
					{
						Debug.Print($"FOUND INVAILD '{attribute.driver}' DRIVER DEFINTION");
					}
				}
			}
		}

		// This is probably not necessary, but JSON deserialization sometimes behaves weirdly with contructors
		private static bool TypeHasValidConstructor(Type type)
		{
			return type.GetConstructors().Any(constructor => constructor.GetParameters().Length == 0 || constructor.GetCustomAttributes(typeof(JsonConstructorAttribute)).Count() == 1);
		}

		public static bool TryGet(string driver, [MaybeNullWhen(false)] out Type o)
		{
			return deviceDriverToType.TryGetValue(driver, out o);
		}
	}
}
