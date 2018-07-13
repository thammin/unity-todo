using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using POC;

[CustomEditor(typeof(POCBind2))]
public class POCBind2Editor : Editor
{
    public override void OnInspectorGUI()
    {
        var obj = (POCBind2)serializedObject.targetObject;
        var source = serializedObject.FindProperty("_src");

        EditorGUILayout.PropertyField(source);

        if (obj.Src)
        {
            var propertyDesc = ComponentsProperties(obj.Src, obj.SrcProperty);

            if (propertyDesc != null)
            {
                obj.Src = propertyDesc.component;
                obj.SrcProperty = propertyDesc.name;
            }
        }

        serializedObject.ApplyModifiedProperties();
    }

    private PropertyDescription2 ComponentsProperties(Component component, string selectedProperty)
    {
        var propertyNames = new List<string>();
        var properties = new List<PropertyDescription2>();
        var selectedIndex = -1;

        foreach (var c in component.GetComponents<Component>())
        {
            var type = c.GetType();
            // is sub type
            if (type.IsSubclassOf(typeof(POCComponent)))
            {
                var dataInfo = type.GetProperty("DTO");

                foreach (var p in dataInfo.PropertyType.GetProperties())
                {
                    if (p.Name == selectedProperty)
                    {
                        selectedIndex = propertyNames.Count;
                    }

                    propertyNames.Add(c.name + "/" + p.Name);
                    properties.Add(new PropertyDescription2(p.Name, c));
                }
            }
        }

        var newSelected = EditorGUILayout.Popup("Property", selectedIndex, propertyNames.ToArray());

        if (newSelected != selectedIndex)
        {
            return properties[newSelected];
        }

        return null;
    }

    private class PropertyDescription2
    {
        public PropertyDescription2(string name, Component component)
        {
            this.name = name;
            this.component = component;
        }

        public string name { get; set; }
        public Component component { get; set; }
    }
}