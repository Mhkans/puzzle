using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideFirst : MonoBehaviour
{
    private bool state;

    public GameObject Guide2;

    // Start is called before the first frame update
    void Start()
    {
        state = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Guide2.SetActive(true);
            state = true;
        }
    }
}
