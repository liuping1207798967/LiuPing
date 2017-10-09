using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;

public class Plane_Portal : MonoBehaviour,IFocusable,IInputClickHandler {

    public void OnFocusEnter()
    {
        Func_Mnr.A.Te_Change("Obj_Plane_Portal", "Te_Portal_B");
    }


    public void OnFocusExit()
    {
        Func_Mnr.A.Te_Change("Obj_Plane_Portal", "Te_Portal_A");
    }

    public void OnInputClicked(InputClickedEventData eventData)
    {
        Func_Mnr.A.Active_Obj("Obj_Room",true);
        //显示户型模块 （即开启传送门）
        Func_Mnr.A.Active_Obj("Obj_Bds", false);
        //关闭沙盘模块
    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
