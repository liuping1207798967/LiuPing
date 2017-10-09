using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Mnr : MonoBehaviour
{
    public static UI_Mnr A=null;
    //单例

    public bool Bl_Load = false;
    //申请布尔变量控制进度条是否开始读取

    public Image Im_L_F;
    //储存进度条填充的Image组件

    public Text Tx_N;
    //储存百分比数字的Text组件

    public int int_Count=0;
    //作为帧数计时器

    public int int_N=0;
    //记录百分比数字

    public float f_F = 0f;
    //记录进度条填充进度

    void Awake()
    {
        A = this;
    }

    // Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void FixedUpdate()
    {
        if (Bl_Load)
        {
            Loading();
            Counting();
        }
        
    }

    //填充进度条函数
    void Loading()
    {
        Im_L_F.fillAmount = f_F;
        //进度填充的量等于填充进度

        //在尚未填充满前
        if (f_F<1.0f)
        {
            f_F += 0.005f;
            //每帧的填充量增加0.00f
        }
    }

    void Counting()
    {
        //当百分比数字没有达到100%前
        if (int_N<100)
        {
            int_Count++;
            //计时器每一帧加1
        }

        int_N = int_Count/2;
        //百分比数字增长的速度为帧速度的一半
        Tx_N.text = int_N.ToString();
        //给UGUI中的进度百分比数字赋值

    }
}
