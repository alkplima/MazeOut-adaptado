using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_DisplayTimer : MonoBehaviour
{
    public TMP_Text textTimer;

    private float timer;
    private bool isTimer = false;

    [SerializeField] GameObject gameScreenManager;
 
    // Start is called before the first frame update
    void OnEnable()
    {
        StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimer)
        {
            if (timer <= 0.1f)
            {
                ShowFailureMessage();
                ResetTimer();
                StopTimer();
            }
            else
            {
                timer -= Time.deltaTime;
                DisplayTime();
            }
        }
    }

    void DisplayTime()
    {
        int minutes = Mathf.FloorToInt(timer / 60.0f);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        textTimer.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    public void StartTimer()
    {
        ResetTimer();
        isTimer = true;
    }

    public void StopTimer()
    {
        isTimer = false;
    }

    public void ResetTimer()
    {
        timer = PlayerPrefs.GetInt("Timer");
    }

    public void ShowFailureMessage() 
    {
        gameScreenManager.GetComponent<UI_MessageScreen>().ShowFailureModal();
    }
}
