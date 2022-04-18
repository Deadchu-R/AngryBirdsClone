using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private float dragDistance; // updates every frame
    private Vector3 dragStartPos;
    [SerializeField]
    private float maxDragDistance = 2;
    private Vector3 dragEndPos;
    private Rigidbody2D rigid;
    [SerializeField]
    private float forceFactorX = 1000;
    [SerializeField]
    private float forceFactorY = 500;

    // Start is called before the first frame update
    void Start()
    {
        rigid = GetComponent<Rigidbody2D>();
    }

    private void OnMouseDown()
    {
        offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint(new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z));
        dragStartPos = gameObject.transform.position;
    }

    private void OnMouseDrag()
    {
        Vector3 curScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, screenPoint.z); // cur stands for current
        Vector3 curPosition = Camera.main.ScreenToWorldPoint(curScreenPoint) + offset; // cur stands for current

        dragEndPos = curPosition;
        dragDistance = (curPosition - dragStartPos).magnitude; // magnitude is the size of vector
        //if (dragDistance < maxDragDistance)
        //{
       
        //    dragEndPos = curPosition;
        //}
        if (dragDistance > maxDragDistance)
        {
            Vector3 dragVector = curPosition - dragStartPos;
            curPosition = dragStartPos + (dragVector.normalized * maxDragDistance);
        }
        transform.position = curPosition;
    }

    private void OnMouseUp()
    {
        Vector3 dragVector = dragEndPos - dragStartPos;
        rigid.AddForce(new Vector2(-dragVector.x * forceFactorX, -dragVector.y * forceFactorY));
        rigid.gravityScale = 1;
    }
}
