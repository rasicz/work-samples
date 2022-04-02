using Magic.MO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicLibraryTests
{
    class TestExternalMethods : IMagicExternalMethods
    {
        public void Debug(string message)
        {
            throw new NotImplementedException();
        }

        public float Distance(float angle)
        {
            throw new NotImplementedException();
        }

        public int Scan(float angle, float distance)
        {
            throw new NotImplementedException();
        }
    }
}
