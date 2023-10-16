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

    ScoreHUD _uiManager;
    int _rotationSpeed = 50;

    // Start is called before the first frame update
    void OnEnable()
    {
        // _uiManager = GameObject.Find("GameScreen").GetComponent<ScoreHUD>();
        coinCollectionController = GameObject.FindObjectOfType<CoinCollectionController>();
        _clip = Resources.Load<AudioClip>("Audios" + Path.DirectorySeparatorChar + "coin");
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, _rotationSpeed * Time.deltaTime, 0, 0);
    }    
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        if (other.tag.StartsWith("Bola")) {
            VerificaLado(other);

            this.gameObject.GetComponent<UnityEngine.UI.Image>().sprite = Resources.Load<Sprite>("Sprites" + Path.DirectorySeparatorChar + "vazioBloco");
            // _uiManager.Score += 1;
            AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f);
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
                float alturaParede = gameObject.GetComponent<RectTransform>().rect.height;
                float larguraParede = gameObject.GetComponent<RectTransform>().rect.width;
                float alturaHandGear = 0.7f * other.gameObject.GetComponent<RectTransform>().rect.height;
                float larguraHandGear = 0.7f * other.gameObject.GetComponent<RectTransform>().rect.width;

                // Verificar se o movimento vem de baixo, de cima, do lado esquerdo ou do lado direito

                float distanciaParaCima = Vector3.Distance(new Vector3(other.GetComponent<RectTransform>().position.x, other.GetComponent<RectTransform>().position.y - (other.GetComponent<RectTransform>().rect.height / 2), other.GetComponent<RectTransform>().position.z), GetComponent<RectTransform>().position);
                float distanciaParaBaixo = Vector3.Distance(new Vector3(other.GetComponent<RectTransform>().position.x, other.GetComponent<RectTransform>().position.y + (other.GetComponent<RectTransform>().rect.height / 2), other.GetComponent<RectTransform>().position.z), GetComponent<RectTransform>().position);
                float distanciaParaDireita = Vector3.Distance(new Vector3(other.GetComponent<RectTransform>().position.x + (other.GetComponent<RectTransform>().rect.width / 2), other.GetComponent<RectTransform>().position.y,    other.GetComponent<RectTransform>().position.z), GetComponent<RectTransform>().position);
                float distanciaParaEsquerda = Vector3.Distance(new Vector3(other.GetComponent<RectTransform>().position.x - (other.GetComponent<RectTransform>().rect.width / 2), other.GetComponent<RectTransform>().position.y, other.GetComponent<RectTransform>().position.z), GetComponent<RectTransform>().position);

                if ((distanciaParaCima <= distanciaParaBaixo) && (distanciaParaCima <= distanciaParaEsquerda) && (distanciaParaCima <= distanciaParaDireita))
                {
                    coinCollectionController.CountTimePerCoin('C');                                
                }
                else if ((distanciaParaEsquerda <= distanciaParaCima) && (distanciaParaEsquerda <= distanciaParaBaixo) && (distanciaParaEsquerda <= distanciaParaDireita))
                {
                    coinCollectionController.CountTimePerCoin('E');
                }
                else if ((distanciaParaBaixo <= distanciaParaCima) && (distanciaParaBaixo <= distanciaParaEsquerda) && (distanciaParaBaixo <= distanciaParaDireita))
                {
                    coinCollectionController.CountTimePerCoin('B');
                }
                else if ((distanciaParaDireita <= distanciaParaCima) && (distanciaParaDireita <= distanciaParaEsquerda) && (distanciaParaDireita <= distanciaParaBaixo))
                {
                    coinCollectionController.CountTimePerCoin('D');
                }
            }

    }
}
