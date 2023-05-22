using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreData : ScriptableObject
{
    public int score = 0;

    public void IncrementScore(int amount)
    {
        score += amount;
    }
}

