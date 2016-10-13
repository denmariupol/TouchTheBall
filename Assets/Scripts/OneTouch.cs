using UnityEngine;
using System.Collections;

public class OneTouch
{

    //private RaycastHit2D hit;
    private GameObject touchPoint;
    private Transform touchPointTransform;
    private GameObject pickedObject;
    private SpringJoint2D sj;
    private Rigidbody2D rb;
    private Vector2 anchor;
    private Vector2 dir;
    private float distance;
    private Vector2 curPos,lastPos;
    public void Init ()
    {
        touchPoint = GameObject.FindGameObjectWithTag("TouchPoint");
        touchPointTransform = touchPoint.transform;
        
    }
	
    public void MouseTouch()
    {

        RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(Input.mousePosition), Vector3.forward, Mathf.Infinity);
        
        if (Input.GetMouseButtonDown(0))
        {
            if (!hit)
                return;

            if (hit.collider.CompareTag("Figure"))
            {
                pickedObject = hit.collider.gameObject;
                sj = hit.collider.gameObject.GetComponent<SpringJoint2D>();
                rb = hit.collider.gameObject.GetComponent<Rigidbody2D>();

                rb.angularDrag = 10;
                rb.drag = 10;
                sj.enabled = true;
                dir = (v2FromV3(hit.collider.gameObject.transform.position) - hit.point);
                distance = (v2FromV3(hit.collider.gameObject.transform.position) - hit.point).magnitude;
                Debug.Log(distance);
                sj.distance = 0.1f;
                sj.frequency = 5;

                touchPointTransform.position = hit.transform.position;
                sj.connectedBody = touchPoint.GetComponent<Rigidbody2D>();
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (!pickedObject)
                return;
            sj.anchor = dir;
            sj.frequency = 5;
            rb.drag = 10;
            rb.angularDrag = 10;
            curPos = touchPointTransform.position;
            if(curPos == lastPos)
            {
                rb.drag = 1;
                rb.angularDrag = 1;
                sj.frequency = 0;
            }
            lastPos = curPos;
            touchPointTransform.position = hit.point;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (pickedObject)
            {
                sj.anchor = Vector2.zero;
                touchPointTransform.position = new Vector2(100, 100);
                rb.drag = 0;
                rb.angularDrag = 0;
                pickedObject = null;
                sj.enabled = false;
            }
        }
    }
    private Vector2 v2FromV3(Vector3 v3)
    {
        return new Vector2(v3.x, v3.y);
    }
}