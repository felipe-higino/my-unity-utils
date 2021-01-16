using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Type Wrapper for <see cref="Tag{E}"/>.<br/>
/// Use it as a [SerializeField] to drag and drop Tags in inspector.<br/>
/// Tags are compared with "==" operators
/// </summary>
public abstract class Tag : MonoBehaviour, IEquatable<Tag>
{
    protected abstract Enum BoxedType { get; }

    internal Tag() { }//cannot be inherited outside this assembly

    public bool Equals(Tag other)
    {
        return this.Equals(other);
    }

    public override bool Equals(object obj)
    {
        return base.Equals(obj);
    }

    public override int GetHashCode()
    {
        return base.GetHashCode();
    }

    public static bool operator ==(Tag Tag1, Tag Tag2)
    {
        return Tag1.CompareTo(Tag2);
    }

    public static bool operator !=(Tag Tag1, Tag Tag2)
    {
        return !(Tag1.CompareTo(Tag2));
    }

    private bool CompareTo(Tag otherTag)
    {
        return BoxedType.Equals(otherTag.BoxedType);
    }
}

/// <summary>
/// Inherit it to create a new Tag Type.
/// This class can be atatched to game objects to 
/// give them optimized tags.<br/>
/// </summary>
public abstract class Tag<E> : Tag where E : Enum
{
    [field: SerializeField]
    [field: Rename("Type (enum-powered)")]
    public E Type { get; set; }

    protected override Enum BoxedType => Type;
}