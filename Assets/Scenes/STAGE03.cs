using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class STAGE03 : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnbuttonClick3()
    {
        SceneManager.LoadScene("STAGE01");
        EnemySpawner.stagecode = 3;
    }
}