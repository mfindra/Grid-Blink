using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class ScoreRegistry : MonoBehaviour
{       
    public GameObject scorePanel;
    public GameObject scoreEntry;
    public Text DownloadText;
    
    // Get score from server
    public IEnumerator GetScores(string appName)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get($"https://serene-tor-28878.herokuapp.com/data?app={appName}"))
        {
            yield return webRequest.SendWebRequest();
            DownloadText.enabled = false;

            // check if request was successful
            if (webRequest.isNetworkError)
            {
                Debug.Log("Error: " + webRequest.error);
                DownloadText.enabled = true;
                DownloadText.text = "Error";
            }
            else
            {              
               // list score entries to table
                if (GameObject.FindObjectOfType<LayerManag>().layers[7].activeSelf == true)
                {
                    ScoreArray s = JsonUtility.FromJson<ScoreArray>(webRequest.downloadHandler.text);                    
                    for (int i = 0; i < s.scores.Length; i++)
                    {                        
                        GameObject newEntry = Instantiate(scoreEntry, scorePanel.transform);
                        newEntry.SetActive(true);                        
                        newEntry.transform.Find("TextRank").GetComponent<Text>().text = (i + 1).ToString();
                        newEntry.transform.Find("TextDate").GetComponent<Text>().text = s.scores[i].date;
                        newEntry.transform.Find("TextScore").GetComponent<Text>().text = s.scores[i].score.ToString();
                        newEntry.transform.Find("TextName").GetComponent<Text>().text = s.scores[i].name;
                        newEntry.GetComponent<RectTransform>().anchoredPosition = new Vector2(0, -50 * i - 30);
                        
                    }
                }
            }
        }                
        

    }

    // Upload score to server
    public void AddNewScore(string gameName, string username, int score)
    {
        StartCoroutine(Upload(gameName, username, score.ToString()));
    }

    private IEnumerator Upload(string gameName, string username, string score)
    {
        WWWForm form = new WWWForm();

        form.AddField("app", gameName);
        form.AddField("user", username);
        form.AddField("score", score);

        UnityWebRequest www = UnityWebRequest.Post("https://serene-tor-28878.herokuapp.com/addEntry", form);

        yield return www.SendWebRequest();

        if (www.isNetworkError || www.isHttpError)
        {
            Debug.Log(www.error);
        }
        else
        {
            Debug.Log("Successfully uploaded new score.");
        }
    }
}