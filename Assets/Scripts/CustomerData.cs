using System;

[Serializable]
public struct CustomerData
{
    /// <summary>
    /// The id number of the customer. The value of 0 or smaller represents an invalid/empty data.
    /// </summary>
    public int id;
    public float interarrivalTime;
    public float serviceTime;

    public bool IsValid => id > 0;

    public CustomerData(int id, float interarrivalTime, float serviceTime)
    {
        this.id = id;
        this.interarrivalTime = interarrivalTime;
        this.serviceTime = serviceTime;
    }
}