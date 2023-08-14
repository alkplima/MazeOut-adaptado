using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScriptCamera : MonoBehaviour
{
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
        if (Input.GetKey(KeyCode.B))
        {
            v_xis += Input.GetAxis("Horizontal") * 10;
        }

        if (Input.GetKey(KeyCode.M))
        {
            v_yps += Input.GetAxis("Vertical") * 10;
        }

        if (Input.GetKey(KeyCode.N))
        {
            v_zeh += 10;
        }
        transform.rotation = Quaternion.Euler(v_xis, v_yps, v_zeh);
    }
}