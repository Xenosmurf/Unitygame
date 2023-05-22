using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MathLvl2 : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI equationText2;
    [SerializeField] private TextMeshProUGUI attempsText2;

    [SerializeField] private GameObject buttonStart2;
    [SerializeField] private GameObject buttonSubmit2;

    [SerializeField] private TMP_InputField answerInput2;
    [SerializeField] private TextMeshProUGUI feedbackText2;
    static int attemps = 3;
    static bool rightAnswer;

    string equation = "";

    private void Start()
    {
       
            buttonSubmit2.gameObject.SetActive(false);
            Debug.Log("obj2 was del: " + buttonSubmit2.name);
            answerInput2.gameObject.SetActive(false);

            Debug.Log("obj2 was del: " + answerInput2.name);
            attempsText2.gameObject.SetActive(false);

            Debug.Log("obj2 was del: " + attempsText2.name);
            answerInput2.DeactivateInputField();

            Debug.Log("obj2 was del: " + answerInput2.name);
        
    }
private void Update()
    {
        if (attemps == 0)
        {
            CorrectOut();
            attemps = 3;
            attempsText2.text = "Attemps: " + attemps.ToString();
            return;
        }
    }
    public void GenerateEquation()
    {
        attempsText2.gameObject.SetActive(true);
        attempsText2.text = "Attemps: " + attemps.ToString();
        buttonStart2.SetActive(false);

        int num1 = Random.Range(1, 100);
        int num2 = Random.Range(1, 11);
        string options = "*/";
        char act = options[Random.Range(0, 2)];
        equation = num1 + " " + act + " " + num2;
        if (act == '/')
        {
            num1 = Random.Range(1, 10);
            num2 = Random.Range(1, 10);
            equation = num2 * num1 + " " + act + " " + num1;
            
        }
        equationText2.text = equation;
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
        int result = CorrectResult(equationText2.text);
        Invoke("GenerateEquation", 2f);
        Invoke("Clean", 2f);
        equationText2.text += " = " + result.ToString();
    }
    public void Clean()
    {
        answerInput2.text = "";
        feedbackText2.text = "";
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
            case "*":
                result = num1 * num2;
                break;
            case "/":
                result = num1 / num2;
                break;
        }
        return result;
    }

    public void CheckAnswer(string equation)
    {

        int res;
        if (int.TryParse(answerInput2.text, out res))
        {
            // Conversion was successful, use the result
            Debug.Log("Parsed integer: " + res);
        }
        else
        {
            // Conversion failed, handle the error
            Debug.LogError("Failed to parse integer from input: " + answerInput2.text);
        }

        int userAnswer = int.Parse(answerInput2.text);
        int result = CorrectResult(equation);

        if (userAnswer == result)
        {
            ScorePlayer.scorePlayer.ChangeScore(5);
            feedbackText2.text = "Correct";
            attemps = 3;
            attempsText2.text = "Attemps: " + attemps.ToString();
            rightAnswer = true;

        }
        else
        {
            feedbackText2.text = "Incorrect";
            attemps -= 1;
            attempsText2.text = "Attemps: " + attemps.ToString();
            rightAnswer = false;
        }

    }

    public void FindTrue()
    {
        CheckAnswer(equationText2.text);
    }
}
