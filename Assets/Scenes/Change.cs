using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Change : MonoBehaviour
{
    private bool state;

    public GameObject Image1;
    public GameObject Image2;

    public void LoadUI2()
    {
        Image1.SetActive(false);
        Image2.SetActive(true);
    }
    public void LoadUI3()
    {
        Image1.SetActive(true);
        Image2.SetActive(false);
    }


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
