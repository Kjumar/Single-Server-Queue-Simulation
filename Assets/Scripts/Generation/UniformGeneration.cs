using UnityEngine;
using Random = System.Random;

[CreateAssetMenu(fileName = "UniformGeneration", menuName = "Generation/Uniform")]
public class UniformGeneration : GenerationOption
{
    public override string GetName()
    {
        return "Uniform Distribution";
    }

    public override float Generate(Random random, float mean)
    {
        return (float) random.NextDouble() * mean * 2.0f;
    }
}
