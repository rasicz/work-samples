using Magic.MO;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Magic
{
    public class MagicEngine
    {
        public static List<MagicObject> magicObjects = new List<MagicObject>();

        public static readonly Dictionary<string, float> manaCost = new Dictionary<string, float>()
        {
            { "scan", 0.2F },
            { "distance", 0.2F }
        };
    }
}
