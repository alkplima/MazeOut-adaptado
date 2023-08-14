using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InformarPosicao : MonoBehaviour
{
    public RectTransform rectOther;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log("LOCAL "+gameObject.GetComponent<RectTransform>().localPosition);
        Debug.Log("GLOBAL "+gameObject.GetComponent<RectTransform>().position);
        Debug.Log("RECT "+gameObject.GetComponent<RectTransform>().rect.x + ","+ gameObject.GetComponent<RectTransform>().rect.y);

        rectOther.SetPositionAndRotation(gameObject.GetComponent<RectTransform>().position, rectOther.rotation);
    }
}
