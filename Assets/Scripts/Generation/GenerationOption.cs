using UnityEngine;
using Random = System.Random;

public abstract class GenerationOption : ScriptableObject
{
    public abstract string GetName();

    public abstract float Generate(Random random, float mean);

#if UNITY_EDITOR
    public virtual void OnPropertyGUI(Rect position) {}

    public virtual float GetGUIHeight() => 0.0f;
#endif
}
