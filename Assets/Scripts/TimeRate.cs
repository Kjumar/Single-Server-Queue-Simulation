using UnityEngine;

[System.Serializable]
public class TimeRate : ISerializationCallbackReceiver
{
    [SerializeField] private int ratePerHour;
    private float meanTimeHour;
    private float meanTimeMinute;

    public int NumPerHour
    {
        get => ratePerHour;
        set
        {
            ratePerHour = value;
            ValidateState();
        }
    }

    public float MeanTimeHour => meanTimeHour;

    public float MeanTimeMinute => meanTimeMinute;

    public TimeRate()
    {
        ValidateState();
    }

    public TimeRate(int perHour)
    {
        NumPerHour = perHour;
    }

    public static implicit operator TimeRate(int perHour) => new TimeRate(perHour);

    public void OnBeforeSerialize()
    {
    }

    public void OnAfterDeserialize()
    {
        ValidateState();
    }

    private void ValidateState()
    {
        ratePerHour = Mathf.Max(1, ratePerHour);
        meanTimeHour = 1.0f / ratePerHour;
        meanTimeMinute = 60.0f / ratePerHour;
    }
}
