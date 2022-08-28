using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveSettingFon : MonoBehaviour
{    
    private AudioSource audios;
    private string key = "SoundFon";
    private int i;

    private void Start()
    {
        audios = GetComponent<AudioSource>();
        
            i = PlayerPrefs.GetInt(key);
            if (i == 0) audios.enabled = false;            
            else audios.enabled = true;        
    }    
}
