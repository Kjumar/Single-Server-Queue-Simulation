using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class QueueManager : MonoBehaviour
{
    [SerializeField] private UnityEvent<Customer> onCustomerAdded;

    private Queue<Customer> queue = new Queue<Customer>();

    public void AddCustomer(Customer newCustomer)
    {
        queue.Enqueue(newCustomer);
        Debug.Log($"Customer ##{newCustomer.id} arrived at queue ({queue.Count})");
        onCustomerAdded.Invoke(newCustomer);
    }

    public Customer RemoveCustomer()
    {
        // removes and returns the customer at the head of the queue
        if (queue.Count <= 0) return null;

        Customer customer = queue.Dequeue();
        Debug.Log($"Customer ##{customer.id} leaving queue ({queue.Count})");
        return customer;
    }

    public void OnGetNextCustomer(ServiceManager service)
    {
        Customer nextCustomer = RemoveCustomer();

        if (nextCustomer != null)
        {
            service.ReceiveCustomer(nextCustomer);
        }
    }
}