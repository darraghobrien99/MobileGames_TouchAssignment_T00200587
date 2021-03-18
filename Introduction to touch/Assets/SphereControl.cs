using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEditor;
using UnityEngine;

public class SphereControl : MonoBehaviour, Icontrollable
{
    private Vector3 drag_position;
    public Renderer rend;
    Color original;
   


    // Start is called before the first frame update
    void Start()
    {
        drag_position = transform.position;
        rend = GetComponent<Renderer>();
        rend.enabled = true;
        rend.material.color = Color.red;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, drag_position, 0.05f);
    }

    public void rotateObject(float rotateAngle, Quaternion startRotation)
    {
        Quaternion rotation = Quaternion.AngleAxis(rotateAngle, Camera.main.transform.forward);
        transform.rotation = rotation * transform.rotation;
    }

    public void youve_been_touched()
    {
        rend.material.color = Color.green;

    }

    public void scaleObject(Vector3 initialScale, float scaleSize)
    {
        transform.localScale = initialScale * scaleSize;
    }

    public void MoveTo(Vector3 destination)
    {
        drag_position = destination;

    }

    public void rotateTo(float angle, Quaternion initialRotation)
    {
        Quaternion rotation = Quaternion.AngleAxis(angle, Camera.main.transform.forward);
        transform.rotation = rotation * initialRotation;
    }

    public void deselect_object()
    {
        rend.material.color = Color.red;
    }


    
}
