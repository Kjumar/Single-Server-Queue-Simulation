using UnityEngine;
using UnityEngine.Events;

public class ServiceManager : MonoBehaviour
{
    [SerializeField] private UnityEvent<ServiceManager> onServiceFinished;

    [Header("Debugging")]
    [SerializeField] private Customer currentCustomer;
    [SerializeField] [Show] private float timeLeftForService;

    private void FixedUpdate()
    {
        if (currentCustomer == null) return;

        timeLeftForService += Time.fixedDeltaTime;

        if (timeLeftForService >= currentCustomer.serviceTime)
        {
            FinishServe();
        }
    }

    public void ReceiveCustomer(Customer customer)
    {
        currentCustomer = customer;
        timeLeftForService = 0;
    }

    public void OnCustomerEnqueue(Customer customer)
    {
        if (currentCustomer == null)
        {
            onServiceFinished.Invoke(this);
        }
    }

    public void FinishServe()
    {
        Destroy(currentCustomer.gameObject);
        currentCustomer = null;

        Debug.Log("Finished Serving Customer in " + timeLeftForService + "s");

        onServiceFinished.Invoke(this);
    }
}
