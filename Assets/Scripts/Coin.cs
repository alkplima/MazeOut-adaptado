using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.IO;

public class Coin : MonoBehaviour
{
    [SerializeField] private GameObject coinPointsImage;

    [SerializeField] private AudioClip _clip;

    ScoreHUD _uiManager;
    int _rotationSpeed = 50;

    // Start is called before the first frame update
    void OnEnable()
    {
        // _uiManager = GameObject.Find("GameScreen").GetComponent<ScoreHUD>();
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

            // this.gameObject.GetComponent<CelulaInfo>().selecionadoSprite = null;
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
}
