using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName ="RogueUpgrade",order =1)]
public class RogueUpgrade : ScriptableObject
{
    public string Name;
    public int Damage;
    public int Speed;
    public int Projectiles;
    public int Spread;
    public bool Unique;
}
