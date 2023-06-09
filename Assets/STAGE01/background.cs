using UnityEngine;

public class background : MonoBehaviour
{
    public Sprite stage0102; // 배경 스프라이트
    public Sprite stage03; // 배경 스프라이트
    public Sprite stage0405; // 배경 스프라이트
    public Sprite stage06; // 배경 스프라이트

    private void Start()
    {
        CreateBackground();
    }

    private void CreateBackground()
    {
        // 배경 오브젝트 생성
        GameObject backgroundObject = new GameObject("Background");
        backgroundObject.transform.position = Vector3.zero;

        // 스프라이트 렌더러 컴포넌트 추가
        SpriteRenderer spriteRenderer = backgroundObject.AddComponent<SpriteRenderer>();
        switch (EnemySpawner.stagecode)
        {
            case 1:
            case 2:
                spriteRenderer.sprite = stage0102;
                break;
            case 3:
                spriteRenderer.sprite = stage03;
                break;
            case 4:
            case 5:
                spriteRenderer.sprite = stage0405;
                break;
            case 6:
                spriteRenderer.sprite = stage06;
                break;
        }

        // 스프라이트를 캔버스의 0번째 위치에 배치
        Camera mainCamera = Camera.main;
        float cameraHeight = mainCamera.orthographicSize * 2f;
        float cameraWidth = cameraHeight * mainCamera.aspect;
        float spriteHeight = spriteRenderer.sprite.bounds.size.y;
        float spriteWidth = spriteRenderer.sprite.bounds.size.x;

        backgroundObject.transform.localScale = new Vector3(
            cameraWidth / spriteWidth,
            cameraHeight / spriteHeight,
            1f
        );

        // 배경 오브젝트를 캔버스의 0번째 위치로 이동
        Vector3 canvasPosition = mainCamera.transform.position;
        canvasPosition.z = 0f;
        backgroundObject.transform.position = canvasPosition;
    }
}