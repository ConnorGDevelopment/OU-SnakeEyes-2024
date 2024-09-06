using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRogueUpgrade {
    public bool Unique { get; set; }
    public string Name { get; set; }
    public int Damage { get; set; }
    public int Speed { get; set; }
    public int Projectiles { get; set; }
    public int Spread { get; set; }
}

[CreateAssetMenu(menuName ="RogueUpgrade",order =1)]
public class RogueUpgrade : ScriptableObject, IRogueUpgrade
{
    public bool Unique { get; set; }
    public string Name { get; set; }
    public int Damage { get; set; }
    public int Speed { get; set; }
    public int Projectiles { get; set; }
    public int Spread { get; set; }

}
