using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Portal_Collider : MonoBehaviour
{

    public bool Bl_Inside = false;
    //记录使用者所处传送的哪边

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerExit(Collider col)
    {
        if (col.gameObject.tag.CompareTo("User")==0)
            //判断离开传送门碰撞器的这个碰撞器标签是否为User
        {
            Bl_Inside = !Bl_Inside;
            //更改所处状态
            Func_Mnr.A.Active_Obj("Obj_Mask_Outside",!Bl_Inside);
            //外部遮罩激活状态与布尔变量相反
            Func_Mnr.A.Active_Obj("Obj_Mask_Inside", Bl_Inside);
            //内部遮罩激活状态与布尔变量一致
            

        }
    }
}
