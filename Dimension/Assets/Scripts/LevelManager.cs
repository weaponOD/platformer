using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public static LevelManager Instance { set; get; }       

    private int lives = 3;
    private int score = 0;

    public Transform spawnPosition;
    public Transform playerTransform;

    public Text scoreText;
    public Text livesText;
    

//***************************************************************************************
//	Awake() - set our score and lives text to screen
//  
//
//***************************************************************************************
    private void Awake()
    {
        Instance = this;
        scoreText.text = "Current Score: " + score.ToString()  ;
        livesText.text = "Lives: " + lives.ToString()  ;
    }


//***************************************************************************************
//	Update() - If player falls below a point we will send them to the spawn position
//  
//
//***************************************************************************************
    private void Update()
    {
        if(playerTransform.position.y < -10)
        {
            
            playerTransform.position = spawnPosition.position;
            lives--;
            livesText.text = "Lives: " + lives.ToString();
            if (lives <= 0)
            {
                SceneManager.LoadScene("MainMenu");
            }

        }
        else
        {

        }

    }


//***************************************************************************************
//	Win() - handles what happens when the player completes the game ( save highscore)
//  
//
//***************************************************************************************
    public void Win()
    {
        if (score > PlayerPrefs.GetInt("PlayerScore"))
        {
            PlayerPrefs.SetInt("PlayerScore", score);
        }
        SceneManager.LoadScene("MainMenu");

    }

//***************************************************************************************
//	CollectCoin() - Add a coin and display score text
//  
//
//***************************************************************************************
    public void CollectCoin()
    {
        score++;
        scoreText.text = "Score: " + score.ToString();
        
    }
}
