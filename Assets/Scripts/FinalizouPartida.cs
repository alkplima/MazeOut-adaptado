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

        if (other.tag.StartsWith("Bola") && EstaCentralizadoSobreAArea(other))
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
            else if ((VariaveisGlobais.partidaCorrente == -5) && VariaveisGlobais.estiloJogoCorrente == "Calibracao")
            {
                gameScreenManager.GetComponent<UI_MessageScreen>().ShowEndOfTutorialModal();
            }
            else if (VariaveisGlobais.partidaCorrente == 30 || VariaveisGlobais.contagemPartidasAuto == 9)
            {
                gameScreenManager.GetComponent<UI_MessageScreen>().ShowEndOfExperimentModal();
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

    private bool EstaCentralizadoSobreAArea(Collider2D other)
    {
        Vector3[] cantosFim = new Vector3[4];
        Vector3[] cantosOther = new Vector3[4];

        this.GetComponent<RectTransform>().GetWorldCorners(cantosFim);
        other.GetComponent<RectTransform>().GetWorldCorners(cantosOther);

        Vector2 centroFim = new Vector2((cantosFim[0].x + cantosFim[2].x) / 2f, (cantosFim[0].y + cantosFim[2].y) / 2f);
        Vector2 centroOther = new Vector2((cantosOther[0].x + cantosOther[2].x) / 2f, (cantosOther[0].y + cantosOther[2].y) / 2f);

        float larguraFim = Mathf.Abs(cantosFim[2].x - cantosFim[0].x);
        float alturaFim = Mathf.Abs(cantosFim[2].y - cantosFim[0].y);

        float larguraOther = Mathf.Abs(cantosOther[2].x - cantosOther[0].x);
        float alturaOther = Mathf.Abs(cantosOther[2].y - cantosOther[0].y);

        // Direção de x e y da bola
        Vector2 direcaoXOther = (cantosOther[1] - cantosOther[0]).normalized;
        Vector2 direcaoYOther = (cantosOther[2] - cantosOther[1]).normalized;

        // Converte os cantos do fim para Vector2
        Vector2 cantoFim0 = new Vector2(cantosFim[0].x, cantosFim[0].y);
        Vector2 cantoFim2 = new Vector2(cantosFim[2].x, cantosFim[2].y);

        // Calcula a projeção dos cantos do objeto "fim" nos eixos x e y da bola
        float projetadoX = Vector2.Dot((cantoFim0 - centroOther), direcaoXOther);
        float projetadoY = Vector2.Dot((cantoFim0 - centroOther), direcaoYOther);

        // Verifica se a projeção está dentro da metade da largura e altura do objeto "other"
        if (Mathf.Abs(projetadoX) < larguraOther / 2f && Mathf.Abs(projetadoY) < alturaOther / 2f)
        {
            return true;
        }
        return false;
    }
}