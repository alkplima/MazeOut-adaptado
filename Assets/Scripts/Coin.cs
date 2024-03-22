using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class Coin : MonoBehaviour
{
    [SerializeField] private GameObject coinPointsImage;

    [SerializeField] private AudioClip _clip;

    private CoinCollectionController coinCollectionController;

    CoinCountHUD _uiManager;
    int _rotationSpeed = 50;

    private Vector3[] cantosMoeda = new Vector3[4];
    private Vector3[] cantosOtherSombra = new Vector3[4];

    private float alturaMoeda, larguraMoeda, alturaOther, larguraOther;


    // Start is called before the first frame update
    void OnEnable()
    {
        _uiManager = GameObject.Find("GameScreenManager").GetComponent<CoinCountHUD>();
        coinCollectionController = GameObject.FindObjectOfType<CoinCollectionController>();
        _clip = Resources.Load<AudioClip>("Audios" + Path.DirectorySeparatorChar + "coin");
        ResetGlobalVariables();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0, 0);
    }

    //private void OnTriggerEnter2D(Collider2D other) 
    private void OnTriggerStay2D(Collider2D other)
    {
        if (VariaveisGlobais.atualControllerLabirinto.adjustingDimensions)
            return;

        if (other.tag.StartsWith("Bola")) 
        {
            gameObject.GetComponent<RectTransform>().GetWorldCorners(cantosMoeda);
            other.GetComponent<PieceController>().sombra.GetWorldCorners(cantosOtherSombra);

            alturaMoeda = Mathf.Abs(cantosMoeda[0].y - cantosMoeda[1].y);
            larguraMoeda = Mathf.Abs(cantosMoeda[1].x - cantosMoeda[2].x);

            alturaOther = Mathf.Abs(cantosOtherSombra[0].y - cantosOtherSombra[1].y);
            larguraOther = Mathf.Abs(cantosOtherSombra[1].x - cantosOtherSombra[2].x);

            this.gameObject.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>("Sprites" + Path.DirectorySeparatorChar + "vazioBloco");
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
            _uiManager.CoinCount += 1;
            
            // não grava dados se for partida livre
            if (VariaveisGlobais.estiloJogoCorrente != "PartidaAvulsa")
            {
                // if (!VariaveisGlobais.ehPrimeiraMoedaDoJogo)
                // {
                    VariaveisGlobais.lastCollectedCoinDirection = VariaveisGlobais.currentCollectedCoinDirection;
                    VerificaLado(other);

                    VariaveisGlobais.direcaoReta = VariaveisGlobais.lastCollectedCoinDirection;

                    // Add entrada no relatório se mudou direção/reta
                    if (MudouDirecao())
                    {
                        if (EhPrimeiraMoedaDoJogoQueConta())
                        {
                            VariaveisGlobais.coordenadaX_InicioReta = this.GetComponent<RectTransform>().position.x;
                            VariaveisGlobais.coordenadaY_InicioReta = this.GetComponent<RectTransform>().position.y;
                            VariaveisGlobais.totalMoedasColetadasReta++;
                            VariaveisGlobais.tempoInicioReta = Time.time;
                            VariaveisGlobais.dateTimeInicioPartida = System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss.fff");
                            VariaveisGlobais.numReta = 1;
                        }
                        else // mudou de direção no meio do jogo
                        {
                            // Registra dados da reta anterior
                            coinCollectionController.AcrescentarEntradaRelatorio();
                            VariaveisGlobais.usouAjudaNaReta = 'N';
                            VariaveisGlobais.escalaMaxDaAjuda = 1;
                            VariaveisGlobais.tempoInicioReta = VariaveisGlobais.tempoInicioRetaAux;
                            VariaveisGlobais.tempoTotalReta = 0;
                            VariaveisGlobais.totalMoedasColetadasReta = 2; // incrementa 1 referente ao canto inicial
                            VariaveisGlobais.numReta += 1;
                            VariaveisGlobais.coordenadaX_InicioReta = VariaveisGlobais.coordenadaX_FimReta;
                            VariaveisGlobais.coordenadaY_InicioReta = VariaveisGlobais.coordenadaY_FimReta;
                        }
                    }
                    else
                    {
                        float xAtual = this.GetComponent<RectTransform>().position.x;
                        float yAtual = this.GetComponent<RectTransform>().position.y;

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
                        VariaveisGlobais.tempoInicioRetaAux = Time.time;
                    } 
                    VariaveisGlobais.totalMoedasColetadas++;
                // }
                // else 
                // {
                //     VariaveisGlobais.ehPrimeiraMoedaDoJogo = false;
                // }
            }

            Destroy(this);
        }

        // if (VariaveisGlobais.passedThroughtStart == true) {
        //     // GameObject clone = Instantiate(coinPointsImage, transform.position, Quaternion.identity);
        //     // clone.transform.SetParent(this.transform.parent);
        //     // Destroy(clone.gameObject, 2);
        //     Destroy(this.gameObject);
        // }
    }

    private void VerificaLado(Collider2D other) 
    {
        if (other.GetComponent<PieceController>().enabled)
            if (Vector2.Distance(other.GetComponent<RectTransform>().position, GetComponent<RectTransform>().position) <= (0.8f * other.GetComponent<RectTransform>().rect.width / 2 + GetComponent<RectTransform>().rect.width / 2))
            {
                float alturaParede = alturaMoeda;
                float larguraParede = larguraMoeda;
                float alturaHandGear = 0.7f * alturaOther;
                float larguraHandGear = 0.7f * larguraOther;

                // Verificar se o movimento vem de baixo, de cima, do lado esquerdo ou do lado direito
                float distanciaParaCima = Vector3.Distance(new Vector3(other.GetComponent<RectTransform>().position.x, other.GetComponent<RectTransform>().position.y - (alturaOther / 2), other.GetComponent<RectTransform>().position.z), GetComponent<RectTransform>().position);
                float distanciaParaBaixo = Vector3.Distance(new Vector3(other.GetComponent<RectTransform>().position.x, other.GetComponent<RectTransform>().position.y + (alturaOther / 2), other.GetComponent<RectTransform>().position.z), GetComponent<RectTransform>().position);
                float distanciaParaDireita = Vector3.Distance(new Vector3(other.GetComponent<RectTransform>().position.x + (larguraOther / 2), other.GetComponent<RectTransform>().position.y,    other.GetComponent<RectTransform>().position.z), GetComponent<RectTransform>().position);
                float distanciaParaEsquerda = Vector3.Distance(new Vector3(other.GetComponent<RectTransform>().position.x - (larguraOther / 2), other.GetComponent<RectTransform>().position.y, other.GetComponent<RectTransform>().position.z), GetComponent<RectTransform>().position);

                if ((distanciaParaCima <= distanciaParaBaixo) && (distanciaParaCima <= distanciaParaEsquerda) && (distanciaParaCima <= distanciaParaDireita))
                {
                    coinCollectionController.CountTimePerCoin('C');
                    VariaveisGlobais.currentCollectedCoinDirection = '2';
                }
                else if ((distanciaParaEsquerda <= distanciaParaCima) && (distanciaParaEsquerda <= distanciaParaBaixo) && (distanciaParaEsquerda <= distanciaParaDireita))
                {
                    coinCollectionController.CountTimePerCoin('D');
                    VariaveisGlobais.currentCollectedCoinDirection = '3';
                }
                else if ((distanciaParaBaixo <= distanciaParaCima) && (distanciaParaBaixo <= distanciaParaEsquerda) && (distanciaParaBaixo <= distanciaParaDireita))
                {
                    coinCollectionController.CountTimePerCoin('B');
                    VariaveisGlobais.currentCollectedCoinDirection = '0';
                }
                else if ((distanciaParaDireita <= distanciaParaCima) && (distanciaParaDireita <= distanciaParaEsquerda) && (distanciaParaDireita <= distanciaParaBaixo))
                {
                    coinCollectionController.CountTimePerCoin('E');
                    VariaveisGlobais.currentCollectedCoinDirection = '1';
                }
            }

    }

    private void ResetGlobalVariables()
    {
        VariaveisGlobais.currentCollectedCoinDirection = ' ';
        VariaveisGlobais.totalMoedasColetadasReta = 0;
        VariaveisGlobais.tempoTotalReta = 0;
        VariaveisGlobais.numReta = 0;
        VariaveisGlobais.totalMoedasColetadas = 0;
        VariaveisGlobais.ehPrimeiraMoedaDoJogo = true;
    }

    private bool MudouDirecao()
    {
        return VariaveisGlobais.currentCollectedCoinDirection != VariaveisGlobais.lastCollectedCoinDirection;
    }

    private bool EhPrimeiraMoedaDoJogoQueConta()
    {
        return VariaveisGlobais.currentCollectedCoinDirection != VariaveisGlobais.lastCollectedCoinDirection && VariaveisGlobais.lastCollectedCoinDirection == ' ';
    }
}
