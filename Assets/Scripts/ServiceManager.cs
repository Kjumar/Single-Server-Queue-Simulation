using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class ServiceManager : MonoBehaviour
{
    [SerializeField] private UnityEvent<ServiceManager> onServiceFinished;

    [Header("Visualizer")]
    [SerializeField] private Vector3 exitPosition;

    [Header("Debug UI")]
    [SerializeField] private Text uiCurrentCustomer;
    [SerializeField] private Text uiServiceTime;
    [SerializeField] private EventLog eventLog;

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

        currentCustomer.SnapToTarget();
        currentCustomer.SetTargetPosition(transform.position);
        currentCustomer.BeginService();

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
        string serviceTime = currentCustomer.serviceTime.ToString();
        currentCustomer.SetTargetPosition(exitPosition);
        currentCustomer.EndService();
        Destroy(currentCustomer.gameObject, 2);
        currentCustomer = null;

        Debug.Log($"Finished Serving Customer in {serviceTime}s");
        if (eventLog) eventLog.Print($"Finished Serving Customer in {serviceTime}s");

        onServiceFinished.Invoke(this);
    }
}
