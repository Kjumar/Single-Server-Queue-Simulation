using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.IO;

public class CustomerGenerator : MonoBehaviour
{
    [SerializeField] private bool generateFromFile = true;
    [SerializeField] private string filePath = "./Assets/Scripts/Project1_Data.txt";

    private List<Customer> customers;

    void Start()
    {
        if (generateFromFile) ReadFile();
    }

    private void ReadFile()
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
                    customers.Add(new Customer(float.Parse(data[1]), float.Parse(data[2])));
                }
            }
        }
        catch (Exception e)
        {
            Debug.Log(e);
        }
    }
}
