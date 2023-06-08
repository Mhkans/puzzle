using UnityEngine;
using UnityEngine.UI;

public class EnemyText : MonoBehaviour
{
    private Text enemyText;
    

    // Start is called before the first frame update
    void Start()
    {
        enemyText = GetComponent<Text>();
        enemyText.fontSize = 20;
        enemyText.color = Color.white;
        enemyText.text = "적의 정보ddd";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
