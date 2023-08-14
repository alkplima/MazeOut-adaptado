using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptMovimentarBola : MonoBehaviour
{
    public float v_xis, v_yps, v_zeh;
    public bool lançamento;
    Rigidbody rb;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        //lançamento = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (lançamento == true)
        {
            rb.useGravity = true;
            //rb.AddForce(new Vector3(60, 400, 290), ForceMode.Acceleration);
            rb.AddForce(new Vector3(v_xis, v_yps, v_zeh), ForceMode.Acceleration);
            lançamento = false;
        }
        
    }
}
