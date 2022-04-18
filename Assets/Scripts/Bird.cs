using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bird : MonoBehaviour
{
    private Vector3 screenPoint;
    private Vector3 offset;
    private Vector3 dragStartPos;
    public Vector3 dragEndPos;
    private float dragDistance; // updates every frame
    [SerializeField]
    private float maxDragDistance = 2;
    private Rigidbody2D rigid;
    private CircleCollider2D col;
    [SerializeField]
    private float forceFactorX = 1000;
    [SerializeField]
    private float forceFactorY = 500;

    public float time = 3;
    public GameObject gameObject;
    public Animator birdAnimator;
   

    [Header("Line renderer veriables")]
    public LineRenderer line;
    [Range(2, 30)]
    public int resolution;

    [Header("Formula variables")]
    public Vector2 velocity;
    public float yLimit;
    private float g;

    [Header("Linecast variables")]
    [Range(2, 30)]
    public int linecastResolution;
    public LayerMask canHit;

    // Bird > bird fly > bird speed  >
    //  CurPos 
    // if -2 > CurTransform.pos.x > 2 || -2 > CurTransform.pos.y > 2 cant move bird
    // Start is called before the first frame update
    void Start()
    {
       // gameObject
        rigid = GetComponent<Rigidbody2D>();
        g = Mathf.Abs(Physics2D.gravity.y);

    }
    private void Update()
    {

        velocity.x = -dragEndPos.x * dragDistance  ; velocity.y  = -dragEndPos.y * dragDistance ;
        RenderArc();
    }

    private void RenderArc()
    {
        line.positionCount = resolution + 1;
        line.SetPositions(CalculateLineArray());
    }

    private Vector3[] CalculateLineArray()
    {
        Vector3[] lineArray = new Vector3[resolution + 1];

        var lowestTimeValue = MaxTimeX() / resolution;

        for (int i = 0; i < lineArray.Length; i++)
        {
            var t = lowestTimeValue * i;
            lineArray[i] = CalculateLinePoint(t);
        }

        return lineArray;
    }

    private Vector2 HitPosition()
    {
        var lowestTimeValue = MaxTimeY() / linecastResolution;

        for (int i = 0; i < linecastResolution + 1; i++)
        {
            var t = lowestTimeValue * i;
            var tt = lowestTimeValue * (i + 1);

            var hit = Physics2D.Linecast(CalculateLinePoint(t), CalculateLinePoint(tt), canHit);

            if (hit)
                return hit.point;
        }

        return CalculateLinePoint(MaxTimeY());
    }

    private Vector3 CalculateLinePoint(float t)
    {
        float x = velocity.x * t ;
        float y = (velocity.y * t ) - (g * Mathf.Pow(t, 2) / 2) ;
        return new Vector3(x + transform.position.x, y + transform.position.y);
    }

    private float MaxTimeY()
    {
        var v = velocity.y;
        var vv = v * v;

        var t = (v + Mathf.Sqrt(vv + 2 * g * (transform.position.y - yLimit))) / g;
        return t;
    }

    private float MaxTimeX()
    {
        var x = velocity.x;
        if (x == 0)
        {
            velocity.x = 000.1f;
            x = velocity.x;
        }

        var t = (HitPosition().x - transform.position.x) / x;
        return t;
    }

    private void OnMouseDown()
    {
        birdAnimator.gameObject.GetComponent<Animator>().enabled = false;
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
        //birdAnimator.gameObject.GetComponent<Animator>().enabled = true;
        Vector3 dragVector = dragEndPos - dragStartPos;
        rigid.AddForce(new Vector2(-dragVector.x * forceFactorX, -dragVector.y * forceFactorY));
        rigid.gravityScale = 1;
       
     
            Destroy(gameObject, time);
 
        
    }

}
