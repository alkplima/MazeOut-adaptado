using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class CoinCountHUD : MonoBehaviour {
    [SerializeField] TMP_Text coinCountText;
    [SerializeField] GameObject coinCountChangePrefab;
    [SerializeField] Transform coinCountParent;
    [SerializeField] RectTransform endPoint;

    [SerializeField] Color colorGreen;
    [SerializeField] Color colorRed;
    [SerializeField] Color colorPurple;
    [SerializeField] Color colorBlue;
    [SerializeField] Color colorOrange;

    int coinCount = 0;

    private void OnEnable () {
        coinCount = 0;
        Invoke ("UpdateHUD", 0.3f);
    }

    private void Awake () {
        UpdateHUD ();
    }

    public int CoinCount {
        get {
            return coinCount;
        }

        set {
            // ShowCoinCountChange (value - coinCount);
            coinCount = value;
            UpdateHUD ();
        }
    }

    private void ShowCoinCountChange (int change) {
        var inst = Instantiate (coinCountChangePrefab, Vector3.zero, Quaternion.identity);
        inst.transform.SetParent (coinCountParent, false);

        RectTransform rect = inst.GetComponent<RectTransform> ();

        Text text = inst.GetComponent<Text> ();

        text.text = (change > 0 ? "+ " : "") + change.ToString ();

        switch (change) {
            case <0:
                text.color = colorRed;
                break;
            case 1:
                // text.color = colorGreen;
                text.color = colorOrange;
                break;
            case 50:
                text.color = colorPurple;
                break;
            default:
                text.color = colorGreen;
                break;
        }
        // text.color = change > 0 ? colorGreen : colorRed;

        LeanTween.moveY (rect, endPoint.anchoredPosition.y, 1.5f).setOnComplete (() => {
            Destroy (inst);
        });
        LeanTween.alphaText (rect, 0.25f, 1.5f);
    }

    private void UpdateHUD () {
        coinCountText.text = coinCount.ToString() + "/" + VariaveisGlobais.totalMoedasNaPartida.ToString();
    }
}