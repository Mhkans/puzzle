using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGuide1 : MonoBehaviour
{
    private bool state;

    public GameObject Guide4;
    public GameObject Guide5;
    public GameObject Guide6;
    public GameObject Guide7;
    public GameObject Guide8;
    public GameObject Guide9;

    public void LoadUI()
    {
        Guide4.SetActive(true);
    }
    public void LoadUI2()
    {
        Guide5.SetActive(true);
    }

    public void LoadUI3()
    {
        Guide6.SetActive(true);
    }

    public void LoadUI6()
    {
        Guide9.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        state = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
