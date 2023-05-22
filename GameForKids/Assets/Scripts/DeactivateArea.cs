using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DeactivateArea : MonoBehaviour
{

    [SerializeField] private TextMeshProUGUI textProblem1;
    [SerializeField] private TextMeshProUGUI feedbackProblem;
    [SerializeField] private TextMeshProUGUI attempsProblem;
    [SerializeField] private TMP_InputField inputProblem;
    [SerializeField] private GameObject startBTN;
    [SerializeField] private GameObject submitBTN;
    // Start is called before the first frame update
    void Start()
    {
        textProblem1.gameObject.SetActive(false);
        feedbackProblem.gameObject.SetActive(false);
        attempsProblem.gameObject.SetActive(false);
        inputProblem.gameObject.SetActive(false);
        submitBTN.SetActive(false);
        
    }

    
}
