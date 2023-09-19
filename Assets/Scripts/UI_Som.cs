using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_Som : MonoBehaviour
{
    public AudioSource som;

    public void ligaDesligaSom()
    {
        if (PlayerPrefs.GetInt("Som") == 1)
            PlayerPrefs.SetInt("Som", 0);
        else PlayerPrefs.SetInt("Som", 1);

        if (PlayerPrefs.GetInt("Som") == 1)
            som.Play();
        else som.Stop();
    }
}
