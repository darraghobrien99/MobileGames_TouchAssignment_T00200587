using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Runtime.InteropServices;
using System.Threading;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class TouchManagerScript : MonoBehaviour
{
    private float timeOfTap, startTime, tapThreshold = 0.05f, starting_distance_to_selected_object, startDistance, startAngle, initialDistance, newestDistance, scaleSize, differenceInDegrees, newAngle, difference;
    private Quaternion startRotation;
    private Touch touch;
    private Vector3 initialScale, newPos;
    private CameraControl camera;
    private Boolean selected = false;
    [SerializeField]

    Icontrollable selectedObject;
    Icontrollable object_hit;
  
    // Start is called before the first frame update
    void Start()
    {
        GameObject ourCameraPlane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        ourCameraPlane.transform.position = new Vector3(0, Camera.main.transform.position.y, 0);
        ourCameraPlane.transform.up = (Camera.main.transform.position - ourCameraPlane.transform.position).normalized;
        camera = FindObjectOfType<CameraControl>();
        selectedObject = camera;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            RaycastHit info;

            Ray ourRay = Camera.main.ScreenPointToRay(Input.touches[0].position);

            Debug.DrawRay(ourRay.origin, 30 * ourRay.direction);

            if (Physics.Raycast(ourRay, out info))
            {
                if (info.collider != null)
                {
                    object_hit = info.transform.GetComponent<Icontrollable>();
                   // selectedObject = object_hit;

                    if (Input.touchCount == 1)
                    {
                        switch (Input.touches[0].phase)
                        {
                            case TouchPhase.Began:

                                timeOfTap = 0;
                                Debug.Log("Touching at: " + touch.position);
                                break;

                            case TouchPhase.Ended:   
                               
                               if (object_hit != null && IsATap())
                               {
                                object_hit.youve_been_touched();
                                Debug.Log("This a tap");
                                selectedObject = object_hit;
                                starting_distance_to_selected_object = Vector3.Distance(Camera.main.transform.position, info.transform.position);
                                selected = true;
                                }

                               else 
                                {
                                    selectedObject.deselect_object();
                                    selectedObject = camera;
                                }

                                break;

                            // Drag Object Code
                            case TouchPhase.Moved:

                                    Ray new_position_ray = Camera.main.ScreenPointToRay(Input.touches[0].position);

                                if (selectedObject is CapsuleControl)
                                {
                                    selectedObject.MoveTo(new_position_ray.GetPoint(starting_distance_to_selected_object));
                                    Debug.Log("Capsule Drag 2");
                                }

                                else
                                {
                                    selectedObject.MoveTo(new_position_ray.GetPoint(starting_distance_to_selected_object));
                                    Debug.Log("Normal Drag");
                                }


                                break;
                        }
                    }

                    if (Input.touchCount == 2)
                    {

                        Touch touch0 = Input.GetTouch(0);
                        Touch touch1 = Input.GetTouch(1);

                        if (touch0.phase == TouchPhase.Began || touch1.phase == TouchPhase.Began)
                        {
                            startDistance = Vector3.Distance(Input.GetTouch(0).position, Input.GetTouch(1).position);

                            startAngle = Mathf.Atan2(touch1.position.y - touch0.position.y, touch1.position.x - touch0.position.y);


                            if (selectedObject != null)
                            {
                                initialScale = info.transform.localScale;

                                startRotation = selectedObject.gameObject.transform.rotation;
                            }

                            else
                            {
                                startRotation = Camera.main.transform.rotation;

                            }
                        }

                        else if (touch0.phase == TouchPhase.Moved || touch1.phase == TouchPhase.Moved)
                        {
                            
                            newestDistance = Vector3.Distance(touch0.position, touch1.position);
                            newPos = touch1.position - touch0.position;
                            newAngle = Mathf.Atan2(newPos.y, newPos.x);
                            difference = newAngle - startAngle;
                            differenceInDegrees = Mathf.Rad2Deg * difference;

                            if (selectedObject.GetType() == typeof(CameraControl) && differenceInDegrees > 20f)
                            {

                                Debug.Log("Rotating Camera");
                                scaleSize = newestDistance / startDistance;
                                selectedObject.rotateTo(differenceInDegrees,startRotation);
                            }

                            else if (selectedObject.GetType() == typeof(CameraControl) && differenceInDegrees < 20f)
                            {

                                Debug.Log("Scaling Camera");
                                scaleSize = newestDistance / startDistance;
                                selectedObject.scaleObject(initialScale,scaleSize);
                            }

                            else 
                            {
                               if (differenceInDegrees > 20f)
                                {
                                    Debug.Log("Rotating");
                                    selectedObject.gameObject.transform.rotation = startRotation * Quaternion.AngleAxis(differenceInDegrees, Camera.main.transform.forward);
                                }

                                else
                                {
                                    scaleSize = newestDistance / startDistance;
                                    selectedObject.scaleObject(initialScale, scaleSize);
                                    Debug.Log("Scaling");
                                }
                            }

                           
                        }
                        
                    }
                }
            }
        }  
    }

     private bool IsATap()
     {
         timeOfTap = 0;

        if(Input.touches[0].phase == TouchPhase.Began)
         {
            startTime = Time.deltaTime;
         }

         if(Input.touches[0].phase == TouchPhase.Ended)
         {
             timeOfTap = Time.deltaTime - startTime;
        }

         if(timeOfTap <= tapThreshold && timeOfTap != 0)
         {
             print("Object Tapped");
            return true;
         }
       else
        {
            return false;
         }


     }

}
