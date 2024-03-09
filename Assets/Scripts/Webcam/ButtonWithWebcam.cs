using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.UI;
using UnityEngine.UI;

public class ButtonWithWebcam : MonoBehaviour{
    public bool changeColor, changeToggle, activateButton;
    Image img;
    public Color32 colorHit = new Color32(163, 255, 96, 255);
    public Color32 colorMiss = new Color32(255, 106, 96, 255);
    
    void Start(){
        img = GetComponent<Image>();
    }

    void Update(){
        RectTransform rt = GetComponent<RectTransform>();
        int x = (int)(Mathf.Abs(rt.anchoredPosition.x) * PaterlandGlobal.currentWebcam.scaleH),
            y = (int)(Mathf.Abs(rt.anchoredPosition.y) * PaterlandGlobal.currentWebcam.scaleV),
            s = (int)(100 * PaterlandGlobal.currentWebcam.scaleH);

        if (PaterlandGlobal.currentWebcam.checkArea(x, y, (int)Mathf.Floor(rt.rect.width * PaterlandGlobal.currentWebcam.scaleH), (int)Mathf.Floor(rt.rect.height * PaterlandGlobal.currentWebcam.scaleV)))
        {
            if (changeColor)
                img.color = colorHit;

            if (changeToggle)
                transform.GetComponent<Toggle>().isOn = true;
            if (activateButton)
                transform.GetComponent<Button>().onClick.Invoke();
            if (gameObject.GetComponent<CaminhoWebcamItemController>())
                if (PaterlandGlobal.autorizadoMovimento)
                    if (gameObject.GetComponent<CaminhoWebcamItemController>().isActiveAndEnabled)
                        if (VariaveisGlobais.atualControllerLabirinto.adjustingDimensions == false)
                            gameObject.GetComponent<CaminhoWebcamItemController>().Verificar();
        }
        else
        {
            if (changeColor)
                img.color = colorMiss;
        }              
    }

    public void ChangeOpacityColorsHitMiss(bool changeColor)
    {
        if (changeColor)
        {
            colorHit = new Color32(colorHit.r, colorHit.g, colorHit.b, 128);
            colorMiss = new Color32(colorMiss.r, colorMiss.g, colorMiss.b, 128);
        }
        else
        {
            colorHit = new Color32(colorHit.r, colorHit.g, colorHit.b, 0);
            colorMiss = new Color32(colorMiss.r, colorMiss.g, colorMiss.b, 0);
        }
    }
}
