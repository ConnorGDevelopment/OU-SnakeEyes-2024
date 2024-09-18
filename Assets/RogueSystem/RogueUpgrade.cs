using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RogueUpgrade")]
public class RogueUpgrade : ScriptableObject
{
    public enum RogueStat
    {
        Damage,
        Speed,
        Projectiles,
        Spread
    }

    public string Name;
    // If Unique == true, duplicates of this upgrade don't stack
    public bool Unique;
    // Readonly dict means elements can be added/removed but the dict can't be replaced
    public Dictionary<RogueStat, int> Stats = new();
}