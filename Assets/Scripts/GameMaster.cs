using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameMaster : MonoBehaviour
{
    public int playerPoints;
    public int maxLevelPoints;
    public int playerLives;
    public Text scoreText;
    public Text livesText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText.text = "SCORE: " + playerPoints;
        livesText.text = "BEACH BALLS: " + playerLives;
    }

    // Update is called once per frame
    void Update()
    {

        if (playerLives <= 0)
        {
            SceneManager.LoadScene("LoseScene");
        }

        // Level 1 to 2
        if (playerPoints >= maxLevelPoints || Input.GetKeyDown(KeyCode.N) && SceneManager.GetActiveScene().name == "Level 1")
        {
            SceneManager.LoadScene("Level 2");
        }

        // Level 2 to 3
        if (playerPoints >= maxLevelPoints || Input.GetKeyDown(KeyCode.N) && SceneManager.GetActiveScene().name == "Level 2")
        {
            SceneManager.LoadScene("Level 3");
        }

        // Level 3 to 4
        if (playerPoints >= maxLevelPoints || Input.GetKeyDown(KeyCode.N) && SceneManager.GetActiveScene().name == "Level 3")
        {
            SceneManager.LoadScene("Level 4");
        }

        // Level 4 to 5
        if (playerPoints >= maxLevelPoints || Input.GetKeyDown(KeyCode.N) && SceneManager.GetActiveScene().name == "Level 4")
        {
            SceneManager.LoadScene("Level 5");
        }

        // Level 5 to Win
        if (FindObjectsOfType<DestroyBrick>().Length == 0 || Input.GetKeyDown(KeyCode.N) && SceneManager.GetActiveScene().name == "Level 5")
        {
            SceneManager.LoadScene("WinScene");
        }

        // Press H to get to the Main Menu
        if (Input.GetKeyDown(KeyCode.H))
        {
            SceneManager.LoadScene("MainMenu");
        }

        // Press G to get to the lose scene
        if (Input.GetKeyDown(KeyCode.G))
        {
            SceneManager.LoadScene("LoseScene");
        }

        // Press Y to get to the win scene
        if (Input.GetKeyDown(KeyCode.Y))
        {
            SceneManager.LoadScene("WinScene");
        }
    }

    // HUD
    public void UpdateLives(int changeInLives)
    {
        playerLives += changeInLives;

        livesText.text = "LIVES: " + playerLives;
    }

    public void UpdateScore(int points)
    {
        playerPoints += points;

        scoreText.text = "SCORE: " + playerPoints;
    }

    // Buttons
    public void PlayAgain()
    {
        SceneManager.LoadScene("Level 1");
    }

    public void HowTo()
    {
        SceneManager.LoadScene("HowToPlay");
    }

    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void Backstory()
    {
        SceneManager.LoadScene("Backstory");
    }

    public void Quit()
    {
        Application.Quit();
    }

}
