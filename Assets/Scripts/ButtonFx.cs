using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonFx : MonoBehaviour
{
    public AudioSource soundFx;
    public AudioClip clickFx;

    public void ClickSound()
    {        
       if (PlayerPrefs.GetInt("sound") == 0)
        {
            soundFx.mute = true;          
        } else
        {
            soundFx.mute = false;
            soundFx.PlayOneShot(clickFx);            
        }      
    }
    
}
