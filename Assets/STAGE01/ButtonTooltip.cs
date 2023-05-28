using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonTooltip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Text tooltipText; // ������ ǥ�õ� �ؽ�Ʈ ������Ʈ

    // ���콺 Ŀ���� UI ��� ���� ���� �� ȣ��� �޼���
    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltipText.gameObject.SetActive(true); // �ؽ�Ʈ�� Ȱ��ȭ�Ͽ� ���̰� ��
    }

    // ���콺 Ŀ���� UI ��ҿ��� ������ �� ȣ��� �޼���
    public void OnPointerExit(PointerEventData eventData)
    {
        tooltipText.gameObject.SetActive(false); // �ؽ�Ʈ�� ��Ȱ��ȭ�Ͽ� ����
    }
}