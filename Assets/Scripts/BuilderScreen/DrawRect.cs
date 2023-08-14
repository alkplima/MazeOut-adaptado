using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawRect : MonoBehaviour
{
    [SerializeField] private LineRenderer lineRend;
    [SerializeField] private float xDir, xEsq, yTop, yBott;
    private Vector2 initialMousePosition, currentMousePosition;

    private float area;

    // Start is called before the first frame update
    void Start()
    {
        lineRend = GetComponent<LineRenderer>();
        lineRend.positionCount = 4;

        // area alcançada (hardcoded)
        xDir = 8.4f-1f;
        xEsq = -7.7f+1f;
        yTop = 3.9f-1f;
        yBott = -2.3f+1f;

        // Desenhando as linhas
        lineRend.SetPosition(0, new Vector2(xEsq, yTop));
        lineRend.SetPosition(1, new Vector2(xEsq, yBott));
        lineRend.SetPosition(2, new Vector2(xDir, yBott));
        lineRend.SetPosition(3, new Vector2(xDir, yTop));
    }
}
