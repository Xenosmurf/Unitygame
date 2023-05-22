using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using TMPro;
using UnityEngine;

public class MathText_1 : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI equationText;
    [SerializeField] private TextMeshProUGUI attempsText;

    [SerializeField] private GameObject buttonStart;
    [SerializeField] private GameObject buttonSubmit;

    [SerializeField] private TMP_InputField answerInput;
    [SerializeField] private TextMeshProUGUI feedbackText;
    static int attemps = 3;
    static bool rightAnswer;
     
    string equation = "";

    private void Start()
    {
        buttonSubmit.gameObject.SetActive(false);
        answerInput.gameObject.SetActive(false);
        attempsText.gameObject.SetActive(false);
        answerInput.DeactivateInputField();
    }
    private void Update()
    {
        if (attemps == 0)
        {
            CorrectOut();
            attemps = 3;
            attempsText.text = "Attemps: " + attemps.ToString();
            return;
        }
    }
    public void GenerateEquation()
    {
        attempsText.gameObject.SetActive(true);
        attempsText.text = "Attemps: " + attemps.ToString();
        buttonStart.SetActive(false);

        int num1 = Random.Range(1, 100);
        int num2 = Random.Range(1, 100);
        string options = "-+";
        char act = options[Random.Range(0, 2)];
        equation = num1 + " " + act + " " + num2 ;
        if (act == '-')
        {
            if (num1 < num2)
            {
                equation = num2 + " " + act + " " + num1 ;
            }
        }
        equationText.text = equation;
    }

    public void GenerateCorrect()
    {
        if (rightAnswer)
        {
            CorrectOut();

        }
        else
        {
            Invoke("Clean", 2f);
        }
       
    }

    public void CorrectOut()
    {
        int result = CorrectResult(equationText.text);
        Invoke("GenerateEquation", 2f);
        Invoke("Clean", 2f);
        equationText.text += " = " + result.ToString();
    }
    public void Clean()
    {
        answerInput.text = "";
        feedbackText.text = "";
    }


    public int CorrectResult(string equation)
    {
        string[] equationParts = equation.Split(' '); // Split the equation into its individual parts
        int num1 = int.Parse(equationParts[0]); // Parse the first number
        string operand = equationParts[1]; // Get the operand
        int num2 = int.Parse(equationParts[2]); // Parse the second number

        // Perform the calculation
        int result = 0;
        switch (operand)
        {
            case "+":
                result = num1 + num2;
                break;
            case "-":
                result = num1 - num2;
                break;
        }
        return result;
    }

    public void CheckAnswer(string equation)
    {

        int res;
        if (int.TryParse(answerInput.text, out res))
        {
            // Conversion was successful, use the result
            Debug.Log("Parsed integer: " + res);
        }
        else
        {
            // Conversion failed, handle the error
            Debug.LogError("Failed to parse integer from input: " + answerInput.text);
        }

        int userAnswer = int.Parse(answerInput.text);
        int result = CorrectResult(equation);

        if (userAnswer == result)
        {
            feedbackText.text = "Correct";
            attemps = 3;
            attempsText.text = "Attemps: " + attemps.ToString();
            rightAnswer = true;

        }
        else
        {
            feedbackText.text = "Incorrect";
            attemps -= 1;
            attempsText.text = "Attemps: " + attemps.ToString();
            rightAnswer = false;
        }

    }

    public void FindTrue()
    {
        CheckAnswer(equationText.text);
    }

}
