using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ScoreHUD : MonoBehaviour {
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text bestScoreText;
    [SerializeField] GameObject scoreChangePrefab;
    [SerializeField] GameObject handGear;
    [SerializeField] Transform scoreParent;
    [SerializeField] RectTransform endPoint;
    [SerializeField] Color colorGreen;
    [SerializeField] Color colorRed;
    [SerializeField] Color colorPurple;
    [SerializeField] Color colorBlue;
    private GameObject scoreChangeInstance;

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

    private void ShowScoreChange (int change)
    {
        if (scoreChangeInstance != null && scoreChangeInstance.activeSelf)
        {
            Destroy(scoreChangeInstance);
        }

        scoreChangeInstance  = Instantiate (scoreChangePrefab, handGear.transform.position, Quaternion.identity);
        scoreChangeInstance.transform.localScale = new Vector3(0.7f, 0.7f, 0.7f);
        scoreChangeInstance.transform.SetParent (scoreParent, false);

        RectTransform rect = scoreChangeInstance.GetComponent<RectTransform> ();

        Text text = scoreChangeInstance.GetComponent<Text> ();

        text.text = /*(change > 0 ? "+ " : "") +*/ change.ToString ();
        text.color = Color.white;

        RawImage rawImage = handGear.GetComponent<RawImage>();

        switch (change) {
            case <10:
                // text.color = colorRed;
                rawImage.color = colorBlue;
                break;
            case <50:
                rawImage.color = colorGreen;
                break;
            case >80:
                rawImage.color = colorPurple;
                break;
            default:
                rawImage.color = colorGreen;
                break;
        }
        // text.color = change > 0 ? colorGreen : colorRed;

        LeanTween.moveZ (rect, handGear.transform.position.z, 1.5f).setOnComplete (() => {
            Destroy (scoreChangeInstance);
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