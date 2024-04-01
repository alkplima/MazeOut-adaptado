using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreHUD : MonoBehaviour {
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text bestScoreText;
    [SerializeField] GameObject scoreChangePrefab;
    [SerializeField] Transform scoreParent;
    [SerializeField] RectTransform endPoint;

    [SerializeField] Color colorGreen;
    [SerializeField] Color colorRed;
    [SerializeField] Color colorPurple;
    [SerializeField] Color colorBlue;

    int score = 0;

    private void OnEnable () {
        score = 0;
        VariaveisGlobais.scoreFinal = 0;
        VariaveisGlobais.scoreRecorde = PlayerPrefs.HasKey("ScoreRecorde") ? PlayerPrefs.GetInt("ScoreRecorde") : 0;
        Invoke ("UpdateHUD", 0.3f);
    }

    private void Awake () {
        UpdateHUD ();
    }

    public int Score {
        get {
            return score;
        }

        set {
            if (value < 1) value = 1;
            ShowScoreChange (value - score);
            VariaveisGlobais.scoreFinal = value;
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
            case <10:
                // text.color = colorRed;
                text.color = colorRed;
                break;
            case <50:
                text.color = colorGreen;
                break;
            case >80:
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
        scoreText.text = score.ToString();

        if (score > VariaveisGlobais.scoreRecorde)
        {
            VariaveisGlobais.scoreRecorde = score;
            PlayerPrefs.SetInt("ScoreRecorde", VariaveisGlobais.scoreRecorde);
        }

        bestScoreText.text = "Your best: " + VariaveisGlobais.scoreRecorde.ToString();
    }
}