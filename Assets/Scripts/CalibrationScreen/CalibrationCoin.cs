using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CalibrationCoin : MonoBehaviour
{
    [SerializeField] private GameObject coinPointsImage;

    [SerializeField] private AudioClip _clip;

    ScoreHUD _uiManager;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }    
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        GameObject clone = Instantiate(coinPointsImage, transform.position, Quaternion.identity);
        clone.transform.SetParent(this.transform.parent);
        Destroy(clone.gameObject, 2);
        AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f); 
        Destroy(this.gameObject);
    }
}
