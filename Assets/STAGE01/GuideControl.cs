using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GuideControl : MonoBehaviour
{
    private bool isPaused = false;
    
    void Start()
    {
        
    }

    // Update is called once per frame
    
    public void click1()
    {
        isPaused = true;
        Time.timeScale = 0f; // 시간을 멈춤

    }

    public void click2()
    {
        isPaused = false;
        Time.timeScale = 1f; // 시간을 정상적으로 흐르게 함

    }
}
