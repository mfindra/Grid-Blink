using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LayerManag : MonoBehaviour
{   
    public GameObject[] layers;  // list of all game layers
    public Text scoreText;
    public Text inputtext;
    [HideInInspector] public int game_over = 0;
    [HideInInspector] public int sceneChange = 0;
    [HideInInspector] public int lvl = 0;
    [HideInInspector] public int score = 0;
  
    void Start()
    {   
        // set togglers and values in playerPrefs on game start
        GameObject.FindObjectOfType<ColorManag>().SetToggler(GameObject.FindObjectOfType<ColorManag>().colorToggle, "colorMode");
        GameObject.FindObjectOfType<ColorManag>().ChangeColor();
        GameObject.FindObjectOfType<ColorManag>().SetToggler(GameObject.FindObjectOfType<ColorManag>().blindToggle, "blindMode");
        GameObject.FindObjectOfType<ColorManag>().BlindMode();
        GameObject.FindObjectOfType<ColorManag>().SetToggler(GameObject.FindObjectOfType<ColorManag>().soundToggle, "sound");
        GameObject.FindObjectOfType<ColorManag>().Sound();
  
        DisableLayers();
        layers[0].SetActive(true);
        StartCoroutine(_wait());
        StartCoroutine(_sceneChange());

    }

    // change scene timer
    private IEnumerator _sceneChange()
    {
        yield return new WaitForSeconds(0.5f);
        if (sceneChange == 0)
        {
            StartCoroutine(_sceneChange());
        }
        else
        {
            StartGame7x7();
        }
    }

    // end game 
    private IEnumerator _wait()
    {
        yield return new WaitForSeconds(0.5f);
        if (game_over == 0)
        {
            StartCoroutine(_wait());
        }
        else
        {
            GameOver();
        }
    }
   
    // disable all layers
    public void DisableLayers()
    {
        foreach (GameObject lay in layers)
        {
            lay.SetActive(false);
        }
    }

    // activate 5x5 level layer
    public void StartGame5x5()
    {
        DisableLayers();
        layers[1].SetActive(true);
        lvl = 1;
    }

    // activate global scores layer
    public void GlobalScores()
    {
        DisableLayers();
        layers[7].SetActive(true);
    }

    // activate 7x7 level layer
    public void StartGame7x7()
    {
        DisableLayers();
        layers[2].SetActive(true);
    }

    // activate menu layer, with value restart
    public void MenuR()
    {
        DisableLayers();
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        layers[0].SetActive(true);
    }

    // activate menu layer
    public void Menu()
    {
        DisableLayers();
        layers[0].SetActive(true);
    }

    // exit game
    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
       Application.Quit();
#endif

    }

    // activate game over layer
    public void GameOver()
    {
        DisableLayers();
        layers[3].SetActive(true);
        scoreText.text = "Score " + score;       
    }

    // add now score and upload to server
    public void AddScore()
    {        
        if (inputtext.text != "")
        {          
            AddHighscore(score, inputtext.text, System.DateTime.Now.ToString("dd-MM-yyyy"));
            GameObject.FindObjectOfType<ScoreRegistry>().AddNewScore("Grid Blink", inputtext.text, score);
            Scores();
        }
        
    }

    // activate score layer
    public void Scores()
    {
        DisableLayers();
        layers[5].SetActive(true);

    }

    // activate settings layer
    public void Settings()
    {
        DisableLayers();
        layers[4].SetActive(true);
    }

    // activate help layer
    public void Help()
    {
        DisableLayers();
        layers[6].SetActive(true);
    }

    // add new local score
    public void AddHighscore(int score, string name, string date)
    {
        // Create HighscoreEntry
        Score highscoreEntry = new Score { score = score, name = name, date = date };

        // Load saved Highscores
        string jsonString = PlayerPrefs.GetString("highscoreTable");
        ScoreList highscores = JsonUtility.FromJson<ScoreList>(jsonString);

        if (highscores == null)
        {
            // There's no stored table, initialize
            highscores = new ScoreList()
            {
                highscoreEntryList = new List<Score>()
            };
        }

        // Add new entry to Highscores
        highscores.highscoreEntryList.Add(highscoreEntry);

        // Save updated Highscores
        string json = JsonUtility.ToJson(highscores);
        PlayerPrefs.SetString("highscoreTable", json);
        PlayerPrefs.Save();
    }
}
