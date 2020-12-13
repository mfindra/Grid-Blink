using UnityEngine;
using UnityEngine.UI;

public class ColorManag : MonoBehaviour
{
    public Button[] Buttons;
    public Sprite[] s_Light;
    public Sprite[] s_Dark;
    public Toggle colorToggle;
    public Toggle blindToggle;
    public Toggle soundToggle;
    public Image bg;
    public Image bgLogo;
   
    public void SetToggler(Toggle toggler, string name)
    {
        if (PlayerPrefs.GetInt(name) == 1)
        {   
            toggler.isOn = true;          
        } else
        {
            toggler.isOn = false;           
        } 
    }

    public void Sound()
    {
        if (soundToggle.isOn)
        {
            PlayerPrefs.SetInt("sound", 1);
        }
        else
        {
            PlayerPrefs.SetInt("sound", 0);
        }
    }

    public void BlindMode()
    {
        if (blindToggle.isOn)
        {
            PlayerPrefs.SetInt("blindMode", 1);
        } else
        {
            PlayerPrefs.SetInt("blindMode", 0);
        }
    }

    public void ChangeColor()
    {        
        for (int i = 0; i < Buttons.Length; i++)
        {
            if (colorToggle.isOn)
            {
                Buttons[i].image.sprite = s_Dark[i];
                PlayerPrefs.SetInt("colorMode", 1);
            } else
            {
                Buttons[i].image.sprite = s_Light[i];
                PlayerPrefs.SetInt("colorMode", 0);
            }
        }   
        if (colorToggle.isOn)
        {
            bg.sprite = s_Dark[Buttons.Length];
            bgLogo.sprite = s_Dark[Buttons.Length + 1];
            PlayerPrefs.SetInt("colorMode", 1);
        } else
        {
            bg.sprite = s_Light[Buttons.Length];
            bgLogo.sprite = s_Light[Buttons.Length + 1];
            PlayerPrefs.SetInt("colorMode", 0);
        }

    }
}