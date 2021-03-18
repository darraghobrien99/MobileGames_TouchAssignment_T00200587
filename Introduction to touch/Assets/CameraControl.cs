using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraControl : MonoBehaviour, Icontrollable
{

    float xAngle;
    float yAngle;
    public int speed = 4;
    private float touchDelta = 0.0F;
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    private Vector3 drag_position;
    public bool isEnabled = false;

 

    // Start is called before the first frame update
    void Start()
    {
        xAngle = 0;
        yAngle = 0;
        this.transform.rotation = Quaternion.Euler(yAngle, xAngle, 0);
        initialPosition = transform.position;
        initialRotation = transform.rotation;

    }

    // Update is called once per frame
    void Update()
    {
        Input.gyro.enabled = isEnabled;

        Debug.Log(Input.gyro.enabled);

        if (Input.gyro.enabled)
        {
            transform.Rotate(new Vector3(-Input.gyro.rotationRateUnbiased.x, -Input.gyro.rotationRateUnbiased.y, -Input.gyro.rotationRateUnbiased.z), Space.Self);
        }
        
    }

    public void changeEnabled()
    {
        if (isEnabled == true)
            isEnabled = false;
        else
            isEnabled = true;
    }


    public void rotateTo(float angle, Quaternion initialRotation)
    {
        Quaternion rotation = Quaternion.AngleAxis(angle, Camera.main.transform.forward);
        transform.rotation = rotation * initialRotation;
    }

  
    public void youve_been_touched()
    {
        throw new System.NotImplementedException();
    }

    public void MoveTo(Vector3 destination)
    {
        drag_position = destination;
    }


    public void deselect_object()
    {
        throw new System.NotImplementedException();
    }

    public void scaleObject(Vector3 initialScale, float scaleSize)
    {
        transform.localScale = initialScale * scaleSize;
    }

    public void rotateObject(float rotateAngle, Quaternion startRotation)
    {
        Quaternion rotation = Quaternion.AngleAxis(rotateAngle, Camera.main.transform.forward);
        transform.rotation = rotation * transform.rotation;
    }
}
