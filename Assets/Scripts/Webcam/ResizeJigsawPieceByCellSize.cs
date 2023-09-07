using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizeJigsawPieceByCellSize : MonoBehaviour
{
    public RectTransform cellRef;
    public float TaxaTamanhoPecaPorTamanhoCell = 1;

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, cellRef.rect.width * TaxaTamanhoPecaPorTamanhoCell);
            transform.GetChild(i).GetComponent<RectTransform>().SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, cellRef.rect.height * TaxaTamanhoPecaPorTamanhoCell);
        }
    }
}
