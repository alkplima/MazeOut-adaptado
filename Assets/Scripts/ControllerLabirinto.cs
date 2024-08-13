using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.SceneManagement;

public class ControllerLabirinto : MonoBehaviour
{
    public RectTransform[] pecas;
    public AudioSource audioPegouItem, audioVoceGanhou, audioFundo;

    Vector2 ScreenDimensions = new Vector2(10,10);
    public RectTransform primeiraPosGrid, ultimaPosGrid;
    public float secsAdjustingScreen = 0.6f;
    internal bool adjustingDimensions = false;
    Coroutine cr_adjustScreen_ref;
    private bool cr_adjust_isRunning = false;
    
    void Awake()
    {
        // Rotina para garantir que eh o unico ControllerJogo em operacao (Singleton).
        if (VariaveisGlobais.atualControllerLabirinto == null)
        {
            VariaveisGlobais.atualControllerLabirinto = this;
            PreparoInicial();
        }
        else Destroy(this.gameObject);
    }

    public void PreparoInicial()
    {
        StartCoroutine(cr_PreparoInicial());
    }

    IEnumerator cr_PreparoInicial()
    {
        yield return new WaitForEndOfFrame();

        //ScreenDimensions = new Vector2(Screen.width, Screen.height);

        if (!PlayerPrefs.HasKey("Velocidade"))
            PlayerPrefs.SetInt("Velocidade", 75);

        if (!PlayerPrefs.HasKey("Arraste"))
            PlayerPrefs.SetInt("Arraste", 5);

        if (!PlayerPrefs.HasKey("Som"))
            PlayerPrefs.SetInt("Som", 0);

        if (!PlayerPrefs.HasKey("Timer"))
            PlayerPrefs.SetInt("Timer", 60);

        if (!PlayerPrefs.HasKey("DataProcessingMode"))
        {
            PlayerPrefs.SetInt("DataProcessingMode", 2);
            VariaveisGlobais.dataProcessingMode = 'D';
        }

        if (!PlayerPrefs.HasKey("ShowHeartRate"))
            PlayerPrefs.SetInt("ShowHeartRate", 0);

        if (PlayerPrefs.GetInt("Som")==1)
            audioFundo.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newScreenDimensions = new Vector2(Screen.width, Screen.height);

        if (newScreenDimensions != ScreenDimensions)
        {
            if (cr_adjustScreen_ref != null)
                StopCoroutine(cr_adjustScreen_ref);
            adjustingDimensions = true;
            ScreenDimensions = newScreenDimensions;
            cr_adjustScreen_ref = StartCoroutine(cr_AguardaEstabilizarDimensoesTela(secsAdjustingScreen));
        }  
        else if (cr_adjust_isRunning == false)
            adjustingDimensions = false;
    }
    public void ReiniciarCena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ResetAllConfigs()
    {
        PlayerPrefs.DeleteAll();
        foreach (var file in Directory.GetFiles(Application.persistentDataPath))
        {
            FileInfo file_info = new FileInfo(file);
            file_info.Delete();
        }
#if ((UNITY_WEBGL) && (!UNITY_EDITOR))
        Application.ExternalEval("FS.syncfs(false, function (err) {})");
#endif
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    IEnumerator cr_AguardaEstabilizarDimensoesTela(float secs)
    {
        cr_adjust_isRunning = true;
        yield return new WaitForSeconds(secs);
        cr_adjust_isRunning = false;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
        adjustingDimensions = false;
    }
}
