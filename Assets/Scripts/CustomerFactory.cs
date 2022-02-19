using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[DisallowMultipleComponent]
public class CustomerFactory : MonoBehaviour
{
    [SerializeField] private GameController gameController;

    [SerializeField] private GameObject customerPrefab;

    [SerializeField] private UnityEvent<Customer> onArrival;

    [Header("Visualizer")]
    [SerializeField] private Vector3 spawnPoint;

    [Header("Debug UI")]
    [SerializeField] private Text uiTimeToArrival;

    [Header("Debugging")]
    [SerializeField] [Show] private CustomerData nextCustomer;
    [SerializeField] [Show] private float timeLeftForArrival;

    private bool isRunning;

    private void FixedUpdate()
    {
        if (!isRunning) return;

        if (!nextCustomer.IsValid) return;

        timeLeftForArrival -= Time.fixedDeltaTime;

        if (uiTimeToArrival) uiTimeToArrival.text = string.Format("{0:0.00}", timeLeftForArrival);

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
        go.transform.position = spawnPoint;
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
