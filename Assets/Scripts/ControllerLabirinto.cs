using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ControllerLabirinto : MonoBehaviour
{
    public RectTransform[] pecas, metas, posIniciais, pecas_ajuste, sombras_ajuste;
    public AudioSource audioPegouItem, audioVoceGanhou;
    public GameObject[] go_Bloquear_2Seg, go_Desativar, go_Ativar;
    Vector2 ScreenDimensions;
    public RectTransform primeiraPosGrid, ultimaPosGrid;
    public Transform gridTestes;
    public Webcam webcamInstance;
    void Awake()
    {
        // Rotina para garantir que é o único ControllerJogo em operação (Singleton).
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
    public void VerPecasAjuste()
    {
        StartCoroutine(cr_PecasAjuste());
    }

    IEnumerator cr_PreparoInicial()
    {
        PaterlandGlobal.autorizadoMovimento = false;

        yield return new WaitForEndOfFrame();

        for (int i = 0; i < posIniciais.Length; i++)
        {
            yield return new WaitForEndOfFrame();
            pecas[i].position = posIniciais[i].position;
        }

        ScreenDimensions = new Vector2(Screen.width, Screen.height);

        yield return new WaitForSeconds(0.5f);
        PaterlandGlobal.autorizadoMovimento = true;

        if (!PlayerPrefs.HasKey("Velocidade"))
            PlayerPrefs.SetInt("Velocidade", 75);

        if (!PlayerPrefs.HasKey("Arraste"))
            PlayerPrefs.SetInt("Arraste", 5);
    }


    IEnumerator cr_PecasAjuste()
    {
        yield return new WaitForEndOfFrame();

        if (pecas_ajuste[0].gameObject.activeSelf)
        {
            for (int i = 0; i < sombras_ajuste.Length; i++)
                sombras_ajuste[i].gameObject.SetActive(false);
            for (int i = 0; i < pecas_ajuste.Length; i++)
                pecas_ajuste[i].gameObject.SetActive(false);

        }
        else
        {
            for (int i = 0; i < sombras_ajuste.Length; i++)
            {
                sombras_ajuste[i].gameObject.SetActive(true);
                sombras_ajuste[i].position = gridTestes.GetChild(metas[i].GetSiblingIndex()).position;
            }
            for (int i = 0; i < pecas_ajuste.Length; i++)
            {
                pecas_ajuste[i].gameObject.SetActive(true);
                yield return new WaitForEndOfFrame();
                pecas_ajuste[i].position = gridTestes.GetChild(posIniciais[i].GetSiblingIndex()).position;
            }

        }
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
}
