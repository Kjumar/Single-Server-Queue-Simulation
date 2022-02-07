using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer
{
    public float interarrivalTime { get; private set; }
    public float serviceTime { get; private set; }

    public Customer(float interarrivalTime, float serviceTime)
    {
        this.interarrivalTime = interarrivalTime;
        this.serviceTime = serviceTime;

        Debug.Log("Interarrival Time: " + interarrivalTime + "\nService Time: " + serviceTime);
    }
}
