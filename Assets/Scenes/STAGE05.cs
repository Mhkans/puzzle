using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class STAGE05 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnbuttonClick5()
    {
        SceneManager.LoadScene("STAGE01");
        EnemySpawner.stagecode = 5;
    }
}