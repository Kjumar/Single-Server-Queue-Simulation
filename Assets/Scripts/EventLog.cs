using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventLog : MonoBehaviour
{
    [SerializeField] private Text uiText;

    public void Print(string message)
    {
        uiText.text = message + '\n' + uiText.text;
    }
}
