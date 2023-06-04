using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossGuide : MonoBehaviour
{
    private bool state;

    public GameObject boss;
    // Start is called before the first frame update
    void Start()
    {
        state = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            boss.SetActive(true);
            state = true;
        }
    }
}
