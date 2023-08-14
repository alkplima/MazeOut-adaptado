using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentarPeca : MonoBehaviour
{
    public Transform pecaRef;
    public Vector3 movimentacao = new Vector3(52.42857142857143f, 52.42857142857143f, 0f);

    public Vector2 posHorizontalValida, posVerticalValida;

    public void VerificarMovimentacao()
    {
        /*
        if (movimentarX)
        {
            Debug.Log("ORIGEM_Y = " + origem.y);
            Debug.Log("LOCAL_Y = " + pecaRef.localPosition.y);
            Debug.Log("RECT_Y = " + pecaRef.GetComponent<RectTransform>().rect.y);

            if ((pecaRef.localPosition.y <= (origem.y + Mathf.Abs(pecaRef.GetComponent<RectTransform>().rect.y) / 2))
                && (pecaRef.localPosition.y >= (origem.y - Mathf.Abs(pecaRef.GetComponent<RectTransform>().rect.y) / 2)))
            {
                pecaRef.localPosition = new Vector3(pecaRef.localPosition.x + movimentacao.x, pecaRef.localPosition.y, pecaRef.localPosition.z);
            }
        }
        */
            if ((pecaRef.localPosition.y <= posVerticalValida.y) && (pecaRef.localPosition.y >= posVerticalValida.x))
                if ((pecaRef.localPosition.x <= posHorizontalValida.y) && (pecaRef.localPosition.x >= posHorizontalValida.x))
                        pecaRef.localPosition = new Vector3(pecaRef.localPosition.x + movimentacao.x, pecaRef.localPosition.y + movimentacao.y, pecaRef.localPosition.z);
       
    }

        /*
        if (movimentarY)
        {
            if ((pecaRef.localPosition.x <= (origem.x + Mathf.Abs(pecaRef.GetComponent<RectTransform>().rect.x) / 2))
                && (pecaRef.localPosition.x >= (origem.x - Mathf.Abs(pecaRef.GetComponent<RectTransform>().rect.x) / 2)))
            {
                pecaRef.localPosition = new Vector3(pecaRef.localPosition.x, pecaRef.localPosition.y + movimentacao.y, pecaRef.localPosition.z);
            }
        }
        */
}
