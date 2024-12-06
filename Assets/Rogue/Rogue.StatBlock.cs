using UnityEngine;

namespace Rogue
{
    [CreateAssetMenu(menuName = "RogueStatBlock")]
    public class StatBlock : ScriptableObject
    {
        public string Name;
        public StatDict Stats = new();
    }
}
