using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LayerManag : MonoBehaviour
{   
    public GameObject[] layers;  
    public Text scoreText;
    public Text inputtext;
    [HideInInspector] public int game_over = 0;
    [HideInInspector] public int sceneChange = 0;
    [HideInInspector] public int lvl = 0;
    [HideInInspector] public int score = 0;
  
    void Start()
    {   
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
   
    public void DisableLayers()
    {
        foreach (GameObject lay in layers)
        {
            lay.SetActive(false);
        }
    }

    public void StartGame5x5()
    {
        DisableLayers();
        layers[1].SetActive(true);
        lvl = 1;
    }

    public void GlobalScores()
    {
        DisableLayers();
        layers[7].SetActive(true);
    }

    public void StartGame7x7()
    {
        DisableLayers();
        layers[2].SetActive(true);
    }

    public void MenuR()
    {
        DisableLayers();
        string currentSceneName = SceneManager.GetActiveScene().name;
        SceneManager.LoadScene(currentSceneName);
        layers[0].SetActive(true);
    }

    public void Menu()
    {
        DisableLayers();
        layers[0].SetActive(true);
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
       Application.Quit();
#endif

    }

    public void GameOver()
    {
        DisableLayers();
        layers[3].SetActive(true);
        scoreText.text = "Score " + score;       
    }

    public void AddScore()
    {        
        if (inputtext.text != "")
        {
            //Debug.Log(inputtext.text);
            AddHighscore(score, inputtext.text, System.DateTime.Now.ToString("dd-MM-yyyy"));
            GameObject.FindObjectOfType<ScoreRegistry>().AddNewScore("Grid Blink", inputtext.text, score);
            Scores();
        }
        
    }

    public void Scores()
    {
        DisableLayers();
        layers[5].SetActive(true);

    }

    public void Settings()
    {
        DisableLayers();
        layers[4].SetActive(true);
    }

    public void Help()
    {
        DisableLayers();
        layers[6].SetActive(true);
    }

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
