using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ServiceManager : MonoBehaviour
{
    [SerializeField] private UnityEvent<ServiceManager> onServiceFinished;

    [Header("Debug UI")]
    [SerializeField] private Text uiCurrentCustomer;
    [SerializeField] private Text uiServiceTime;

    [Header("Debugging")]
    [SerializeField] private Customer currentCustomer;
    [SerializeField] [Show] private float timeLeftForService;

    private void FixedUpdate()
    {
        if (currentCustomer == null) return;

        timeLeftForService -= Time.fixedDeltaTime;

        if (uiServiceTime) uiServiceTime.text = string.Format("{0:0.00}", timeLeftForService);

        if (timeLeftForService <= 0)
        {
            FinishServe();
        }
    }

    public void ReceiveCustomer(Customer customer)
    {
        currentCustomer = customer;
        timeLeftForService = customer.serviceTime;

        if (uiCurrentCustomer) uiCurrentCustomer.text = currentCustomer.id.ToString();
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
