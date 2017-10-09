using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;
using UnityEngine.Video;

public class Plane_Video : MonoBehaviour,IFocusable,IInputClickHandler
{

    public bool Bl_Play = false;
    //记录视频的播放状态

    public void OnFocusEnter()
    {
        Func_Mnr.A.Te_Change("Obj_Plane_Video", "Te_Video_B");

    }

    public void OnFocusExit()
    {
        Func_Mnr.A.Te_Change("Obj_Plane_Video", "Te_Video_A");
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        if (!Bl_Play)
        {
            gameObject.GetComponent<VideoPlayer>().enabled = true;
            gameObject.GetComponent<VideoPlayer>().Play();
        }
        else
        {
            gameObject.GetComponent<VideoPlayer>().Stop();
            gameObject.GetComponent<VideoPlayer>().enabled = false;
            Func_Mnr.A.Te_Change("Obj_Plane_Video", "Te_Video_A");
        }
        Bl_Play = !Bl_Play;
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
