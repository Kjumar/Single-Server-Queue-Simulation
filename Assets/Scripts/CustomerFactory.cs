﻿using UnityEngine;
using UnityEngine.Events;

[DisallowMultipleComponent]
public class CustomerFactory : MonoBehaviour
{
    [SerializeField] private GameController gameController;

    [SerializeField] private GameObject customerPrefab;

    [SerializeField] private UnityEvent<Customer> onArrival;

    [Header("Debugging")]
    [SerializeField] [Show] private CustomerData nextCustomer;
    [SerializeField] [Show] private float timeLeftForArrival;

    private bool isRunning;

    private void FixedUpdate()
    {
        if (!isRunning) return;

        if (!nextCustomer.IsValid) return;

        timeLeftForArrival -= Time.fixedDeltaTime;

        if (timeLeftForArrival <= 0)
        {
            EnqueueCustomer();
        }
    }

    public void OnSimulationStarted()
    {
        isRunning = true;
        GetNextCustomer();
    }

    private void EnqueueCustomer()
    {
        GameObject go = Instantiate(customerPrefab);
        Customer customer = go.GetComponent<Customer>();

        customer.SetData(nextCustomer);

        onArrival.Invoke(customer);

        GetNextCustomer();
    }

    private void GetNextCustomer()
    {
        nextCustomer = gameController.GetNextCustomer();

        if (nextCustomer.IsValid)
        {
            timeLeftForArrival = nextCustomer.interarrivalTime;
        }
    }
}
