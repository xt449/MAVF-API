using System;

namespace MILAV.API.Device
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class DeviceAttribute : Attribute
    {
        public readonly DeviceType type;
        public readonly string driver;

        public DeviceAttribute(DeviceType type, string driver)
        {
            this.type = type;
            this.driver = driver;
        }
    }
}
