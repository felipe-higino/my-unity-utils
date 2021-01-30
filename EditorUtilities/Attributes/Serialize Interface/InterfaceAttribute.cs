using UnityEngine;
using System;

[AttributeUsage(AttributeTargets.Field)]
public class InterfaceAttribute : PropertyAttribute
{
    public Type type;
    public bool allowSceneObjects = true;

    public InterfaceAttribute(Type type)
    {
        if (type == null) throw new ArgumentNullException("type is null");
        this.type = type;
    }
}
