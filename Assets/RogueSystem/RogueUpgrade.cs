using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RogueUpgrade: ScriptableObject
{
    // If Unique == true, duplicates of this upgrade don't stack
    public bool Unique { get; set; }
    public string Name { get; set; }
    // The following are TBD, should match weapon stats
    public int Damage { get; set; }
    public int Speed { get; set; }
    public int Projectiles { get; set; }
    public int Spread { get; set; }
    // Indexer Function
    // Basically assigns a function to this.propertynamegoeshere, if it exists and is an integer it returns that value, otherwise it returns 0
    // This lets us index access properties on objects of this class as if it were an array like "this[propertynamegoeshere]"
    public int this[string key]
    {
        get
        {
            if (this[key].GetType() == typeof(int))
            {
                return this[key];
            }
            else
            {
                // If this weren't a getter I could use int? which basically means this value is either int or undefined
                return 0;
            }
        }
    }
}