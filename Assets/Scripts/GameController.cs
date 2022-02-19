using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using System;
using System.IO;

/// <summary>
/// Generates Customers with random interarrival times and services times following a normal distribution.
/// Values can be generated from a txt file or programmatically at runtime.
/// </summary>
[DisallowMultipleComponent]
public class GameController: MonoBehaviour
{
    [SerializeField] private bool generateFromFile = true;

    [Header("Data From File")]
    [SerializeField] private string filePath = "./Assets/Scripts/Project1_Data.txt";

    [Header("Data From Code")]
    [SerializeField] [Range(1, 1000)] private int numberOfCustomers = 500;
    [SerializeField] private float meanInterarrivalTime = 5;
    [SerializeField] private float meanServiceTime = 3;
    [SerializeField] private float standardDev = 1;

    [Header("Simulation")]
    [SerializeField] private UnityEvent onSimulationStart;

    [Header("Debug UI")]
    [SerializeField] private Text uiElapsedTime;
    [SerializeField] private Text uiNextCustomerID;
    [SerializeField] private EventLog eventLog;

    [Header("Debugging")]
    [SerializeField] [Show] private float elapsedTime;
    [SerializeField] [Show] private int nextCustomerIdx = -1;
    [SerializeField] [Show] private List<CustomerData> customers = new List<CustomerData>();

    public bool IsRunning { get; private set; }

    private System.Random rand = new System.Random();

    private void Start()
    {
        GetData();
    }

    private void FixedUpdate()
    {
        if (!IsRunning) return;
        elapsedTime += Time.fixedDeltaTime;

        if (uiElapsedTime) uiElapsedTime.text = String.Format("{0:0.00}", elapsedTime);
    }

    public void GetData()
    {
        customers.Clear();

        if (generateFromFile)
        {
            GetDataFromFile();
        }
        else
        {
            GetDataProgramatically();
        }

        if (uiNextCustomerID) uiNextCustomerID.text = customers[0].id.ToString();
    }

    public void StartSimulation()
    {
        IsRunning = true;
        onSimulationStart.Invoke();
    }

    public CustomerData GetNextCustomer()
    {
        nextCustomerIdx = (nextCustomerIdx + 1) % customers.Count;

        if (uiNextCustomerID) uiNextCustomerID.text = customers[nextCustomerIdx].id.ToString();

        return customers[nextCustomerIdx];
    }

    private void GetDataFromFile()
    {
        try
        {
            FileStream fs = new FileStream(filePath, FileMode.Open);
            StreamReader sr = new StreamReader(fs);

            string line = sr.ReadLine(); // read the first line (headers)
            char[] separators = new char[] { '\t' };

            while ((line = sr.ReadLine()) != null)
            {
                string[] data = line.Split(separators);
                if (data.Length > 2)
                {
                    customers.Add(new CustomerData(int.Parse(data[0]), float.Parse(data[1]), float.Parse(data[2])));
                }
            }

            Debug.Log($"Successfully loaded data from file {filePath}");
            if (eventLog) eventLog.Print($"Successfully loaded data from file {filePath}");
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    private void GetDataProgramatically()
    {
        for (int i = 0; i < numberOfCustomers; i++)
        {
            CustomerData data = new CustomerData(
                i + 1,
                RandomUniform(meanInterarrivalTime, standardDev),
                RandomUniform(meanServiceTime, standardDev));
            customers.Add(data);
        }

        Debug.Log($"Successfully generated data for {numberOfCustomers} customers");
        if (eventLog) eventLog.Print($"Successfully generated data for {numberOfCustomers} customers");
    }

    private float RandomUniform(float mean, float standardDev)
    {
        // using count = 12 specifically eliminates the square root from the formula and simplifies it
        // sqrt(12/n) * (fx(x:n) - n/2), n-> infinity
        // => 1 * (fx(12:n) - 6)
        int count = 12;

        double r = 0;
        for (int i = 0; i < count; i++)
        {
            r += rand.NextDouble();
        }
        double magnitude = count / 2;
        r = (r - magnitude);

        float randNormal = (float)(mean + standardDev * r);

        return randNormal;
    }
}
