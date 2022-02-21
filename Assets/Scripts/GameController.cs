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
    // this bool will help us know if the gen option was switched during simulation
    private bool prevGenStatus = true;

    [Header("Data From File")]
    [SerializeField] private string filePath = "./Assets/Scripts/Project1_Data.txt";

    [Header("Data From Code")]
    [SerializeField] [Range(1, 1000)] private int numberOfCustomers = 500;
    [SerializeField] private TimeRate interArrivalRate;
    [SerializeField] private TimeRate serviceRate;
    [SerializeField] private GenerationOption generationOption;

    [Header("Simulation")]
    [SerializeField] [Min(0.0f)] public float speed = 1.0f;
    [SerializeField] private UnityEvent onSimulationStart;

    [Header("Debug UI")]
    [SerializeField] private Text uiElapsedTime;
    [SerializeField] private Text uiNextCustomerID;
    [SerializeField] private EventLog eventLog;
    [SerializeField] private Text uiSpeed;

    [Header("Debugging")]
    [SerializeField] [Show] private float elapsedTime;
    [SerializeField] [Show] private int nextCustomerIdx = -1;
    [SerializeField] [Show] private List<CustomerData> customers = new List<CustomerData>();

    public bool IsRunning { get; private set; }

    private System.Random rand = new System.Random();

    private void Start()
    {
        GetData();

        prevGenStatus = generateFromFile;
    }

    private void FixedUpdate()
    {
        if (!IsRunning) return;

        Time.timeScale = speed;
        if (uiSpeed) uiSpeed.text = $"{speed:F2}";

        elapsedTime += Time.fixedDeltaTime;

        if (uiElapsedTime) uiElapsedTime.text = String.Format("{0:0.00}", elapsedTime);

        if (eventLog && prevGenStatus != generateFromFile)
        {
            if (generateFromFile) eventLog.Print("Switched to pregenerated data");
            else eventLog.Print("Switched to live data generation");
        }

        prevGenStatus = generateFromFile;
    }

    public void GetData()
    {
        customers.Clear();

        GetDataFromFile();

        if (uiNextCustomerID) uiNextCustomerID.text = customers[0].id.ToString();
    }

    public void StartSimulation()
    {
        IsRunning = true;
        onSimulationStart.Invoke();
    }

    public void speedOne()
    {
        speed = 0.5f;
    }

    public void speedTwo()
    {
        speed = 1.0f;
    }
    public void speedThree()
    {
        speed = 2.0f;
    }

    public void speedFour()
    {
        speed = 5.0f;
    }

    public CustomerData GetNextCustomer()
    {
        nextCustomerIdx = (nextCustomerIdx + 1) % customers.Count;

        if (uiNextCustomerID) uiNextCustomerID.text = customers[nextCustomerIdx].id.ToString();

        if (generateFromFile) return customers[nextCustomerIdx];

        return GenerateNextCustomer();
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
            if (eventLog) eventLog.Print("Could not open pregenerated data file. Switching to programatically generated data");

            GetDataProgramatically();
        }
    }

    private void GetDataProgramatically()
    {
        for (int i = 0; i < numberOfCustomers; i++)
        {
            CustomerData data = new CustomerData(
                i + 1,
                generationOption.Generate(rand, interArrivalRate.MeanTimeMinute),
                generationOption.Generate(rand, serviceRate.MeanTimeMinute));
            customers.Add(data);
        }

        Debug.Log($"Successfully generated data for {numberOfCustomers} customers");
        if (eventLog) eventLog.Print($"Successfully generated data for {numberOfCustomers} customers");
    }

    private CustomerData GenerateNextCustomer()
    {
        CustomerData data = new CustomerData(
            nextCustomerIdx + 1,
            generationOption.Generate(rand, interArrivalRate.MeanTimeMinute),
            generationOption.Generate(rand, serviceRate.MeanTimeMinute));

        return data;
    }
}
