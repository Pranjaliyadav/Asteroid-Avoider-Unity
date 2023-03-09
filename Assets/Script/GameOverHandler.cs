using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverHandler : MonoBehaviour
{
    [SerializeField] private GameObject gameOverDisplay;

    [SerializeField] private AsteroidSpawner asteroidSpawner;

    [SerializeField] private TMP_Text gameOverText;

    [SerializeField] private ScoreSystem scoreSystem;
    [SerializeField] private GameObject player;
    [SerializeField] private Button continueBtn;
    
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

    public void ContinueButton()
    {
        AdManager.Instance.ShowAd(this); //taking the gameOverHandler
        continueBtn.interactable = false;
    }

    public void ContinuesGame()
    {
        scoreSystem.StartTimer();

        player.transform.position = Vector3.zero;
        player.SetActive(true);
        player.GetComponent<Rigidbody>().velocity = Vector3.zero; 

        asteroidSpawner.enabled=true;

        gameOverDisplay.gameObject.SetActive(false);
    }
}
