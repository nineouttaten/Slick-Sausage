using System;
using System.Runtime.CompilerServices;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Sausage : MonoBehaviour
{
    private Vector3 mousePressDownPos;
    private Vector3 mouseReleasePos;
    //private Vector3 mouseSub;
    [SerializeField] public Rigidbody rb;
    [SerializeField] public GameObject forceStartingPoint;
    [SerializeField] public Collider sausageCollider;
    private bool isShoot;
    
    void Start()
    {
        //rb = GetComponent<Rigidbody>();
    }

    private void OnMouseDown()
    {
        if (!IsGrounded()) return;
        mousePressDownPos = Input.mousePosition;
    }

    private void OnMouseDrag()
    {
        if (!IsGrounded())
        {
            DrawTrajectory.Instance.HideLine();
            return;
        }

        Vector3 forceInit = (Input.mousePosition - mousePressDownPos);
        if(forceInit.y > 0) return;
        //print(forceInit);
        Vector3 forceV = (new Vector3(forceInit.x, forceInit.y, /*forceInit.y*/0)) * forceMultiplier;
        
        //if(!isShoot) 
            DrawTrajectory.Instance.UpdateTrajectory(forceV, rb, forceStartingPoint.transform.position);
    }

    private void OnMouseUp()
    {
        if (!IsGrounded()) return;
        DrawTrajectory.Instance.HideLine();
        mouseReleasePos = Input.mousePosition;
        var mouseSub = mousePressDownPos - mouseReleasePos;
        if(mouseSub.y < 0) return;
        Shoot(mouseSub);
        //Shoot(mouseReleasePos - mousePressDownPos);
    }

    private float forceMultiplier = 10f;

    private bool IsGrounded()
    {
        float extraHeightText = .05f;
        int layerMask = (1 << LayerMask.NameToLayer("Default"));
        bool raycastHit = Physics.Raycast(sausageCollider.bounds.center, Vector3.down, sausageCollider.bounds.extents.y + extraHeightText, layerMask);
        //Debug.DrawRay(sausageCollider.bounds.center, Vector3.down * (sausageCollider.bounds.extents.y + extraHeightText));
        print(raycastHit);
        return raycastHit;
    }
    private void Shoot(Vector3 force)
    {
        //if(isShoot)    
        //    return;
        forceStartingPoint.transform.Rotate(0, 0, -90, Space.World);
        rb.AddForce(new Vector3(force.x,force.y,/*Force.y*/0) * forceMultiplier);
        //isShoot = true;
        //Spawner.Instance.NewSpawnRequest();
    }
    
}