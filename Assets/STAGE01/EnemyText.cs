using UnityEngine;
using UnityEngine.UI;

public class EnemyText : MonoBehaviour
{
    public Text enemyText;
    public Enemy enemy;

     // Start is called before the first frame update
    void Start()
    {
        enemy = FindObjectOfType<Enemy>();
        enemyText = GetComponent<Text>(); // Text 컴포넌트 할당 추가
        enemyText.fontSize = 25;
        enemyText.fontStyle = FontStyle.Bold;
        enemyText.color = Color.white;
    }

    // Update is called once per frame
    
}