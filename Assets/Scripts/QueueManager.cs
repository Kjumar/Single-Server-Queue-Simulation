using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class QueueManager : MonoBehaviour
{
    [SerializeField] private UnityEvent<Customer> onCustomerAdded;

    [Header("DebugUI")]
    [SerializeField] private Text uiCustomersInQueue;
    [SerializeField] private Text uiFirstInLine;

    [Header("Debugging")]
    [SerializeField] [Show] [Rename("# of Customers in Queue")] private int count;
    [SerializeField] private Customer firstInLine;

    private Queue<Customer> queue = new Queue<Customer>();

    public void AddCustomer(Customer newCustomer)
    {
        Enqueue(newCustomer);
        Debug.Log($"Customer ##{newCustomer.id} arrived at queue ({count})");
        onCustomerAdded.Invoke(newCustomer);
    }

    public Customer RemoveCustomer()
    {
        // removes and returns the customer at the head of the queue
        if (count <= 0) return null;

        Customer customer = Dequeue();
        Debug.Log($"Customer ##{customer.id} leaving queue ({count})");
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

    private void Enqueue(Customer customer)
    {
        if (queue.Count <= 0)
        {
            firstInLine = customer;
            if (uiFirstInLine) uiFirstInLine.text = firstInLine.id.ToString();
        }
        queue.Enqueue(customer);
        count = queue.Count;

        if (uiCustomersInQueue) uiCustomersInQueue.text = count.ToString();
    }

    private Customer Dequeue()
    {
        // We need this check to prevent an exception being thrown
        if (queue.Count <= 0) return null;

        Customer customer = queue.Dequeue();
        count = queue.Count;
        firstInLine = queue.Count > 0 ? queue.Peek() : null;
        if (uiFirstInLine && firstInLine) uiFirstInLine.text = firstInLine.id.ToString();
        return customer;
    }
}
