using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCubo : MonoBehaviour
{
    public string NomeDoJogador;
    public int Idade;
    float v_horizontal, v_vertical, v_xis, v_yps, v_zeh;

    // Start is called before the first frame update
    void Start()
    {
        v_horizontal = Input.GetAxis("Horizontal");
        v_vertical = Input.GetAxis("Vertical");
        v_xis = 0;
        v_yps = 0;
        v_zeh = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (v_horizontal != Input.GetAxis("Horizontal"))
        {
            /*            Transform trsf = GetComponent<Transform>();

                        v_hor2 = v_horizontal - Input.GetAxis("Horizontal");
                        Debug.Log(v_hor2);
                        transform.rotation = Quaternion.Euler(trsf.rotation.x+v_hor2, trsf.rotation.y, trsf.rotation.z);
            */
                v_xis += Input.GetAxis("Horizontal") * 10;
        }

        if (Input.GetAxis("Vertical")!=0)
        {
            v_yps += Input.GetAxis("Vertical") * 10;
        }

        if (Input.GetKey(KeyCode.Y))
        {
            v_zeh += 10;
        }

        if (Input.GetKey(KeyCode.Z))
        {
            v_zeh += -10;
        }
        transform.rotation = Quaternion.Euler(v_xis, v_yps, v_zeh);
    }
}
