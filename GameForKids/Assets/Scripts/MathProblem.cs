using System.Collections;
using UnityEngine;
using TMPro;
using Mono.Data.Sqlite;
using Mono.Data;
using System.Data;



public class MathProblem : MonoBehaviour
{
    public string DataBaseName;
    //static bool problemPresent;
    static int currentId;
    static int currentAnswer;
    public int attemptsProblemCount = 3;

    [SerializeField] private TextMeshProUGUI textProblem1;
    [SerializeField] private TextMeshProUGUI feedbackProblem;
    [SerializeField] private TextMeshProUGUI attempsProblem;
    [SerializeField] private TMP_InputField inputProblem;
    [SerializeField] private GameObject startBTN;
    [SerializeField] private GameObject submitBTN;


    private void Update()
    {
        if(attemptsProblemCount == 0) 
        { 
            CheckAnswer();
            attemptsProblemCount = 3;
            attempsProblem.text = "Attemps: " + attemptsProblemCount.ToString();
            return;
        }
    }
    public int CountProblem(IDbConnection dbConection)
    {
        IDbCommand dbCommand;
        IDataReader dbReader;
        int count = 0;
        dbCommand = dbConection.CreateCommand();
        string SQLQuery = "SELECT COUNT(*) FROM MathProblems";
        dbCommand.CommandText = SQLQuery;
        dbReader = dbCommand.ExecuteReader();
        while (dbReader.Read())
        {
            count = dbReader.GetInt32(0);
        }
        dbReader.Close();
        dbReader = null;
        dbCommand.Dispose();
        dbConection = null;
        return count;
    }
    
    public void TestProblem()
    {
        textProblem1.gameObject.SetActive(true);
        submitBTN.gameObject.SetActive(true);
        Debug.Log("obj2 was act: " + submitBTN.name);

        inputProblem.gameObject.SetActive(true);
        Debug.Log("obj2 was act: " + inputProblem.name);

        inputProblem.DeactivateInputField();
        Debug.Log("obj2 was act: " + inputProblem.name);

        attempsProblem.gameObject.SetActive(true);

        feedbackProblem.gameObject.SetActive(true);

        //attempsProblem.gameObject.SetActive(true);
        //attempsProblem.text = "Attemps: " + attemptsProblemCount.ToString();
        startBTN.SetActive(false);
        string conn = SetDataBaseClass.SetDataBase(DataBaseName + ".db");
        IDbConnection dbConection;
        IDbCommand dbCommand;
        IDataReader dbReader;

        dbConection = new SqliteConnection(conn);
        dbConection.Open();
        dbCommand = dbConection.CreateCommand();

        int count = CountProblem(dbConection);
        
        string SQLQuery = "SELECT id, problemText, answer FROM MathProblems WHERE played = 0;";
        dbCommand.CommandText = SQLQuery;
        dbReader = dbCommand.ExecuteReader();
       
        if (dbReader.Read())
        {
            //problemPresent = true;
            attempsProblem.gameObject.SetActive(true);
            attempsProblem.text = "Attemps: " + attemptsProblemCount.ToString();
            currentId = dbReader.GetInt32(0);
            textProblem1.text = dbReader.GetString(1);
            currentAnswer = dbReader.GetInt32(2);

        }
        else
        {
            MyInput.was = false;
            textProblem1.text = "That`s all!";
            inputProblem.gameObject.SetActive(false);
            Debug.Log("obj2 was del: " + inputProblem.name);

            inputProblem.DeactivateInputField();
            Debug.Log("obj2 was del: " + inputProblem.name);

            attempsProblem.gameObject.SetActive(false);
            submitBTN.SetActive(false);

        }
        dbReader.Close();
        dbReader = null;
        dbConection.Close();
        dbCommand.Dispose();
        dbConection = null;

    }
    public void Clean()
    {
        inputProblem.text = "";
        feedbackProblem.text = "";
    }
    public void CheckAnswer()
    {
        string conn = SetDataBaseClass.SetDataBase(DataBaseName + ".db");
        IDbConnection dbConnection;
        IDbCommand dbCommand;

        dbConnection = new SqliteConnection(conn);
        dbConnection.Open();

        int userAnswer = int.Parse(inputProblem.text);
       
        dbCommand = dbConnection.CreateCommand();
        string SQLPlayed = "UPDATE MathProblems SET played = 1 WHERE id = " + currentId.ToString() + " ;";
        dbCommand.CommandText = SQLPlayed;
        dbCommand.Connection = dbConnection;

        if(currentAnswer == userAnswer)
        {
            ScorePlayer.scorePlayer.ChangeScore(10);
            feedbackProblem.text = "Correct";
            currentId++;
            Invoke("TestProblem", 2f);
            Invoke("Clean", 2f);
            attemptsProblemCount = 3;
            attempsProblem.text = "Attemps: " + attemptsProblemCount.ToString();
            dbCommand.ExecuteNonQuery();
        }
        else
        {
            feedbackProblem.text = "Incorrect";
            attemptsProblemCount--;
            attempsProblem.text = "Attemps: " + attemptsProblemCount.ToString();
            Invoke("Clean", 2f);
        }
        dbCommand.Dispose();
        dbConnection.Close();
        dbConnection = null;
    }

    
}





