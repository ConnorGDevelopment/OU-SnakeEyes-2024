using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RogueUpgradeManager : MonoBehaviour
{
    public readonly List<RogueUpgrade> Upgrades = new();

    public RogueUpgrade Summary
    {
        get {
            // Create a blank RogueUpgrade so we can assign stats
            RogueUpgrade _summary = ScriptableObject.CreateInstance<RogueUpgrade>();

            // Upgrades is our list of upgrades (duh)
            // Upgrades.GetType() returns the type of the items in the list (RogueUpgrade)
            // Upgrades.GetType().GetProperties() returns the properties on the the type of the items in the list (RogueUpgrade)
            foreach (var key in Upgrades.GetType().GetProperties() ) { 
                // Check if the value of it is an integer
                if(key.PropertyType == typeof(int)) {
                    // Upgrades.Select() lets you run a function for each member of a list (or other iterable) and returns the output of whatever function you ran
                    
                    // The "=>" is called an arrow function and its just a handy shortcut 
                    // This could also be written as the following                    
                    // Upgrades.Select((upgrade) {
                    //  return upgrade[key.ToString()]
                    // }).Sum();

                    // key.ToString() is just a conversion from whatever weird type key.PropertyType spits out to a string, which we feed into the indexer function on RogueUpgrade
                    Upgrades.Select((upgrade) => upgrade[key.ToString()]).Sum();
                    
                };
            };

            return _summary;
        }
    }
}
