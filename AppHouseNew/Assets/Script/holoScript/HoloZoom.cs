using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;
//缩放
public class HoloZoom : MonoBehaviour, IManipulationHandler
{

    private Vector3 navigationPreviousPosition; //初始位置
    private  float MaxScale = 5f; //最大值
   private  float MinScale = 0.5f; //最小值
    private bool isNa = false;
    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
    public void OnManipulationStarted(ManipulationEventData eventData)
    {
        navigationPreviousPosition = eventData.CumulativeDelta;
    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
        Vector3 deltaScale = Vector3.zero;
        float Scalevalue = 0.01f;
        if (eventData.CumulativeDelta.x < 0)
        {
            Scalevalue = -1 * Scalevalue;
        }
        //当缩放超出设置的最大，最小范围时直接返回
        if (transform.localScale.x >= MaxScale && Scalevalue > 0)
        {
            return;
        }
        else if (transform.localScale.x <= MinScale && Scalevalue < 0)
        {
            return;
        }
        //根据比例计算每个方向上的缩放大小
        deltaScale.x = Scalevalue;
        deltaScale.y = (transform.localScale.y / transform.localScale.x) * Scalevalue;
        deltaScale.z = (transform.localScale.z / transform.localScale.x) * Scalevalue;
        transform.localScale += deltaScale;
    }

    public void OnManipulationCompleted(ManipulationEventData eventData)
    {
        isNa = false;
    }

    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
        throw new NotImplementedException();
    }
}
