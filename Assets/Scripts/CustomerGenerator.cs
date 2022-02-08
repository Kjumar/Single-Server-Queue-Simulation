using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

/// <summary>
/// Generates Customers with random interarrival times and services times following a normal distribution.
/// Values can be generated from a txt file or programmatically at runtime.
/// </summary>
public class CustomerGenerator : MonoBehaviour
{
    [SerializeField] private bool generateFromFile = true;

    [Header("From File")]
    [SerializeField] private string filePath = "./Assets/Scripts/Project1_Data.txt";

    [Header("From Code")]
    [SerializeField] private float meanInterarrivalTime = 5;
    [SerializeField] private float meanServiceTime = 3;
    [SerializeField] private float standardDev = 1;

    System.Random rand = new System.Random();

    private List<Customer> customers;

    void Start()
    {
        if (generateFromFile)
        {
            GetData();
        }
        else
        {
            // testing generating Customers at runtime
            // the Customer constructor will print its values
            for (int i = 0; i < 500; i++)
            {
                GetCustomerProgramatically(i + 1);
            }
        }
    }

    private void GetData()
    {
        customers = new List<Customer>();

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
                    customers.Add(new Customer(int.Parse(data[0]), float.Parse(data[1]), float.Parse(data[2])));
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }

    private float RandomBoxMuller(float mean, float standardDev)
    {
        // Box-Muller method for aproximating gaussian random numbers. Interesting but expensive
        double r1 = 1f - rand.NextDouble(); // System.Random.NextDouble() returns a uniform random double in (0, 1] (zero is inclusive)
        double r2 = 1f - rand.NextDouble(); // our value can't be exatly 0, so we subtract from 1
        double rNormal = Math.Sqrt(-2f * Math.Log(r1)) * Math.Sin(2.0 * Math.PI * r2);
        float randNormal = (float)(mean + standardDev * rNormal);

        return randNormal;
    }

    private float RandomIrwinHall(float mean, float standardDev)
    {
        // see Irwin-Hall distribution and Central Limit Theory.
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

    private Customer GetCustomerProgramatically(int id)
    {
        //return new Customer(RandomBoxMuller(meanInterarrivalTime, standardDev),
        //    RandomBoxMuller(meanServiceTime, standardDev));
        return new Customer(id, RandomIrwinHall(meanInterarrivalTime, standardDev),
            RandomIrwinHall(meanServiceTime, standardDev));
    }
}
