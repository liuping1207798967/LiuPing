using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Vrb_Mnr : MonoBehaviour {

    public Dictionary<string, GameObject> Di_Obj;
    //申请字典 用来对应字符串与游戏对象的关系
    public Dictionary<string, AudioClip> Di_Au;
    //申请字典 用来对应字符串与音频文件的关系
    public Dictionary<string, Texture> Di_Te;
    //申请字典 用来对应字符串与贴图的关系 

    public static Vrb_Mnr A=null;
    //单例

    public AudioSource Au_Sc;
    //储存声音源组件

    public GameObject Obj_Bds;
    //储存楼盘模型

    public GameObject Obj_S_Bd_O;
    //储存具体建筑的原始模型
    public GameObject Obj_S_Bd_H;
    //储存具体建筑的全息模型
    public GameObject Obj_S_Room_H;
    //储存具体户型的全息模型
    public GameObject Obj_Im_L_BG;
    //储存进度条的背景图图片组件
    public GameObject Obj_Im_L_F;
    //储存进度条填充图片组件
    public GameObject Obj_Plane_Video;
    //储存播放视频的面片
    public GameObject Obj_Plane_Portal;
    //储存传送门功能的面片
    public GameObject Obj_Room;
    //储存整个户型模块
    public GameObject Obj_Mask_Outside;
    //储存在传送门外时的遮罩
    public GameObject Obj_Mask_Inside;
    //储存在传送门内时的遮罩

    //储存音频文件
    public AudioClip Au_Hello;
    public AudioClip Au_Place;
    public AudioClip Au_Wait;
    public AudioClip Au_Suitable;
    public AudioClip Au_OK;
    public AudioClip Au_Check;

    //储存贴图文件
    public Texture Te_Portal_A;
    public Texture Te_Portal_B;
    public Texture Te_Video_A;
    public Texture Te_Video_B;

    void Awake() {
        A = this;
    }

	// Use this for initialization
	void Start () {
        Di_Obj = new Dictionary<string, GameObject>();
        Di_Obj.Add("Obj_Bds", Obj_Bds); //储存楼盘模型
        Di_Obj.Add("Obj_S_Bd_O", Obj_S_Bd_O);//储存具体建筑的原始模型
        Di_Obj.Add("Obj_S_Bd_H", Obj_S_Bd_H); //储存具体建筑的全息模型
        Di_Obj.Add("Obj_S_Room_H", Obj_S_Room_H);//储存具体户型的全息模型
        Di_Obj.Add("Obj_Im_L_BG", Obj_Im_L_BG);//储存进度条的背景图图片组件
        Di_Obj.Add("Obj_Im_L_F", Obj_Im_L_F);//储存进度条填充图片组件
        Di_Obj.Add("Obj_Plane_Video", Obj_Plane_Video); //储存播放视频的面片
        Di_Obj.Add("Obj_Plane_Portal", Obj_Plane_Portal);   //储存传送门功能的面片
        Di_Obj.Add("Obj_Room", Obj_Room);                  //储存整个户型模块
        Di_Obj.Add("Obj_Mask_Outside", Obj_Mask_Outside);//储存在传送门外时的遮罩
        Di_Obj.Add("Obj_Mask_Inside", Obj_Mask_Inside);//储存在传送门内时的遮罩



        Di_Au = new Dictionary<string, AudioClip>();//储存音频文件
        Di_Au.Add("Au_Hello", Au_Hello);
        Di_Au.Add("Au_Place", Au_Place);
        Di_Au.Add("Au_Wait", Au_Wait);
        Di_Au.Add("Au_Suitable", Au_Suitable);
        Di_Au.Add("Au_OK", Au_OK);
        Di_Au.Add("Au_Check", Au_Check);

        Di_Te=new Dictionary<string, Texture>();//储存贴图文件
        Di_Te.Add("Te_Portal_A", Te_Portal_A);
        Di_Te.Add("Te_Portal_B", Te_Portal_B);
        Di_Te.Add("Te_Video_A", Te_Video_A);
        Di_Te.Add("Te_Video_B", Te_Video_B);

    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
