using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class AuxiliarGridLayoutSizeItems : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        CalcularTamanhoCell();
        //gameObject.GetComponent<GridLayoutGroup>().constraintCount
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {
        if (transform.GetChild(0).GetComponent<SetPositionItem>())
            transform.GetChild(0).GetComponent<SetPositionItem>().UpdateColorsButtons();
    }

    public void onResize()
    {
        CalcularTamanhoCell();
    }

    void CalcularTamanhoCell()
    {
        gameObject.GetComponent<GridLayoutGroup>().cellSize = new Vector2(
    gameObject.GetComponent<RectTransform>().rect.width / gameObject.GetComponent<GridLayoutGroup>().constraintCount,
    gameObject.GetComponent<RectTransform>().rect.width / gameObject.GetComponent<GridLayoutGroup>().constraintCount);
    }
}
