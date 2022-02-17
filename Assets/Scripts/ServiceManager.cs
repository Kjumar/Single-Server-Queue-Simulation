using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ServiceManager : MonoBehaviour
{
    Customer currentCustomer;
    float timeLeftForService = 0;

    public bool isBusy = false;

    private void FixedUpdate()
    {
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
        isBusy = true;
    }

    void FinishServe()
    {
        Destroy(currentCustomer.gameObject);
        isBusy = false;
    }
}
