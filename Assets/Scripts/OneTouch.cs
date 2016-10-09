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
                sj.enabled = true;
                touchPointTransform.position = hit.transform.position;
                sj.connectedBody = touchPoint.GetComponent<Rigidbody2D>();
            }
        }
        else if (Input.GetMouseButton(0))
        {
            Debug.Log(hit.point);
            if (!pickedObject)
                return;
            
            touchPointTransform.position = hit.point;
        }
        else if (Input.GetMouseButtonUp(0))
        {
            pickedObject = null;
            sj.enabled = false;
        }
    }
 }