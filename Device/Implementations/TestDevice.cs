using MILAV.API.Connection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MILAV.API.Device.Implementations
{
    public class TestDevice : IDevice, IPowerControl
    {
        public string Room => "test";

        public string Id => "test";

        public string Name => "test";

        public IConnection Communication => null;

        public bool GetPower()
        {
            return false;
        }

        public void SetPower(bool state)
        {
            // nothing
        }
    }
}
