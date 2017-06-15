using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour {

    private int highScore;

    public Text highScoreText;

	public void ToGame()
    {
        SceneManager.LoadScene("Map_01");
    }

    private void Start()
    {
        highScore =  PlayerPrefs.GetInt("PlayerScore");
        highScoreText.text = "Highscore: " + highScore.ToString();
    }
}
