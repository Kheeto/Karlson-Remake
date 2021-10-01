using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Draggable : MonoBehaviour
{
    private LineRenderer lr;
    private Rigidbody rb;

    private Vector3 startPos;
    private Vector3 mOffset;
    private float mZCoord;

    private void Start()
    {
        lr = GetComponent<LineRenderer>();
        lr.positionCount = 0;

        rb = GetComponent<Rigidbody>();
    }

    void OnMouseDown()
    {
        startPos = transform.position;
        
        rb.useGravity = false;
        lr.positionCount = 2;

        lr.SetPosition(0, startPos);
        lr.SetPosition(1, startPos);

        mZCoord = Camera.main.WorldToScreenPoint(

        gameObject.transform.position).z;

        // Store offset = gameobject world pos - mouse world pos
        mOffset = gameObject.transform.position - GetMouseAsWorldPoint();
    }

    private Vector3 GetMouseAsWorldPoint()
    {
        // Pixel coordinates of mouse (x,y)
        Vector3 mousePoint = Input.mousePosition;

        // z coordinate of game object on screen
        mousePoint.z = mZCoord;

        // Convert it to world points
        return Camera.main.ScreenToWorldPoint(mousePoint);

    }

    void OnMouseDrag()
    {
        // move the object
        rb.MovePosition(GetMouseAsWorldPoint() + mOffset);
        rb.AddTorque(Vector3.up * 1);
        
        // draws the line correctly showing how you drag the object
        lr.SetPosition(1, GetMouseAsWorldPoint());
    }

    void OnMouseUp()
    {

        rb.useGravity = true;
        lr.positionCount = 0;
    }
}
