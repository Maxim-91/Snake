using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuSaveSetting : MonoBehaviour
{    
    private SpriteRenderer spriteR;
    private string key;
    private string pathOff, pathOn;
    private int i;

    private void Start()
    {
        spriteR = GetComponent<SpriteRenderer>();
        key = spriteR.name;
        pathOff = "Sprites/" + spriteR.name + "Off";
        pathOn = "Sprites/" + spriteR.name + "On";

        if (PlayerPrefs.HasKey(key))
        { 
            i = PlayerPrefs.GetInt(key);
            if (i == 0) spriteR.sprite = Resources.Load<Sprite>(pathOff);            
            else spriteR.sprite = Resources.Load<Sprite>(pathOn);
        }
        else
        {
            i = 1;
            spriteR.sprite = Resources.Load<Sprite>(pathOn);
            PlayerPrefs.SetInt(key, i);
        }
    }

    private void OnMouseDown()
    {
        if (i == 0)
        {
            i = 1;
            spriteR.sprite = Resources.Load<Sprite>(pathOn);
        }
        else
        {
            i = 0;
            spriteR.sprite = Resources.Load<Sprite>(pathOff);
        }
            
        PlayerPrefs.SetInt(key, i);        
    }
}
