using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Attemps : MonoBehaviour
{
    public int attemps;
    [SerializeField] private TextMeshProUGUI attempText;

    public void DisplayAttempts()
    {
 
        attempText.text = "Attemps: " + attemps.ToString();
    }
}
