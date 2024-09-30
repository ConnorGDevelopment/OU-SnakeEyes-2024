using Rogue;
using System.Collections.Generic;

namespace Weapons
{
    public class GunData : UpgradeData
    {
        public new List<Stat> Stats = new()
        {
            new Stat(StatKey.Speed, floatValue: 60f)
        };

    }

}
