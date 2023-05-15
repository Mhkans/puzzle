using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guide : MonoBehaviour
{
    private bool state;

    public GameObject Guide2;

    public void Guiding()
    {
        Guide2.SetActive(false);
    }
    
    public void Guiding2()
    {
        Guide2.SetActive(true);
    }
    // Start is called before the first frame update
    void Start()
    {
        state = true;
    }

    // Update is called once per frame
    void Update()
    {

    }


}
