using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class mnr : MonoBehaviour {

    public static mnr A = null;
    public GameObject Obj_Bds;
    public GameObject room;
    //储存楼盘模型
    void Start () {
		
	}
    void Awake()
    {
        A = this;
    }

    // Update is called once per frame
    void Update () {
		
	}
}
