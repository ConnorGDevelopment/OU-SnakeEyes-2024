using Rogue;
using System.Collections.Generic;
using UnityEngine;

namespace Weapons
{
    [CreateAssetMenu(menuName = "GunData")]
    public class GunData : UpgradeData
    {
        public void Awake()
        {
            Stats.Add(new(StatKey.Speed, floatValue: 60f));
        }

    }

}
