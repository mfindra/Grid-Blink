using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class ScoreManager : MonoBehaviour
{ 
    public GameObject scorePanel;
    public GameObject scoreEntry;    

    private void Start()
    {        
        if (GameObject.FindObjectOfType<LayerManag>().layers[5].activeSelf == true)
        {
            ScoreList jsonScores = JsonUtility.FromJson<ScoreList>(PlayerPrefs.GetString("highscoreTable"));
            scorePanel.GetComponent<RectTransform>().sizeDelta = new Vector2(0, jsonScores.highscoreEntryList.Count * 50);

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

        if (GameObject.FindObjectOfType<LayerManag>().layers[7].activeSelf == true)
        {
            Debug.Log("call getscore function");
            StartCoroutine(GameObject.FindObjectOfType<ScoreRegistry>().GetScores("Grid Blink"));

        }
    }

}


[System.Serializable]
public class ScoreList
{
    public List<Score> highscoreEntryList;
}

[System.Serializable]
public class ScoreArray
{
    public Score[] scores;
}

[System.Serializable]
public class Score
{
    public string name;
    public string date;
    public int score;
}
