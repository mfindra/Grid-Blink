using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

// list of score items (local)
[System.Serializable] public class ScoreList
{
    public List<Score> highscoreEntryList;
}

//array of scores (global)
[System.Serializable] public class ScoreArray
{
    public Score[] scores;
}

// score entry
[System.Serializable] public class Score
{
    public string name;
    public string date;
    public int score;
}

public class ScoreManager : MonoBehaviour
{ 
    public GameObject scorePanel;
    public GameObject scoreEntry;    

    private void Start()
    {   
        // list local scores 
        if (GameObject.FindObjectOfType<LayerManag>().layers[5].activeSelf == true)
        {
            ScoreList jsonScores = JsonUtility.FromJson<ScoreList>(PlayerPrefs.GetString("highscoreTable"));
            scorePanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, jsonScores.highscoreEntryList.Count * 50);
            scoreEntry.SetActive(false) ;
            jsonScores.highscoreEntryList.Sort((x, y) => y.score.CompareTo(x.score));

            for (int i = 0; i < jsonScores.highscoreEntryList.Count; i++)
            {
                Score s = jsonScores.highscoreEntryList[i];
                GameObject newEntry = Instantiate(scoreEntry, scorePanel.transform);
                newEntry.SetActive(true);
                newEntry.transform.Find("TextRank").GetComponent<Text>().text = (i + 1).ToString();
                newEntry.transform.Find("TextDate").GetComponent<Text>().text = s.date;
                newEntry.transform.Find("TextScore").GetComponent<Text>().text = s.score.ToString();
                newEntry.transform.Find("TextName").GetComponent<Text>().text = s.name;
                newEntry.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -50 * i - 30);
            }
        }
        
        // list global scores
        if (GameObject.FindObjectOfType<LayerManag>().layers[7].activeSelf == true)
        {          
            StartCoroutine(GameObject.FindObjectOfType<ScoreRegistry>().GetScores("Grid Blink"));
        }
    }

}
