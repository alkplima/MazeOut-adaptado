using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResizePieceImagePartByPieceContainer : MonoBehaviour
{
    public RectTransform containerRef;
    public float PercentagemMargem_Left, PercentagemMargem_Right, PercentagemMargem_Top, PercentagemMargem_Bottom;

    // Update is called once per frame
    void Update()
    {
        transform.GetComponent<RectTransform>().offsetMin = new Vector2(containerRef.rect.width * PercentagemMargem_Left, containerRef.rect.height * PercentagemMargem_Bottom);
        transform.GetComponent<RectTransform>().offsetMax = new Vector2(-containerRef.rect.width * PercentagemMargem_Right, -containerRef.rect.height * PercentagemMargem_Top);
    }
}
