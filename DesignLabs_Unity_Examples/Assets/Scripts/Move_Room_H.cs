using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;

public class Move_Room_H : MonoBehaviour,IManipulationHandler {

    private Vector3 Pst_O;
    //记录手势起始时物体的初始位置

    private Vector3 Pst_D;
    //记录手势移动的差值

    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
        
    }

    public void OnManipulationCompleted(ManipulationEventData eventData)
    {
        
    }

    public void OnManipulationStarted(ManipulationEventData eventData)
    {
        Pst_O = transform.position;
        //手势起始时 把物体的位置记录在Pst_O中
    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
        Pst_D = new Vector3(eventData.CumulativeDelta.x, eventData.CumulativeDelta.y, eventData.CumulativeDelta.z);
        //记录手势移动时三个坐标轴上的差值
        transform.position = Pst_O + Pst_D;
        //更改物体的位置
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
