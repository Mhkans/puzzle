using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGuide1 : MonoBehaviour
{
    private bool state;

    public GameObject Guide4;

    public void LoadUI()
    {
        Guide4.SetActive(true);
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
