using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Controller_Ativ01_QuebraCabeca : MonoBehaviour
{
    public RectTransform[] pecas, metas, sombras, posIniciais, pecas_ajuste, sombras_ajuste;
    public AudioSource audioAcertou, audioVoceGanhou;
    public GameObject[] go_Bloquear_2Seg, go_Desativar, go_Ativar;
    Vector2 ScreenDimensions;
    public RectTransform primeiraPosGrid, ultimaPosGrid;
    public Transform gridTestes;
    public GameObject[] coisasADesativarNaOlhadinha;
    public Webcam webcamInstance;
    void Awake()
    {
        // Rotina para garantir que é o único ControllerJogo em operação (Singleton).
        if (VariaveisGlobais.currentControllerQuebraCabeca == null)
        {
            VariaveisGlobais.currentControllerQuebraCabeca = this;
            AtividadesIniciais();
        }
        // Caso a variável flag de restauração esteja setada, iniciar o processo de restauração.
        else Destroy(this.gameObject);
    }

    public void AtividadesIniciais()
    {
        //linha,coluna;linha,coluna...
        // Resgatar posições iniciais das peças
        if (!PlayerPrefs.HasKey("PosicaoInicialPecas"))
            PlayerPrefs.SetString("PosicaoInicialPecas", "3,3;15,3;3,28;15,28");

        if (!PlayerPrefs.HasKey("Velocidade"))
            PlayerPrefs.SetInt("Velocidade", 75);

        if (!PlayerPrefs.HasKey("Arraste"))
            PlayerPrefs.SetInt("Arraste", 5);

#if UNITY_WEBGL
        Application.ExternalEval("FS.syncfs(false, function (err) {})");
        Debug.Log("Sincronia disco - navegador realizada.");
#endif

        string[] posPecas = PlayerPrefs.GetString("PosicaoInicialPecas").Split(';');
        string[][] newPosPecas = new string[posPecas.Length][];

        for (int i = 0; i < newPosPecas.Length; i++)
            newPosPecas[i] = posPecas[i].Split(',');

        for (int i = 0; i < posIniciais.Length; i++)
            posIniciais[i] = primeiraPosGrid.parent.GetChild(System.Convert.ToInt32(newPosPecas[i][0]) * 32 + System.Convert.ToInt32(newPosPecas[i][1])).GetComponent<RectTransform>();

        // Resgatar posições iniciais das metas
        if (!PlayerPrefs.HasKey("PosicaoInicialMetas"))
            PlayerPrefs.SetString("PosicaoInicialMetas", "5,14;8,14;5,17;8,17");

        string[] posMetas = PlayerPrefs.GetString("PosicaoInicialMetas").Split(';');
        string[][] newPosMetas = new string[posMetas.Length][];

        for (int i = 0; i < newPosMetas.Length; i++)
            newPosMetas[i] = posMetas[i].Split(',');

        for (int i = 0; i < metas.Length; i++)
            metas[i] = primeiraPosGrid.parent.GetChild(System.Convert.ToInt32(newPosMetas[i][0]) * 32 + System.Convert.ToInt32(newPosMetas[i][1])).GetComponent<RectTransform>();

    }
    public void GravarPosIniciais()
    {
        string novoPosInicialPecas = "";

        for (int i = 0; i < posIniciais.Length; i++)
        {
            novoPosInicialPecas += Mathf.FloorToInt(posIniciais[i].GetSiblingIndex() / 32).ToString() + "," + Mathf.FloorToInt(posIniciais[i].GetSiblingIndex() % 32).ToString();
            if (i < posIniciais.Length - 1)
                novoPosInicialPecas += ";";
        }

        string novoPosInicialMetas = "";

        for (int i = 0; i < metas.Length; i++)
        {
            novoPosInicialMetas += Mathf.FloorToInt(metas[i].GetSiblingIndex() / 32).ToString() + "," + Mathf.FloorToInt(metas[i].GetSiblingIndex() % 32).ToString();
            if (i < metas.Length - 1)
                novoPosInicialMetas += ";";
        }

        PlayerPrefs.SetString("PosicaoInicialPecas", novoPosInicialPecas);
        PlayerPrefs.SetString("PosicaoInicialMetas", novoPosInicialMetas);

#if UNITY_WEBGL
        Application.ExternalEval("FS.syncfs(false, function (err) {})");
        Debug.Log("Sincronia disco - navegador realizada.");
#endif

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

        for (int i = 0; i < sombras.Length; i++)
        {
            yield return new WaitForEndOfFrame();
            //Debug.Log("Ajeitando sombra numero " + i);
            sombras[i].position = metas[i].position;
        }
        for (int i = 0; i < posIniciais.Length; i++)
        {
            //Debug.Log("Ajeitando peça numero " + i);
            yield return new WaitForEndOfFrame();
            pecas[i].position = posIniciais[i].position;
        }

        ScreenDimensions = new Vector2(Screen.width, Screen.height);

        yield return new WaitForSeconds(0.5f);
        PaterlandGlobal.autorizadoMovimento = true;

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
            foreach (GameObject go in coisasADesativarNaOlhadinha)
                go.SetActive(true);
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

            foreach (GameObject go in coisasADesativarNaOlhadinha)
                go.SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Vector2 newScreenDimensions = new Vector2(Screen.width, Screen.height);
        if (newScreenDimensions != ScreenDimensions)
        {
            Debug.Log("Mudou resolução: " + Screen.width + "x" + Screen.height + "controller.primeiraPosGrid.rect.width = " + primeiraPosGrid.rect.width + " controller.primeiraPosGrid.rect.height = " + primeiraPosGrid.rect.height);
            for (int i = 0; i < sombras.Length; i++)
            {
                //Debug.Log("Ajeitando sombra numero " + i);
                sombras[i].position = metas[i].position;
            }
            ScreenDimensions = newScreenDimensions;
        }

    }

    public void VerificarPecas()
    {
        bool tudoOK = true;

        for (int i = 0; i < pecas.Length; i++)
            if (pecas[i].GetComponent<PieceController>().enabled)
                tudoOK = false;

        if (tudoOK)
            conclusaoQuebraCabeca();

    }

    public void conclusaoQuebraCabeca()
    {
        // StartCoroutine(ganhouNoQuebraCabeca());
    }

    IEnumerator ganhouNoQuebraCabeca()
    {
        foreach (GameObject go in go_Bloquear_2Seg)
            go.SetActive(false);

        yield return new WaitForSeconds(2);

        audioVoceGanhou.Play();

        foreach (GameObject go in go_Desativar)
            go.SetActive(false);
        foreach (GameObject go in go_Ativar)
            go.SetActive(true);
    }

    public void VerificarPecaIndividual(PieceController piece)
    {
        int indicePecaMeta = -1;
        // for (int i = 0; i < pecas.Length; i++)
        for (int i = 0; i < 4; i++)
            if (pecas[i].name == piece.gameObject.name)
                indicePecaMeta = i;

        if (indicePecaMeta != -1)
            if (Vector2.Distance(pecas[indicePecaMeta].position, metas[indicePecaMeta].position) < primeiraPosGrid.rect.width)
            {
                audioAcertou.Play();
                pecas[indicePecaMeta].position = metas[indicePecaMeta].position;
                pecas[indicePecaMeta].GetComponent<PieceController>().StopAllCoroutines();
                pecas[indicePecaMeta].GetComponent<PieceController>().enabled = false;
                VerificarPecas();
            }
    }

    public void ReiniciarCena()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
}
