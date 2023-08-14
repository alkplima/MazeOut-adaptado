using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Diamond : MonoBehaviour
{
    [SerializeField] private GameObject diamondPointsImage;
    [SerializeField] private AudioClip _clip;
    ScoreHUD _uiManager;
    
    // Start is called before the first frame update
    void Start()
    {
        _uiManager = GameObject.Find("Manager").GetComponent<ScoreHUD>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }    
    
    private void OnTriggerEnter2D(Collider2D other) 
    {
        GameObject clone = Instantiate(diamondPointsImage, transform.position, Quaternion.identity);
        Destroy(clone.gameObject, 5);
        _uiManager.Score += 50;
        AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f); 
        Destroy(this.gameObject);
    }
}
