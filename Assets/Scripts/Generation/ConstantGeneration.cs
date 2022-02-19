using UnityEngine;
using Random = System.Random;

[CreateAssetMenu(fileName = "ConstantGeneration", menuName = "Generation/Constant")]
public class ConstantGeneration : GenerationOption
{
    public override string GetName()
    {
        return "Constant";
    }

    public override float Generate(Random random, float mean)
    {
        return mean;
    }
}
