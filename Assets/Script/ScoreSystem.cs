using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreSystem : MonoBehaviour
{

    [SerializeField] private TMP_Text scoreText;
    [SerializeField] private float scoreMultiplier;
    

    private float score;
    private bool shouldCount = true; //if false that means score wont increase

    private void Update()
    {   
        if (!shouldCount) { return; } //if false return, no need to increase or decrease score

        score += Time.deltaTime * scoreMultiplier; //score increasing by a number

        scoreText.text = Mathf.FloorToInt(score).ToString(); //as the value will have lot of numbers after decimals so conert to floor

    }

    public int EndTimer()
    {
        scoreText.text = string.Empty; //just set it to empty, so it'll just disappear from screen
        shouldCount = false; //now its false, so score wont change

        return Mathf.FloorToInt(score); //returning here, as we'll call this in gameOverHandler
    }


}
