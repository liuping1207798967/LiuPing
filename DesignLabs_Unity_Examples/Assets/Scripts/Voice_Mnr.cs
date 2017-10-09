using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity;


public class Voice_Mnr : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //定位模式函数
    public void Place_Mode() {
        Vrb_Mnr.A.Obj_Bds.GetComponent<TapToPlace_Bd>().enabled = true;
        Vrb_Mnr.A.Obj_Bds.GetComponent<Rotate_Bd>().enabled = false;
    }

    //手动拖拽旋转模式函数
    public void Rotation_Mode()
    {
        if (Vrb_Mnr.A.Obj_Bds.GetComponent<TapToPlace_Bd>().IsBeingPlaced==false) {
            Vrb_Mnr.A.Obj_Bds.GetComponent<TapToPlace_Bd>().enabled = false;
            Vrb_Mnr.A.Obj_Bds.GetComponent<Rotate_Bd>().enabled = true;
        }
        
    }

    //切换到查询模式
    public void Check_Mode() {
        if (Vrb_Mnr.A.Obj_Bds.GetComponent<TapToPlace_Bd>().IsBeingPlaced == false)
        {
            Vrb_Mnr.A.Obj_Bds.GetComponent<BoxCollider>().enabled = false;
            //取消碰撞器组件
            Vrb_Mnr.A.Obj_Bds.GetComponent<TapToPlace_Bd>().enabled = false;
            Vrb_Mnr.A.Obj_Bds.GetComponent<Rotate_Bd>().enabled = false;
        }
            
    }

    //语音控制查找户型
    public void OnFind() {
        Func_Mnr.A.Au_Ctrl("Au_Wait");
        //提示等待 正在查询
        StartCoroutine(Sw_Room_H());
    }

    //延迟显示全息户型
    IEnumerator Sw_Room_H() {
        yield return new WaitForSeconds(3f);
        Func_Mnr.A.Au_Ctrl("Au_Suitable");
        Func_Mnr.A.Active_Obj("Obj_S_Room_H", true);
    }

    //语音获取户型的详细信息
    public void OnDetails()
    {
        Func_Mnr.A.Au_Ctrl("Au_Wait");
        //提示等待 正在查询
        StartCoroutine(Sw_Load());
        //调用显示UI的延迟函数
    }

    //显示UI的延迟函数
    IEnumerator Sw_Load()
    {
        yield return new WaitForSeconds(3f);
        //延迟三秒
        Func_Mnr.A.Active_Obj("Obj_Im_L_BG",true);
        Func_Mnr.A.Active_Obj("Obj_Im_L_F", true);
        UI_Mnr.A.Bl_Load = true;
        //让进度条开始加载

        StartCoroutine(Sw_Plane());
        //调用显示功能面板的延迟函数
    }

    //显示功能面板的延迟函数
    IEnumerator Sw_Plane()
    {
        yield return new WaitForSeconds(5f);
        //延迟5秒
        Func_Mnr.A.Au_Ctrl("Au_Check");
        //提示显示了户型的具体信息选项
        Func_Mnr.A.Active_Obj("Obj_Plane_Video",true);
        //显示视频播放面板
        Func_Mnr.A.Active_Obj("Obj_Plane_Portal",true);
        //显示传送门功能面板
    }
}
