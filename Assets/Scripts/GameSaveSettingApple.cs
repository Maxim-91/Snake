using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveSettingApple : MonoBehaviour
{    
    private AudioSource audios;
    private string key = "SoundApple";
    private int i;

    private void Start()
    {
        audios = GetComponent<AudioSource>();
        
        i = PlayerPrefs.GetInt(key);
        if (i == 0) audios.enabled = false;            
        else audios.enabled = true;        
    }    
}
