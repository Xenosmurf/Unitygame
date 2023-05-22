using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MyInput : MonoBehaviour
{

    [SerializeField] private TMP_InputField inputField;
    [SerializeField] private GameObject buttonSubmit;

    [SerializeField] private TMP_InputField myInput;
    [SerializeField] private TextMeshProUGUI myFeedback;

    // Start is called before the first frame update
    static public bool was = true;
    public void OnInput()
    {
        buttonSubmit.SetActive(true);
        inputField.gameObject.SetActive(true);
        inputField.ActivateInputField();
        myFeedback.gameObject.SetActive(true);
    }

    public void CleanUp()
    {
        myInput.text = "";
        myFeedback.text = "";
    }

    public void OnClick()
    {
        Debug.Log("From Input.cs was: " + was.ToString());
        if (was)
        {
            buttonSubmit.SetActive(true);
            Debug.Log("Input: obj was act: " + buttonSubmit.name);
            inputField.gameObject.SetActive(true);
            Debug.Log("Input: obj was act: " + inputField.name);
            inputField.ActivateInputField();
            Debug.Log("Input: obj was act: " + inputField.name);
        }

        was = true;
    }
}
