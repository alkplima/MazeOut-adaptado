using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UI_DisplayTimer : MonoBehaviour
{
    public TMP_Text textTimer;

    public float timer;
    private bool isTimer = false;

    [SerializeField] GameObject gameScreenManager;
    private CoinCollectionController coinCollectionController;
 
    // Start is called before the first frame update
    void OnEnable()
    {
        textTimer.text = "";
        StartTimer();
        coinCollectionController = GameObject.FindObjectOfType<CoinCollectionController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isTimer && PaterlandGlobal.autorizadoMovimento)
        {
            // if (timer <= 0.1f)
            // {
            //     // SalvarDadosDaUltimaReta();
            //     // ShowFailureMessage();
            //     ResetTimer();
            //     StopTimer();
            // }
            // else
            // {
                timer += Time.deltaTime;
                DisplayTime();
            // }
        }
    }

    void OnDisable()
    {
        ResetTimer();
        StopTimer();
    }

    void DisplayTime()
    {
        int minutes = Mathf.FloorToInt(timer / 60.0f);
        int seconds = Mathf.FloorToInt(timer - minutes * 60);
        textTimer.text = string.Format("{0:0}:{1:00}", minutes, seconds);
    }

    public void StartTimer()
    {
        StartCoroutine(cr_StartTimer());
    }

    IEnumerator cr_StartTimer()
    {
        yield return new WaitForSeconds(0.2f);
        ResetTimer();
        isTimer = true;
    }

    public void StopTimer()
    {
        isTimer = false;
    }

    public void ResetTimer()
    {
        timer = 0;
    }

    // public void ShowFailureMessage() 
    // {
    //     gameScreenManager.GetComponent<UI_MessageScreen>().ShowFailureModal();
    //     if (VariaveisGlobais.estiloJogoCorrente != "PartidaAvulsa" && VariaveisGlobais.nomePaciente != "")
    //     {
    //         VariaveisGlobais.AtualizarAtributosBuffer(VariaveisGlobais.tamanhoBufferBD);
    //         VariaveisGlobais.conexaoBD.PostData();
    //     }
    // }

    // private void SalvarDadosDaUltimaReta()
    // {
    //     VariaveisGlobais.tempoTotalGasto = PlayerPrefs.GetInt("Timer") - timer;
    //     VariaveisGlobais.tempoTotalReta = Time.time - VariaveisGlobais.tempoInicioReta;

    //     // Registra dados da reta anterior
    //     coinCollectionController.AcrescentarEntradaRelatorio();
    // }
}
