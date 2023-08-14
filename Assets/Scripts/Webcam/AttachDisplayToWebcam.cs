using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AttachDisplayToWebcam : MonoBehaviour
{
    public bool isDisplay, isDisplay2;
    void Start()
    {
        if (isDisplay)
            PaterlandGlobal.currentWebcam.Display = transform.GetComponent<RawImage>();
        if (isDisplay2)
            PaterlandGlobal.currentWebcam.Display2 = transform.GetComponent<RawImage>();
    }
}
