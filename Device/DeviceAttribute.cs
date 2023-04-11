namespace MILAV.API.Device
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = false, Inherited = false)]
    public class DeviceAttribute : Attribute
    {
        public readonly string driver;

        public DeviceAttribute(string driver)
        {
            this.driver = driver;
        }
    }
}
