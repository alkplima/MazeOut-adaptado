using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinCollectionController : MonoBehaviour
{
    public bool firstCoin;
    public int coinCountLeftToRight;
    public int coinCountRightToLeft;
    public int coinCountTopToBottom;
    public int coinCountBottomToTop;
    public float timeLeftToRight;
    public float timeRightToLeft;
    public float timeTopToBottom;
    public float timeBottomToTop;
    public float timerTimeFromLastCoin;
    private UI_DisplayTimer displayTimer;

    // Start is called before the first frame update
    void OnEnable()
    {
        displayTimer = GameObject.FindObjectOfType<UI_DisplayTimer>();
        firstCoin = true;
        coinCountTopToBottom = 0;
        coinCountLeftToRight = 0;
        coinCountBottomToTop = 0;
        coinCountRightToLeft = 0;
        timeLeftToRight = 0.0f;
        timeRightToLeft = 0.0f;
        timeTopToBottom = 0.0f;
        timeBottomToTop = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CountTimePerCoin(char lado) 
    {
        float timeWentBy = 0.0f;

        if (firstCoin) {
            firstCoin = false;
            if (displayTimer != null) timeWentBy = PlayerPrefs.GetInt("Timer") - displayTimer.timer;
        }
        else {
            if (displayTimer != null) timeWentBy = timerTimeFromLastCoin - displayTimer.timer;
        }

        if (displayTimer != null) timerTimeFromLastCoin = displayTimer.timer;

        switch (lado)
        {
            case 'C':
                coinCountTopToBottom++;
                timeTopToBottom += timeWentBy;
                break;
            case 'E':
                coinCountLeftToRight++;
                timeLeftToRight += timeWentBy;
                break;
            case 'B':
                coinCountBottomToTop++;
                timeBottomToTop += timeWentBy;
                break;
            case 'D':
                coinCountRightToLeft++;
                timeRightToLeft += timeWentBy;
                break;
            default:
                break;
        }

    }

    void OnDisable() 
    {
        VariaveisGlobais.timePerCoinTopToBottom = timeTopToBottom / coinCountTopToBottom;
        VariaveisGlobais.timePerCoinBottomToTop = timeBottomToTop / coinCountBottomToTop;
        VariaveisGlobais.timePerCoinLeftToRight = timeLeftToRight / coinCountLeftToRight;
        VariaveisGlobais.timePerCoinRightToLeft = timeRightToLeft / coinCountRightToLeft;
    }
}