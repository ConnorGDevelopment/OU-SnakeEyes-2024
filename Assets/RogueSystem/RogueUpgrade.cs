using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IRogueUpgrade {
    public bool Unique { get; }
    public string Name { get; }
    public int Damage { get; }
    public int Speed { get; }
    public int Projectiles { get; }
    public int Spread { get; }
}

[CreateAssetMenu(menuName ="RogueUpgrade",order =1)]
public class RogueUpgrade : ScriptableObject, IRogueUpgrade
{
    public bool Unique;
    public string Name;
    public int Damage;
    public int Speed;
    public int Projectiles;
    public int Spread;

}
