using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

// The following are TBD, should match weapon stats

public enum RogueStat {
    Damage,
    Speed,
    Projectiles,
    Spread
}

public class RogueUpgrade: ScriptableObject
{
    public string Name { get; set; }
    // If Unique == true, duplicates of this upgrade don't stack
    public bool Unique { get; set; }
    public Dictionary<RogueStat,int> Stats { get; set; }
}