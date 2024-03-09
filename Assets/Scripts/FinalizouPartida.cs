using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinalizouPartida : MonoBehaviour
{
    GameObject gameScreenManager;
    private CoinCollectionController coinCollectionController;
    private float alturaFim, larguraFim, alturaOther, larguraOther;

    void OnEnable()
    {
        gameScreenManager = GameObject.Find("GameScreenManager");
        coinCollectionController = GameObject.FindObjectOfType<CoinCollectionController>();
    }

    //private void OnTriggerEnter2D(Collider2D other)
    private void OnTriggerStay2D(Collider2D other)
    {
        NovaDeteccao(other);
    }

    private void NovaDeteccao(Collider2D other)
    {
        if (VariaveisGlobais.atualControllerLabirinto.adjustingDimensions)
            return;

        if (other.tag.StartsWith("Bola"))
        {
            if (VariaveisGlobais.estiloJogoCorrente != "PartidaAvulsa" && VariaveisGlobais.nomePaciente != "")
            {
                SalvarDadosDaUltimaReta();
                VariaveisGlobais.AtualizarAtributosBuffer(VariaveisGlobais.tamanhoBufferBD);
                VariaveisGlobais.conexaoBD.PostData();
            }
            if (VariaveisGlobais.partidaCorrente == 14 && VariaveisGlobais.estiloJogoCorrente == "Trilha")
            {
                gameScreenManager.GetComponent<UI_MessageScreen>().ShowEndOfChallengeModal();
            }
            if (VariaveisGlobais.partidaCorrente == -7 && VariaveisGlobais.estiloJogoCorrente == "Calibracao")
            {
                gameScreenManager.GetComponent<UI_MessageScreen>().ShowEndOfTutorialModal();
            }
            else
            {
                gameScreenManager.GetComponent<UI_MessageScreen>().ShowSuccessModal();
            }
        }
    }

    private void SalvarDadosDaUltimaReta()
    {
        VariaveisGlobais.direcaoReta = VariaveisGlobais.currentCollectedCoinDirection;

        float xAtual = this.GetComponent<RectTransform>().position.x;
        float yAtual = this.GetComponent<RectTransform>().position.y;

        VariaveisGlobais.totalMoedasColetadas++;
        VariaveisGlobais.totalMoedasColetadasReta++;
        VariaveisGlobais.coordenadaX_FimReta = xAtual;
        VariaveisGlobais.coordenadaY_FimReta = yAtual;
        if (xAtual > VariaveisGlobais.coordenadaX_Maxima)
            VariaveisGlobais.coordenadaX_Maxima = xAtual;
        if (yAtual > VariaveisGlobais.coordenadaY_Maxima)
            VariaveisGlobais.coordenadaY_Maxima = yAtual;
        if (xAtual < VariaveisGlobais.coordenadaX_Minima)
            VariaveisGlobais.coordenadaX_Minima = xAtual;
        if (yAtual < VariaveisGlobais.coordenadaY_Minima)
            VariaveisGlobais.coordenadaY_Minima = yAtual;
        VariaveisGlobais.tempoTotalReta = Time.time - VariaveisGlobais.tempoInicioReta;
        VariaveisGlobais.tempoTotalGasto = coinCollectionController.displayTimer.timer;

        // Registra dados da reta anterior
        coinCollectionController.AcrescentarEntradaRelatorio();
    }
}