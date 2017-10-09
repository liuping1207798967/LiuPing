using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;

public class S_Bd_Click : MonoBehaviour,IInputClickHandler {

    public void OnInputClicked(InputClickedEventData eventData)
    {
       
        Func_Mnr.A.Active_Obj("Obj_S_Bd_H",true);
        Func_Mnr.A.Active_Obj("Obj_S_Bd_O", false);

    }

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
