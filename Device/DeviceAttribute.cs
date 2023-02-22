using System;

namespace MILAV.API.Device
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class DeviceAttribute : Attribute
    {
        public readonly DeviceType type;
        public readonly string id;

        public DeviceAttribute(DeviceType type, string id)
        {
            this.type = type;
            this.id = id;
        }
    }
}
