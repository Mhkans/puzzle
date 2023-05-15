using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextHPbar : MonoBehaviour
{
    public Text healthText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        healthText.fontStyle = FontStyle.Bold;

        healthText.text = ((int)Player.currentHp).ToString() + "/" + ((int)Player.MaxHp).ToString();    }
}
