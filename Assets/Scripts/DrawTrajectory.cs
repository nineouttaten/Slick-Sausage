using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawTrajectory : MonoBehaviour
{
    [SerializeField] 
    private LineRenderer _lineRenderer;

    [SerializeField] [Range(3, 30)] private int _lineSegmentCount = 20;

    [SerializeField] [Range(10, 100)] private int _showPercentage = 50;

    [SerializeField] private int _linePointCount;
    
    private List<Vector3> _linePoints = new List<Vector3>();

    #region Singleton

    public static DrawTrajectory Instance;

    private void Awake()
    {
        Instance = this;
    }

    #endregion

    private void Start()
    {
        _linePointCount = (int) (_lineSegmentCount * (_showPercentage / 100f));
    }

    public void UpdateTrajectory(Vector3 forceVector, Rigidbody rigidBody, Vector3 startingPoint)
    {
        Vector3 velocity = (forceVector / rigidBody.mass) * Time.fixedDeltaTime;

        float FlightDuration = (2 * velocity.y) / Physics.gravity.y;

        float stepTime = FlightDuration / _lineSegmentCount;
        
        _linePoints.Clear();
        _linePoints.Add(startingPoint);

        for (int i = 1; i < _linePointCount; i++)
        //for (int i = 1; i < _lineSegmentCount; i++)
        {
            float stepTimePassed = stepTime * i * 1.5f;

            Vector3 MovementVector = new Vector3(
                velocity.x * stepTimePassed,
                velocity.y * stepTimePassed - 0.5f * Physics.gravity.y * stepTimePassed * stepTimePassed,
                velocity.z * stepTimePassed
            );
            
            Vector3 newPointOnLine = -MovementVector + startingPoint;
            int layerMask = (1 << LayerMask.NameToLayer("Default"));
            RaycastHit hit;
            if (Physics.Raycast(_linePoints[i - 1], newPointOnLine - _linePoints[i - 1], out hit, (newPointOnLine - _linePoints[i - 1]).magnitude, layerMask))
            {
                _linePoints.Add(hit.point);
                break;
            }
            
            Debug.DrawLine(_linePoints[i - 1], newPointOnLine, Color.magenta, 0.0f, true);
            
            _linePoints.Add(newPointOnLine);
        }

        _lineRenderer.positionCount = _linePoints.Count;
        _lineRenderer.SetPositions(_linePoints.ToArray());
    }

    public void HideLine()
    {
        _lineRenderer.positionCount = 0;
    }
}
