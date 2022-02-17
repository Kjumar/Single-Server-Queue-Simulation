using System;
using UnityEngine;

/// <summary>
///   <para>Show this field on the editor for debugging purposes</para>
///   <para>
///     The field itself should be serialized for this to work. Editing when not in play mode is disabled to prevent
///     changing the saved value.
///   </para>
/// </summary>
[AttributeUsage(AttributeTargets.Field)]
public class ShowAttribute : PropertyAttribute
{
    /// <summary>
    ///   <para>When this field should be shown and if it is editable</para>
    ///   <para>Defaults to OnlyOnPlay.</para>
    /// </summary>
    public readonly ShowMode mode;

    public bool IsOnlyOnPlay => mode == ShowMode.OnlyOnPlay || mode == ShowMode.OnlyOnPlayEditable;

    public bool IsEditable => mode == ShowMode.OnlyOnPlayEditable || mode == ShowMode.AlwaysAndPlayEditable;

    public bool ShouldShowValue => Application.isPlaying || !IsOnlyOnPlay;

    public ShowAttribute(ShowMode mode = ShowMode.OnlyOnPlay)
    {
        this.mode = mode;
    }
}

/// <summary>
///   When this field should be shown and if it is editable
/// </summary>
public enum ShowMode
{
    /// <summary>
    ///   Only show this field when the game is being played
    /// </summary>
    OnlyOnPlay,

    /// <summary>
    ///   Only show this field when the game is being played and enable editing
    /// </summary>
    OnlyOnPlayEditable,

    /// <summary>
    ///   Always show this field
    /// </summary>
    Always,

    /// <summary>
    ///   Always show this field and enable editing when the game is being played
    /// </summary>
    AlwaysAndPlayEditable,
}
