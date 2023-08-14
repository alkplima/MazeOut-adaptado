using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreHUD : MonoBehaviour {
    [SerializeField] TMP_Text scoreText;
    [SerializeField] GameObject scoreChangePrefab;
    [SerializeField] Transform scoreParent;
    [SerializeField] RectTransform endPoint;

    [SerializeField] Color colorGreen;
    [SerializeField] Color colorRed;
    [SerializeField] Color colorPurple;
    [SerializeField] Color colorBlue;

    int score = 0;

    private void Awake () {
        UpdateHUD ();
    }

    public int Score {
        get {
            return score;
        }

        set {
            ShowScoreChange (value - score);
            score = value;
            UpdateHUD ();
        }
    }

    private void ShowScoreChange (int change) {
        var inst = Instantiate (scoreChangePrefab, Vector3.zero, Quaternion.identity);
        inst.transform.SetParent (scoreParent, false);

        RectTransform rect = inst.GetComponent<RectTransform> ();

        Text text = inst.GetComponent<Text> ();

        text.text = (change > 0 ? "+ " : "") + change.ToString ();

        switch (change) {
            case <0:
                text.color = colorRed;
                break;
            case 1:
                text.color = colorGreen;
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
        scoreText.text = "Score: " + score.ToString();

    }
}