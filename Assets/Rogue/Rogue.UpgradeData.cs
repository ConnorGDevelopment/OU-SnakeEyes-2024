using System.Collections.Generic;
using UnityEngine;

namespace Rogue
{
  

    [CreateAssetMenu(menuName = "RogueUpgrade")]
    public class UpgradeData : ScriptableObject
    {
        public string Name;
        // If Unique == true, duplicates of this upgrade don't stack
        public bool Unique;

        public List<Stat> Stats = new();
    }
}
