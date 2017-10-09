using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Func_Mnr : MonoBehaviour {

    public static Func_Mnr A = null;

    void Awake() {
        A = this;
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    //控制物体激活状态的通用函数
    public void Active_Obj(string Nm_Obj,bool Bl) {

        Vrb_Mnr.A.Di_Obj[Nm_Obj].SetActive(Bl);
        //在变量管理器中通过参数中的字符串在字典中查询到具体的变量，并通过参数中的布尔值决定激活状态。

    }

    //控制声音播放的通用函数
    public void Au_Ctrl(string Nm_Au) {
        Vrb_Mnr.A.Au_Sc.clip = Vrb_Mnr.A.Di_Au[Nm_Au];
        //使用参数将变量脚本中对应的音频文件赋值到声音源组件中
        Vrb_Mnr.A.Au_Sc.Play();
    }

    //控制贴图替换的函数
    public void Te_Change(string Nm_Obj,string Nm_Te)
    {
        Vrb_Mnr.A.Di_Obj[Nm_Obj].GetComponent<Renderer>().material.mainTexture = Vrb_Mnr.A.Di_Te[Nm_Te];
    }
}
