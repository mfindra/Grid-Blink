using UnityEngine;
using UnityEngine.UI;

public class ColorManag : MonoBehaviour
{   
    public Button[] Buttons;    // list of buttons which change color
    public Sprite[] s_Light;    // list of light sprites
    public Sprite[] s_Dark;     // list of dark sprites
    public Toggle colorToggle; 
    public Toggle blindToggle;
    public Toggle soundToggle;
    public Image bg;
    public Image bgLogo;
   
    // set toggle to state from previous session
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

    // set playerPrefs for sound
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

    // set playerPrefs for color blind mode
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

    // set playerPrefs for dark mode
    public void ChangeColor()
    {      
        // set color mode of buttons
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

        // set color mode for background
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