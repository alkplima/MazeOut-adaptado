using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMark : MonoBehaviour
{
    [SerializeField] private GameObject checkMark;

    [SerializeField] private AudioClip _clip;

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
        VariaveisGlobais.passedThroughtStart = true;
        GameObject clone = Instantiate(checkMark, transform.position, Quaternion.identity);
        clone.transform.SetParent(this.transform.parent);
        AudioSource.PlayClipAtPoint(_clip, Camera.main.transform.position, 1f); 
        Destroy(this.gameObject);
    }
}
