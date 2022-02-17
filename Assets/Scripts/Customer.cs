using UnityEngine;

[DisallowMultipleComponent]
public class Customer : MonoBehaviour
{
    public int id;
    public float interarrivalTime;
    public float serviceTime;

    public void SetData(CustomerData data)
    {
        id = data.id;
        interarrivalTime = data.interarrivalTime;
        serviceTime = data.serviceTime;
    }
}
