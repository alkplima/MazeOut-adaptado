using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_StaticHandGearRotation : MonoBehaviour
{
    int _rotationSpeed = 150;

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(0, 0, _rotationSpeed * Time.deltaTime, 0);
    }
}
