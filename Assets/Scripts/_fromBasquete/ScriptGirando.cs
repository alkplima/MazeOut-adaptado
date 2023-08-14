using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptGirando : MonoBehaviour
{
    public float velocidade;
    float v_zeh, v_vertical;

    // Start is called before the first frame update
    void Start()
    {
        v_zeh = 0;
    }

    // Update is called once per frame
    void Update()
    {
        v_zeh += (velocidade*Time.deltaTime);
        transform.rotation = Quaternion.Euler(0, v_zeh, 0);
    }
}

