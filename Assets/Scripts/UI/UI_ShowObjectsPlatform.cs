using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ShowObjectsPlatform : MonoBehaviour
{
    public RuntimePlatform[] platformName = new RuntimePlatform[1];
    public GameObject[] GameObjectToBeShow = new GameObject[1];
    public bool platformDefault;
    public GameObject DefaultObjectToBeShow;

    // Start is called before the first frame update
    void Start()
    {
        bool platformFound = false;

        for (int i = 0; i < platformName.Length; i++)
        {
            if (platformName[i] == Application.platform)
            {
                platformFound = true;
                if (GameObjectToBeShow[i]) ;
                GameObjectToBeShow[i].SetActive(true);
            }
        }

        if (!platformFound && platformDefault)
            if (DefaultObjectToBeShow)
                DefaultObjectToBeShow.SetActive(true);

    }

    private void OnEnable()
    {
        Start();
    }
}
