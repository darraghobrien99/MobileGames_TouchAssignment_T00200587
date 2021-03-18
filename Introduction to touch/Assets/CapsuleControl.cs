using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;

public class CapsuleControl : MonoBehaviour, Icontrollable
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
        original = rend.material.GetColor("_Color");
        rend.material.color = Color.red;
    }

    void Update()
    {
        transform.position = Vector3.Lerp(transform.position, drag_position, 0.05f);
    }

    public void rotateObject(float rotateAngle, Quaternion startRotation)
    {
        Quaternion rotation = Quaternion.AngleAxis(rotateAngle, Camera.main.transform.forward);
        transform.rotation = rotation * transform.rotation;
    }

    public Quaternion getRotation()
    {
        return transform.rotation;
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
        destination = new Vector3(destination.x, destination.y, 0);
        drag_position = destination;

    }

    public void deselect_object()
    {
        rend.material.color = Color.red;
    }

    public void rotateTo(float angle, Quaternion initialRotation)
    {
        Quaternion rotation = Quaternion.AngleAxis(angle, Camera.main.transform.forward);
        transform.rotation = rotation * initialRotation;
    }


}
