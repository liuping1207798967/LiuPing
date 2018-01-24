using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;
//旋转
public class HoloRotate : MonoBehaviour, IManipulationHandler
{
    public float rotationSensitivity = 10.0f;
    private float rotationFactor;
   // private bool isNa = false;
    void Start()
    {

    }
    void Update()
    {

    }
    //手势旋转物体。
    public void OnManipulationStarted(ManipulationEventData eventData)
    {
    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
        rotationFactor = eventData.CumulativeDelta.x * rotationSensitivity;
        transform.Rotate(new Vector3(0, -1 * rotationFactor, 0));
    }

    public void OnManipulationCompleted(ManipulationEventData eventData)
    {
        //isNa = false;
    }
    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
        rotationFactor = eventData.CumulativeDelta.x * rotationSensitivity;
        transform.Rotate(new Vector3(0, 1 * rotationFactor,0));
    }
}
