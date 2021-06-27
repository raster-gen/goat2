using UnityEngine;

public class TouchController : MonoBehaviour
{

    

    public float power = 10f;
    public float maxDrag = 5f;
    public Rigidbody2D rb;
    public LineRenderer lr;

    public GameObject gObject;
    private JumpCollider jumpCollider;
    

    private Vector3 dragStartPos;

    private Touch touch;
    private bool isTouchPhaseMoved;

    private void Start()
    {
        jumpCollider = gObject.GetComponent<JumpCollider>();
        isTouchPhaseMoved = false;
    }


    private void Update()
    {
        bool area = jumpCollider.isArea;
        if (Input.touchCount > 0 && area)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                Debug.Log("DragStart");
                DragStart();
            }

            if (touch.phase == TouchPhase.Moved)
            {
                Debug.Log("Draging");
                Dragging();
                isTouchPhaseMoved = true;
            }

            
        }
        if (isTouchPhaseMoved && !area)
        {
            Debug.Log("DragRelease");
            DragRelease();
        }
    }

    void DragStart()
    {
       
        dragStartPos = Camera.main.ScreenToWorldPoint(touch.position);
        dragStartPos.z = 0f;
        lr.positionCount = 1;
        lr.SetPosition(0, dragStartPos);

    }

    void Dragging()
    {
        
        Vector3 draggingPos = Camera.main.ScreenToWorldPoint(touch.position);
        draggingPos.z = 0f;
        lr.positionCount = 2;
        lr.SetPosition(1, draggingPos);
    }

    void DragRelease()
    {
        
        lr.positionCount = 0;
        Vector3 dragReleasePos = Camera.main.ScreenToWorldPoint(touch.position);
        dragReleasePos.z = 0;
        Vector3 force = dragStartPos - dragReleasePos;
        Vector3 clampedForce = Vector3.ClampMagnitude(force, maxDrag) * power;
        isTouchPhaseMoved = false;
        rb.AddForce(clampedForce, ForceMode2D.Impulse);
        
    }
}
