using System;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

[RequireComponent(typeof(Collider))]
public class Sausage : MonoBehaviour{


    private Vector3 mousePressDownPos;
    private Vector3 mouseReleasePos;
    public static float timer;
    public static bool reversed;
    [SerializeField] public Rigidbody rb;
    [SerializeField] public GameObject sausage;
    [SerializeField] public GameObject forceStartingPoint;
    [SerializeField] public Collider sausageCollider;
    public TextMeshProUGUI TimerText; 
    private bool playing = false;
    private bool isShoot;
    private Vector3 forceInit;
    private Vector3 mouseSub;
    private void OnMouseDown()
    {
        if (!IsGrounded()) return;
        mousePressDownPos = Input.mousePosition;
    }


    private void Update()
    {
        if(playing == true){
  
            timer += Time.deltaTime;
            int minutes = Mathf.FloorToInt(timer / 60F);
            int seconds = Mathf.FloorToInt(timer % 60F);
            int milliseconds = Mathf.FloorToInt((timer * 100F) % 100F);
            TimerText.text = minutes.ToString ("00") + ":" + seconds.ToString ("00") + ":" + milliseconds.ToString("00");
        }
        
        if (Input.GetKeyDown(KeyCode.R))
        {
            reversed = false;
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex) ;
            timer = 0;
        }
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            reversed = false;
            SceneManager.LoadScene(0) ;
            timer = 0;
        }
    }

    private void OnMouseDrag()
    {
        if (!IsGrounded())
        {
            DrawTrajectory.Instance.HideLine();
            return;
        }
        if (reversed)
        {
            forceInit = (mousePressDownPos - Input.mousePosition);
        }
        else
        {
            forceInit = (Input.mousePosition - mousePressDownPos);
        }
        if(forceInit.y > 0) return;
        Vector3 forceV = (new Vector3(forceInit.x, forceInit.y, 0)) * forceMultiplier;
        
        DrawTrajectory.Instance.UpdateTrajectory(forceV, rb, forceStartingPoint.transform.position);
    }

    private void OnMouseUp()
    {
        playing = true;
        if (!IsGrounded()) return;
        DrawTrajectory.Instance.HideLine();
        mouseReleasePos = Input.mousePosition;
        if (reversed)
        {
            mouseSub = mouseReleasePos - mousePressDownPos;
        }
        else
        {
            mouseSub = mousePressDownPos - mouseReleasePos;
        }
        if(mouseSub.y < 0) return;
        Shoot(mouseSub);
    }

    private float forceMultiplier = 10f;

    private bool IsGrounded()
    {
        float extraHeightText = .05f;
        int layerMask = (1 << LayerMask.NameToLayer("Default"));
        bool raycastHit = Physics.Raycast(sausageCollider.bounds.center, Vector3.down, sausageCollider.bounds.extents.y + extraHeightText, layerMask);
        print(raycastHit);
        return raycastHit;
    }
    private void Shoot(Vector3 force)
    {
        forceStartingPoint.transform.Rotate(0, 0, -90, Space.World);
        rb.AddForce(new Vector3(force.x,force.y,0) * forceMultiplier);
    }
    
}