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
            // Set Default Value, check if one present already
            Stats.Add(new(StatKey.Speed, floatValue: 60f));
        }

    }

}
