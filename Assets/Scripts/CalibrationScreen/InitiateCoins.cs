using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiateCoins : MonoBehaviour
{
    [SerializeField] private GameObject _coin;
    [SerializeField] private float height, width;

    // Start is called before the first frame update
    void Start()
    {
        width = Screen.width/100;
        height = Screen.height/100+2;
        Vector3 adjustment = new Vector3(0,1.2f,0);

        for (float y=-height; y<=height; y+=1.5f)
        {
            for (float x=-width; x<=width; x+=1.5f)
            {
                var newCoin = Instantiate(_coin, new Vector3(x,y,0)+adjustment, Quaternion.identity);
                newCoin.transform.parent = GameObject.Find("CalibrationScreen").transform;
            }
        }
    }
}
