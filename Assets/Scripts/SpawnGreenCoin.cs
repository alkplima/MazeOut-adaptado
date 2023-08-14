using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnGreenCoin : MonoBehaviour
{
    [SerializeField] private GameObject greenCoin;
    [SerializeField] private float xDir, xEsq, yTop, yBott;
    private float x1, x2, x3, x4, x5, x6, x7, x8, x9;
    private float y1, y2, y3, y4, y5, y6, y7, y8, y9, xNew, yNew, z = 0, raio, yAdjust;
    Vector3 handPos;

    // Start is called before the first frame update
    void Start()
    {
        // area alcançada (hardcoded)
        // xDir = 10.4f-1f;
        // xEsq = -9.7f+1f;
        // yTop = 5.9f-1f;
        // yBott = -4.3f+1f;


        // definindo os limites pela primeira vez
        // if (!GameObject.Find("handCircle")) { return; }
        if (GameObject.Find("handCircle") != null) 
        {
            handPos = GameObject.Find("handCircle").gameObject.transform.position;
        }
        else 
        {
            handPos = new Vector3(0,0,0);
        }
        // xDir = handPos.transform.position.x/10-1f;
        // xEsq = handPos.transform.position.x/10+1f;
        // yTop = handPos.transform.position.y/10-1f;
        // yBott = handPos.transform.position.y/10+1f;
        xDir = 0f-1f;
        xEsq = 0f+1f;
        yTop = 0f-1f;
        yBott = 0f+1f;
    }

    // Update is called once per frame
    void Update()
    {
        // definindo os limites pela primeira vez
        // if (!GameObject.Find("handCircle")) { return; }
        if (GameObject.Find("handCircle") != null) 
        {
            handPos = GameObject.Find("handCircle").gameObject.transform.position;
        }
        else 
        {
            handPos = new Vector3(0,0,0);
        }
        // handPos = GameObject.Find("handCircle").gameObject.transform.position;
        xNew = handPos.x;
        yNew = handPos.y;
        if (xDir < xNew) { xDir = xNew; }
        if (xEsq > xNew) { xEsq = xNew; }
        if (yTop < yNew) { yTop = yNew; }
        if (yBott > yNew) { yBott = yNew; }
    }

    public void GenerateGreenCoins()
    {
        xDir -= 1f;
        xEsq += 1f;
        yTop -= 1f;
        yBott += 1f;

        yAdjust = (yBott - yTop)/2 + 1f;


        // raio = (yTop-yBott)<(xDir-xEsq) ? (yTop-yBott) : (xDir-xEsq);
        raio = (yTop-yBott);

        // posições das 9 moedas verdes
        x1 = raio;
        y1 = 0+yAdjust;

        x2 = raio*Mathf.Cos(Mathf.PI/8);
        y2 = raio*Mathf.Sin(Mathf.PI/8)+yAdjust;

        x3 = raio*Mathf.Cos(Mathf.PI/4);
        y3 = raio*Mathf.Sin(Mathf.PI/4)+yAdjust;

        x4 = raio*Mathf.Cos(3*Mathf.PI/8);
        y4 = raio*Mathf.Sin(3*Mathf.PI/8)+yAdjust;

        x5 = raio*Mathf.Cos(Mathf.PI/2);
        y5 = raio+yAdjust;

        x6 = raio*Mathf.Cos(5*Mathf.PI/8);
        y6 = raio*Mathf.Sin(5*Mathf.PI/8)+yAdjust;

        x7 = raio*Mathf.Cos(3*Mathf.PI/4);
        y7 = raio*Mathf.Sin(3*Mathf.PI/4)+yAdjust;

        x8 = raio*Mathf.Cos(7*Mathf.PI/8);
        y8 = raio*Mathf.Sin(7*Mathf.PI/8)+yAdjust;

        x9 = raio*Mathf.Cos(Mathf.PI);
        y9 = raio*Mathf.Sin(Mathf.PI)+yAdjust;

        Vector3 position1 = new Vector3(x1, y1, z);
        Vector3 position2 = new Vector3(x2, y2, z);
        Vector3 position3 = new Vector3(x3, y3, z);
        Vector3 position4 = new Vector3(x4, y4, z);
        Vector3 position5 = new Vector3(x5, y5, z);
        Vector3 position6 = new Vector3(x6, y6, z);
        Vector3 position7 = new Vector3(x7, y7, z);
        Vector3 position8 = new Vector3(x8, y8, z);
        Vector3 position9 = new Vector3(x9, y9, z);
        Instantiate(greenCoin, position1, Quaternion.identity);
        Instantiate(greenCoin, position2, Quaternion.identity);
        Instantiate(greenCoin, position3, Quaternion.identity);
        Instantiate(greenCoin, position4, Quaternion.identity);
        Instantiate(greenCoin, position5, Quaternion.identity);
        Instantiate(greenCoin, position6, Quaternion.identity);
        Instantiate(greenCoin, position7, Quaternion.identity);
        Instantiate(greenCoin, position8, Quaternion.identity);
        Instantiate(greenCoin, position9, Quaternion.identity);
    }
}
