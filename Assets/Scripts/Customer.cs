using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Customer
{
    public int id { get; private set; }
    public float interarrivalTime { get; private set; }
    public float serviceTime { get; private set; }

    public Customer(int id, float interarrivalTime, float serviceTime)
    {
        this.id = id;
        this.interarrivalTime = interarrivalTime;
        this.serviceTime = serviceTime;

        Debug.Log("Customer id: " + id + "\nInterarrival Time: " + interarrivalTime + " | Service Time: " + serviceTime);
    }
}
