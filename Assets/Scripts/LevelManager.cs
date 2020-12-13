using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public GameObject[] layer;
    public GameObject scrp;
    public GameObject lvlText;
    public GameObject scoreText;
    public AudioSource audioS;
    public AudioClip audioC;
    private int[] nums = new int[50];
    private int LvlAmt;
    private float lvl_time;
    private int amt = 0;
    private Color c_gray;

    [SerializeField] private Text cntDwnText;

    private void Start()
    {
        LvlAmt = 0;
        StartCoroutine(_wait());
    }

    private IEnumerator _wait()
    {
        if (scrp.GetComponent<LayerManag>().sceneChange == 0)
        {
            amt = 3;
            int show_time = 5;
            int guess_time = 5;

            while (amt != 11)
            {
                if (scrp.GetComponent<LayerManag>().game_over != 1)
                {
                    if (PlayerPrefs.GetInt("sound") == 1 && amt != 3)
                        audioS.PlayOneShot(audioC);

                    lvl_time = CreateLvl1(show_time, guess_time, amt);
                    lvlText.GetComponent<Text>().text = "Level " + scrp.GetComponent<LayerManag>().lvl;
                    scoreText.GetComponent<Text>().text = "Score " + scrp.GetComponent<LayerManag>().score;
                    yield return new WaitForSeconds(lvl_time);
                    scrp.GetComponent<LayerManag>().lvl++;
                    scrp.GetComponent<LayerManag>().score += amt * 10;
                }

                if (scrp.GetComponent<LayerManag>().lvl % 4 == 0)
                {
                    amt += 4;
                }else if (scrp.GetComponent<LayerManag>().lvl % 2 == 0)
                {
                    amt += 2;
                }

            }           

            scrp.GetComponent<LayerManag>().sceneChange = 1;
        }
        else
        {
            int amt_cnt = 3;
            int show_time = 6;
            int guess_time = 5;

            while (scrp.GetComponent<LayerManag>().game_over != 1)
            {
                if (PlayerPrefs.GetInt("sound") == 1)
                {
                    audioS.PlayOneShot(audioC);
                }

                if (scrp.GetComponent<LayerManag>().lvl % 2 == 1)
                {
                    amt_cnt++;
                }
                if (scrp.GetComponent<LayerManag>().lvl % 5 == 0)
                {
                    show_time += 1;
                    guess_time += 1;
                }
                                 
                lvl_time = CreateLvl2(show_time, guess_time, amt_cnt);
                lvlText.GetComponent<Text>().text = "Level " + scrp.GetComponent<LayerManag>().lvl;
                scoreText.GetComponent<Text>().text = "Score " + scrp.GetComponent<LayerManag>().score;
                yield return new WaitForSeconds(lvl_time);
                scrp.GetComponent<LayerManag>().lvl++;
                scrp.GetComponent<LayerManag>().score += amt_cnt * 10;
            }
        }
    }

    public float CreateLvl1(float timeshow, float timeguess, int amt)
    {
        HideNumber();
        int size = 15;
        ClearArray(size);
        GenerateArray(amt, size);
        LvlAmt = amt;
        ShowNumber();
        FunctionTimer.Create(HideNumber, timeshow);
        StartCoroutine(_checkwait(timeshow, timeguess));
        return (timeshow + timeguess + 0.5f);
    }

    public float CreateLvl2(float timeshow, float timeguess, int amt)
    {
        HideNumber();
        int size = 48;
        ClearArray(size);
        GenerateArray(amt, size);
        LvlAmt = amt;
        ShowNumber();
        FunctionTimer.Create(HideNumber, timeshow);
        StartCoroutine(_checkwait(timeshow, timeguess));
        return (timeshow + timeguess + 0.5f);
    }

    private IEnumerator _checkwait(float timeshow, float timeguess)
    {
        yield return new WaitForSeconds(timeshow);
        //Debug.Log("ah");
        FunctionTimer.Create(CheckNumbers, timeguess);
    }

    public void ShowTime(float time)
    {
        cntDwnText.text = time.ToString("0");
    }

    private void ShowNumber()
    {
        int k = 0;
        foreach (GameObject _button in layer)
        {
            ColorUtility.TryParseHtmlString("#BFBFBF", out c_gray);
            _button.GetComponent<Button>().interactable = false;
            ColorBlock colorBlock = _button.GetComponent<Button>().colors;

            if (nums[k] == 1)
            {
                colorBlock.normalColor = Color.blue;
                colorBlock.selectedColor = Color.blue;
                colorBlock.highlightedColor = Color.blue;
                colorBlock.disabledColor = Color.blue;
                if (PlayerPrefs.GetInt("blindMode") == 1)
                {
                    _button.GetComponentInChildren<Text>().text = "O";
                }
            }
            else
            {
                if (PlayerPrefs.GetInt("colorMode") == 1)
                {
                    colorBlock.disabledColor = c_gray;
                }
                else
                {
                    colorBlock.disabledColor = Color.white;
                }
            }
            _button.GetComponent<Button>().colors = colorBlock;
            k++;
        }
    }

    private void HideNumber()
    {
        int k = 0;
        foreach (GameObject _button in layer)
        {
            ColorUtility.TryParseHtmlString("#BFBFBF", out c_gray);
            _button.GetComponent<Button>().interactable = true;
            ColorBlock colorBlock = _button.GetComponent<Button>().colors;
            if (PlayerPrefs.GetInt("colorMode") == 1)
            {
                colorBlock.normalColor = c_gray;
                colorBlock.selectedColor = c_gray;
                colorBlock.highlightedColor = c_gray;
                _button.GetComponentInChildren<Text>().text = "";
            }
            else
            {
                colorBlock.normalColor = Color.white;
                colorBlock.selectedColor = Color.white;
                colorBlock.highlightedColor = Color.white;
                _button.GetComponentInChildren<Text>().text = "";
            }
            k++;
            _button.GetComponent<Button>().colors = colorBlock;
        }
    }

    private int GenerateArray(int max, int size)
    {
        int sum = 0;
        for (int i = 0; i < max; i++)
        {
            int _num = Random.Range(0, size);

            while (nums[_num] == 1)
            {
                _num = Random.Range(0, size);
            }
            nums[_num] = 1;
        }
        return sum;
    }

    private void ClearArray(int size)
    {
        for (int i = 0; i < size; i++)
        {
            nums[i] = 0;
        }
    }

    public void SetButColor(GameObject button)
    {
        ColorBlock colorBlock = button.GetComponent<Button>().colors;
        if (colorBlock.normalColor != Color.red)
        {
            colorBlock.normalColor = Color.red;
            colorBlock.selectedColor = Color.red;
            colorBlock.highlightedColor = Color.red;
            if (PlayerPrefs.GetInt("blindMode") == 1)
            {
                button.GetComponentInChildren<Text>().text = "X";
            }
            button.GetComponent<Button>().colors = colorBlock;
        }
        else
        {
            if (PlayerPrefs.GetInt("colorMode") == 1)
            {
                colorBlock.normalColor = c_gray;
                colorBlock.selectedColor = c_gray;
                colorBlock.highlightedColor = c_gray;
            }
            else
            {
                colorBlock.normalColor = Color.white;
                colorBlock.selectedColor = Color.white;
                colorBlock.highlightedColor = Color.white;
            }
            if (PlayerPrefs.GetInt("blindMode") == 1)
            {
                button.GetComponentInChildren<Text>().text = "";
            }
            button.GetComponent<Button>().colors = colorBlock;
        }
    }

    public void CheckNumbers()
    {
        int k = 0;
        int cnt = 0;
        int allcnt = 0;

        foreach (GameObject _button in layer)
        {
            if (_button.GetComponent<Button>().colors.normalColor == Color.red)
            {
                allcnt++;
            }
            if ((_button.GetComponent<Button>().colors.normalColor == Color.red) && nums[k] == 1)
            {
                cnt++;
            }
            k++;
        }

        if (cnt == LvlAmt && allcnt == LvlAmt)
        {
            //Debug.Log("spravne");
        }
        else
        {
            //Debug.Log("ne");
            scrp.GetComponent<LayerManag>().game_over = 1;
        }
    }
}