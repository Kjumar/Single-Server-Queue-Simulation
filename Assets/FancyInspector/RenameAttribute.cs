using System;
using UnityEngine;

/// <summary>
///   Show the name of this field to something else
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public class RenameAttribute : PropertyAttribute
{
    /// <summary>
    ///   The name to display on the editor inspector
    /// </summary>
    public readonly string DisplayName;

    public RenameAttribute(string displayName)
    {
        DisplayName = displayName;
    }
}
