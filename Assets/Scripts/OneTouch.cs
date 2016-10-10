using UnityEngine;
using System.Collections;

public class OneTouch
{

    private RaycastHit2D hit;
    private GameObject touchPoint;
    private Transform touchPointTransform;
    private GameObject pickedObject;
    private HingeJoint2D hj;
    private SpringJoint2D sj;
    private Rigidbody2D rb;
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
                rb.angularDrag = 5;
                rb.drag = 5;
                sj.enabled = true;
                sj.distance = 0.1f;
                touchPointTransform.position = hit.transform.position;
                sj.connectedBody = touchPoint.GetComponent<Rigidbody2D>();
            }
        }
        else if (Input.GetMouseButton(0))
        {
            if (!pickedObject)
                return;
            
            touchPointTransform.position = hit.point;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            if (pickedObject)
            {
                rb.drag = 0;
                rb.angularDrag = 0;
                pickedObject = null;
                sj.enabled = false;
            }
        }
    }
 }