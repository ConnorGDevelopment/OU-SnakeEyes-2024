
using System;

namespace Rogue
{
    // This is an enum, basically confines Stats to a set of known values rather than strings
    public enum StatKey
    {
        None,
        Damage,
        Speed,
        FireRate,
    }
    // This is just an key/pair obj so you can check in lists what a Stat is, has some mojo for accepting a string instead of key
    [System.Serializable]
    public class Stat
    {
        public StatKey Key;
        public int IntValue;
        public float FloatValue;

        // This uses optional params, basically they have a default so you don't need to write empty values every time you make one
        public Stat(StatKey key, int value = 0, float floatValue = 0)
        {
            Key = key;
            IntValue = value;
            FloatValue = floatValue;
        }

        public Stat(string key, int value = 0, float floatValue = 0)
        {
            if (Enum.TryParse(key, true, out StatKey validKey))
            {
                Key = validKey;
            }
            else
            {
                Key = StatKey.None;
            }

            IntValue = value;
            FloatValue = floatValue;

        }
    }
}
