using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace UIToolkitExamples
{
    [CustomEditor(typeof(RogueUpgradeEditorComponent))]
    public class RogueUpgradeEditorComponentEditor : Editor
    {
        [SerializeField]
        VisualTreeAsset m_Uxml;

        public override VisualElement CreateInspectorGUI()
        {
            var parent = new VisualElement();

            m_Uxml?.CloneTree(parent);

            return parent;
        }
    }
}