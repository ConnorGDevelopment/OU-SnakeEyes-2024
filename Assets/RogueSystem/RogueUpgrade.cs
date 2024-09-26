
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "RogueUpgrade")]
public class RogueUpgrade : ScriptableObject
{
    public enum RogueStatKey
    {
        Damage,
        Speed,
        Projectiles,
        Spread
    }

    [System.Serializable]
    public class RogueStat
    {
        public RogueStatKey Key;
        public int Value;

        public RogueStat(RogueStatKey key, int value)
        {
            Key = key;
            Value = value;
        }
    }

    public string Name;
    // If Unique == true, duplicates of this upgrade don't stack
    public bool Unique;

    [SerializeField]
    public List<RogueStat> Stats = new();
}

