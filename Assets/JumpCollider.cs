using UnityEngine;
using UnityEngine.EventSystems;

public class JumpCollider : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public bool isArea;


    private void Start()
    {
        isArea = false;
    }




    public void OnPointerDown(PointerEventData eventData)
    {
        isArea = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isArea = false;
    }
}
