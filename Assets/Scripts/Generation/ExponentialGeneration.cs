using UnityEngine;
using Random = System.Random;

[CreateAssetMenu(fileName = "ExponentialGeneration", menuName = "Generation/Exponential")]
public class ExponentialGeneration : GenerationOption
{
    public override string GetName()
    {
        return "Exponential Distribution";
    }

    public override float Generate(Random random, float mean)
    {
        return -1.0f * Mathf.Log(1.0f - (float) random.NextDouble()) * mean;
    }
}
