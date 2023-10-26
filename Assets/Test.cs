using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using WeaponSystem;


public class Test : MonoBehaviour
{
    public GameObject t;
    
    private void Update()
    {
        if (MouseUtil.GetVector2ToMouse(transform.position).x > 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        else
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }

        
        float actualAngle = Vector2.SignedAngle(Vector2.right, MouseUtil.GetVector2ToMouse(transform.position));
        if (MouseUtil.GetVector2ToMouse(transform.position).x <= 0)
        {
            actualAngle -= 180;
        }
        
        
        transform.localRotation = Quaternion.Euler(0, 0, actualAngle);
        
        t.transform.localRotation = Quaternion.Euler(transform.localRotation.x,transform.localRotation.y,transform.localRotation.z - 30);

        //transform.localRotation = Quaternion.Euler(0, 0, Vector2.SignedAngle(Vector2.right, MouseUtil.GetVector2ToMouse(transform.position)));
    }
}