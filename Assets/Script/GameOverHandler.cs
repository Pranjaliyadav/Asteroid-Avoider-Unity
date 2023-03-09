using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private GameObject gameOverDisplay;

    [SerializeField] private AsteroidSpawner asteroidSpawner;

    [SerializeField] private TMP_Text gameOverText;

    [SerializeField] private ScoreSystem scoreSystem;
    public void EndGame()
    {
        asteroidSpawner.enabled = false; //asteroid wont instantiate on screem

        int finalScore = scoreSystem.EndTimer(); //score will be shared here
        gameOverText.text = $"Your Score: {finalScore}"; //altern that gameover text we set up earliar

        gameOverDisplay.gameObject.SetActive(true);
    }
   public void Playagain()
    {
        SceneManager.LoadScene(1);
    }
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
    }
}
