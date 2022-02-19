using UnityEngine;
using Random = System.Random;

[CreateAssetMenu(fileName = "NormalGeneration", menuName = "Generation/Normal")]
public class NormalDistribution : GenerationOption
{
    public float standardDeviation = 1.0f;

    public override string GetName()
    {
        return "Normal Distribution";
    }

    public override float Generate(Random random, float mean)
    {
        // using count = 12 specifically eliminates the square root from the formula and simplifies it
        // sqrt(12/n) * (fx(x:n) - n/2), n-> infinity
        // => 1 * (fx(12:n) - 6)
        const int count = 12;
        double r = 0;

        for (int i = 0; i < count; i++)
        {
            r += random.NextDouble() - 0.5f;
        }

        float randNormal = (float)(mean + standardDeviation * r);

        return Mathf.Max(0.0f, randNormal);
    }

#if UNITY_EDITOR
    public override void OnPropertyGUI(Rect position)
    {
        using var changed = new UnityEditor.EditorGUI.ChangeCheckScope();

        float stdDev = UnityEditor.EditorGUI.FloatField(position, "Standard Deviation", standardDeviation);

        if (changed.changed)
        {
            standardDeviation = Mathf.Max(0.0f, stdDev);
        }
    }

    public override float GetGUIHeight()
    {
        return UnityEditor.EditorGUIUtility.singleLineHeight;
    }
#endif
}
