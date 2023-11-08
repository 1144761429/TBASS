using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(LineRenderer))]
public class CircleRenderer2D : MonoBehaviour
{
    [field:SerializeField] public Transform Center { get; set; }
    [field:SerializeField] public float Radius { get; set; }
    
    [field:Range(3,32)]
    [field:SerializeField] 
    public int Division { get; set; }

    [field:Range(0,360)]
    [field:SerializeField] 
    public float RotationOffset { get; set; }

    [field:SerializeField] 
    public float Thickness { get; set; }
    
    [field:SerializeField] 
    public Material Material { get; set; }
    
    private LineRenderer _lineRenderer;
    
    private void Awake()
    {
        _lineRenderer = GetComponent<LineRenderer>();
        _lineRenderer.loop = true;
    }

    private void Update()
    {
        _lineRenderer.material = Material;
        _lineRenderer.widthMultiplier = 0.1f * Thickness;
        
        Vector3 center = Center.position;
        float angle = 360.0f / Division;

        _lineRenderer.positionCount = Division;
        
        for (int i = 0; i < _lineRenderer.positionCount; i++)
        {
            float angleInRad = (angle * i + RotationOffset)* Mathf.Deg2Rad;
            float xPos = Radius * MathF.Cos(angleInRad);
            float yPos = Radius * Mathf.Sin(angleInRad);

            _lineRenderer.SetPosition(i,center + new Vector3(xPos,yPos));
        }
    }
}
