using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizePieceContentByPieceContainer : MonoBehaviour
{
    public RectTransform containerRef;
    public float TaxaTamanhoPecaPorTamanhoContainer = 1;
    public int taxa_Left, taxa_Right, taxa_Top, taxa_Bottom;

    // Update is called once per frame
    void Update()
    {
        transform.GetComponent<RectTransform>().offsetMin = new Vector2(containerRef.rect.width * taxa_Left, containerRef.rect.height * taxa_Bottom);
        transform.GetComponent<RectTransform>().offsetMax = new Vector2(-containerRef.rect.width * taxa_Right, -containerRef.rect.height * taxa_Top);
    }
}
