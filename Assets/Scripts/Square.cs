using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Square : MonoBehaviour
{
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
        // this.gameObject.GetComponent<SpriteRenderer>().color = Color.green;
        Color currentColor = this.gameObject.GetComponent<SpriteRenderer>().color;
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(currentColor.r*1.1f, currentColor.g*1.1f, currentColor.b*1.1f);
        // Destroy(this.gameObject);
    }
}
