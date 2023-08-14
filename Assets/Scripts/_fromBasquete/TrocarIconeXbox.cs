using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrocarIconeXbox : MonoBehaviour
{
    // Start is called before the first frame update

    public Sprite spriteNormal, spriteXbox;

    void Start()
    {
        if (VariaveisGlobais.estouNoXbox)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = spriteXbox;
            gameObject.GetComponent<BoxCollider2D>().enabled = false;
        }
        else
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = spriteNormal;
            gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
