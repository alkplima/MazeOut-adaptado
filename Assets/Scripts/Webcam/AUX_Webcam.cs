using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AUX_Webcam : MonoBehaviour
{
    public RawImage Display, Display2;

    private void OnEnable()
    {
        PaterlandGlobal.currentWebcam.Display = Display;
        PaterlandGlobal.currentWebcam.Display2 = Display2;
        PaterlandGlobal.currentWebcam.AcionarDisplays();
    }

    private void OnDisable()
    {
        PaterlandGlobal.currentWebcam.Display = null;
        PaterlandGlobal.currentWebcam.Display2 = null;
        PaterlandGlobal.currentWebcam.AcionarDisplays();
    }
}
