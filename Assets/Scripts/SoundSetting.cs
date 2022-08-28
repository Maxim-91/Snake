using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundSetting : MonoBehaviour
{    
    private SpriteRenderer spriteR;
    private string key;
    private string pathOff, pathOn;
    private int i;

    public GameObject soundObj;
    private AudioSource audios;

    private void Start()
    {
        spriteR = GetComponent<SpriteRenderer>();
        key = spriteR.name;
        pathOff = "Sprites/" + spriteR.name + "Off";
        pathOn = "Sprites/" + spriteR.name + "On";
        audios = soundObj.GetComponent<AudioSource>();

        if (PlayerPrefs.HasKey(key))
        { 
            i = PlayerPrefs.GetInt(key);
            if (i == 0)
            {
                audios.enabled = false;
                spriteR.sprite = Resources.Load<Sprite>(pathOff);
            }
            else
            {
                audios.enabled = true;
                spriteR.sprite = Resources.Load<Sprite>(pathOn);
            }
        }
        else
        {
            i = 1;
            audios.enabled = true;
            spriteR.sprite = Resources.Load<Sprite>(pathOn);
            PlayerPrefs.SetInt(key, i);
        }
    }

    private void OnMouseDown()
    {
        if (i == 0)
        {
            i = 1;
            audios.enabled = true;
            spriteR.sprite = Resources.Load<Sprite>(pathOn);
        }
        else
        {
            i = 0;
            audios.enabled = false;
            spriteR.sprite = Resources.Load<Sprite>(pathOff);
        }
            
        PlayerPrefs.SetInt(key, i);        
    }
}
