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

    Vector2 ScreenDimensions;
    public RectTransform primeiraPosGrid, ultimaPosGrid;
    public Webcam webcamInstance;
    void Awake()
    {
        // Rotina para garantir que � o �nico ControllerJogo em opera��o (Singleton).
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

        ScreenDimensions = new Vector2(Screen.width, Screen.height);

        if (!PlayerPrefs.HasKey("Velocidade"))
            PlayerPrefs.SetInt("Velocidade", 75);

        if (!PlayerPrefs.HasKey("Arraste"))
            PlayerPrefs.SetInt("Arraste", 5);

        if (!PlayerPrefs.HasKey("Som"))
            PlayerPrefs.SetInt("Som", 0);

        if (!PlayerPrefs.HasKey("Timer"))
            PlayerPrefs.SetInt("Timer", 60);

        if (PlayerPrefs.GetInt("Som")==1)
            audioFundo.Play();
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newScreenDimensions = new Vector2(Screen.width, Screen.height);
        if (newScreenDimensions != ScreenDimensions)
            ScreenDimensions = newScreenDimensions;
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
}
