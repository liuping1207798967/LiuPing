using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Vrb_Mnr : MonoBehaviour
{

    public Dictionary<string, GameObject> Di_Obj;
    //申请字典 用来对应字符串与游戏对象的关系
    public Dictionary<string, AudioClip> Di_Au;
    //申请字典 用来对应字符串与音频文件的关系

    public static Vrb_Mnr A = null;
    //单例

    public AudioSource Au_Sc;
    //储存声音源组件

    public GameObject Obj_Bds;
    //储存楼盘模型

    //public GameObject Obj_S_Bd_O;
    ////储存具体建筑的原始模型
    //public GameObject Obj_S_Bd_H;
    ////储存具体建筑的全息模型
    //public GameObject Obj_S_Room_H;
    //储存具体户型的全息模型

    //储存音频文件
    public AudioClip Au_Hello;
    public AudioClip Au_Place;
    public AudioClip Au_Wait;
    public AudioClip Au_Suitable;
    public AudioClip Au_OK;
    public AudioClip Au_Check;
    public AudioClip jiemian;

    public GameObject rooms1;
    public GameObject rooms2;
    public GameObject rooms3;
    public GameObject model1;
    public GameObject model2;
    public GameObject model3;
    //public GameObject rooms7;
    //public GameObject rooms8;
    //public GameObject rooms9;
    //public GameObject rooms10;




    void Awake()
    {
        A = this;
    }

    // Use this for initialization
    void Start()
    {
        Di_Obj = new Dictionary<string, GameObject>();
        Di_Obj.Add("Obj_Bds", Obj_Bds);
        Di_Obj.Add("rooms1", rooms1);
        Di_Obj.Add("rooms2", rooms2);
        Di_Obj.Add("rooms3", rooms3);
        Di_Obj.Add("model1", model1);
        Di_Obj.Add("model2", model2);
        Di_Obj.Add("model3", model3);



        Di_Au = new Dictionary<string, AudioClip>();
        Di_Au.Add("Au_Hello", Au_Hello);
        Di_Au.Add("Au_Place", Au_Place);
        Di_Au.Add("Au_Wait", Au_Wait);
        Di_Au.Add("Au_Suitable", Au_Suitable);
        Di_Au.Add("Au_OK", Au_OK);
        Di_Au.Add("Au_Check", Au_Check);
        Di_Au.Add("jiemian", jiemian);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
