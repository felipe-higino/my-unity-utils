#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

[CustomPropertyDrawer(typeof(InterfaceAttribute))]
public class InterfaceDrawer : PropertyDrawer
{
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        if (property.propertyType != SerializedPropertyType.ObjectReference) return;

        var attrib = this.attribute as InterfaceAttribute;
        if (attrib == null) return;

        EditorGUI.BeginChangeCheck();
        Object obj = EditorGUI.ObjectField(
            position,
            label,
            property.objectReferenceValue,
            typeof(Object),
            attrib.allowSceneObjects
        );

        if (EditorGUI.EndChangeCheck())
        {
            var name = obj.name;
            if (obj != null)
            {
                var type = obj.GetType();
                if (!attrib.type.IsAssignableFrom(type))
                {
                    GameObject boxing = null;
                    if (obj is GameObject)
                    {
                        boxing = (obj as GameObject);
                    }
                    else if (obj is Component)
                    {
                        obj = (obj as Component).gameObject;
                    }

                    var components = boxing?.GetComponents(attrib.type);
                    var numberOfComponents = components.Length;
                    if (numberOfComponents <= 0)
                    {
                        obj = null;
                    }
                    else if (numberOfComponents == 1)
                    {
                        obj = components[0];
                    }
                    else
                    {
                        obj = components[0];
                        Debug.LogWarning($"{numberOfComponents} components founded, first one automatically selected");
                    }
                }
            }
            if (null == obj)
                Debug.LogError($"[{name}] Interface implementation not found");

            property.objectReferenceValue = obj;
        }
    }
}

#endif