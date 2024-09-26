using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

public class RogueUpgradeEditor : EditorWindow
{
    // ListView is kept for easy reference.
    private ListView _listView;

    // List of CharacterInfo items, bound to the ListView.
    private List<RogueUpgrade.RogueStat> _items;
    [MenuItem("Window/ListView Custom Item")]
    public static void OpenWindow()
    {
        GetWindow<RogueUpgradeEditor>().Show();
    }

    private void OnEnable()
    {
        var rogueStatKeys = Enum.GetNames(typeof(RogueUpgrade.RogueStatKey));
        _items = new List<RogueUpgrade.RogueStat>(rogueStatKeys.Length);

        foreach (var key in Enum.GetNames(typeof(RogueUpgrade.RogueStatKey)))
        {
            _items.Add(
                new RogueUpgrade.RogueStat((RogueUpgrade.RogueStatKey)Enum.Parse(typeof(RogueUpgrade.RogueStatKey), key), 0));
        }



        // The ListView calls this to add visible items to the scroller.
        Func<VisualElement> makeItem = () =>
        {
            var rogueStatVisualElement = new RogueStat();
            var intField = rogueStatVisualElement.Q<IntegerField>(name: "Value");
            intField.RegisterValueChangedCallback(evt =>
            {

                var i = (int)intField.userData;


            });
            return rogueStatVisualElement;
        };

        // The ListView calls this if a new item becomes visible when the item first appears on the screen, 
        // when a user scrolls, or when the dimensions of the scroller are changed.
        Action<VisualElement, int> bindItem = (e, i) => BindItem(e as RogueStat, i);

        // Height used by the ListView to determine the total height of items in the list.
        int itemHeight = 55;

        // Use the constructor with initial values to create the ListView.
        _listView = new ListView(_items, itemHeight, makeItem, bindItem);
        _listView.reorderable = false;
        _listView.style.flexGrow = 1f; // Fills the window, at least until the toggle below.
        _listView.showBorder = true;
        rootVisualElement.Add(_listView);

        // Add a toggle to switch the reorderable property of the ListView.
        var reorderToggle = new Toggle("Reorderable");
        reorderToggle.style.marginTop = 10f;
        reorderToggle.value = false;
        reorderToggle.RegisterValueChangedCallback(evt => _listView.reorderable = evt.newValue);
        rootVisualElement.Add(reorderToggle);
    }


    // Bind the data (characterInfo) to the display (elem).
    private void BindItem(RogueStat elem, int i)
    {
        var label = elem.Q<Label>(name: "KeyLabel");
        var slider = elem.Q<IntegerField>(name: "Value");
        slider.userData = i;
        RogueUpgrade.RogueStat stat = _items[i];
        label.text = stat.Key.ToString();
    }


    // This class inherits from VisualElement to display and modify data to and from a CharacterInfo.
    public class RogueStat : VisualElement
    {
        // Use Constructor when the ListView uses makeItem and returns a VisualElement to be 
        // bound to a CharacterInfo data class.
        public RogueStat()
        {
            var root = new VisualElement();

            // The code below to style the ListView is for demo purpose. It's better to use a USS file
            // to style a visual element. 
            root.style.paddingTop = 3f;
            root.style.paddingRight = 0f;
            root.style.paddingBottom = 15f;
            root.style.paddingLeft = 3f;
            root.style.borderBottomColor = Color.gray;
            root.style.borderBottomWidth = 1f;
            var nameLabel = new Label() { name = "KeyLabel" };
            nameLabel.style.fontSize = 14f;


            Add(root);
        }
    }

}