using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text tooltipText; // 툴팁에 표시될 텍스트 컴포넌트

    // 마우스 커서가 UI 요소 위에 들어갔을 때 호출될 메서드
    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltipText.gameObject.SetActive(true); // 텍스트를 활성화하여 보이게 함
    }

    // 마우스 커서가 UI 요소에서 나왔을 때 호출될 메서드
    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipText.gameObject.SetActive(false); // 텍스트를 비활성화하여 숨김
    }
}