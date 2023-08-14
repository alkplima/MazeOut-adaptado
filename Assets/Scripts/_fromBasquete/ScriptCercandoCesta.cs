using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCercandoCesta : MonoBehaviour
{
    public float velocidade;
    float v_zeh, v_vertical;
    public bool sentido;
    

    // Start is called before the first frame update
    void Start()
    {
        v_zeh = transform.rotation.eulerAngles.y;
    }

    // Update is called once per frame
    void Update()
    {
        if (v_zeh >= 90)
        { sentido = false; }
        else if ((v_zeh <= -90) || (v_zeh == 270))
        { sentido = true; }

        if (sentido==true)
        { v_zeh += velocidade * Time.deltaTime * 45; }
        else
        { v_zeh -= velocidade * Time.deltaTime * 45; }
                     
        transform.rotation = Quaternion.Euler(0, v_zeh, 0);
    }
}
