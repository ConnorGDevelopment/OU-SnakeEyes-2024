using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RogueUpgrade
{
    public bool Unique { get; set; }
    public string Name { get; set; }
    public int Damage { get; set; }
    public int Speed { get; set; }
    public int Projectiles { get; set; }
    public int Spread { get; set; }

    public int this[string key] {
        get
        {
            if (this[key].GetType() == typeof(int)) {
                return this[key];
            } else
            {
                return 0;
            }
        }
    }
}


public class RogueUpgradeManager : MonoBehaviour
{
    public readonly List<RogueUpgrade> Upgrades = new();

    public RogueUpgrade Summary
    {
        get {
            RogueUpgrade _summary = new();

            foreach (var key in Upgrades.GetType().GetProperties() ) { 
                if(key.PropertyType == typeof(int)) {

                    Upgrades.Select((upgrade) => upgrade[key.ToString()]).Sum();
                    
                };
            };

            return _summary;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
