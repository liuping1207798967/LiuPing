using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnMouseHoverDemo : MonoBehaviour
{
    public Camera camera1; //获取摄像机

    //public float cmHeight = 5f;
    private Ray ray; //创建一条射线
    private RaycastHit hit;  //射线碰到的物体
    public List<MoveHands> Bag = new List<MoveHands>();   //创建一个数组储存MoveHands组件
    public List<InitialDemo> initial = new List<InitialDemo>();//创建一个数组储存InitialDemo组件 
    public List<SphereCubeActive> active = new List<SphereCubeActive>();
    void Start()
    {

        camera1 = Camera.main;
    }

    // Update is called once per frame
    void Update()
    {
        ColledDemo();
        CameraMoveDemo();
        //ColledDemo2();
        Delete();
        HideShow();
    }

    void CameraMoveDemo()
    {
        //获取摄像机欧拉角
        Vector3 cmEuler = camera1.transform.eulerAngles;
        if (Input.GetKey(KeyCode.Mouse1))
        {
            //摄像机旋转
            cmEuler.y += Input.GetAxis("Mouse X");
            cmEuler.x -= Input.GetAxis("Mouse Y");
            camera1.transform.eulerAngles = cmEuler;
        }
    }
    //为鼠标点击的物体添加脚本，把脚本放进数组
    void ColledDemo()
    {
        MoveHands move;
        InitialDemo p;
        SphereCubeActive s;
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out hit))
        {

            p = hit.collider.GetComponent<InitialDemo>();
            s = hit.collider.GetComponent<SphereCubeActive>();
            //hit.collider.tag == "cube"
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                if (hit.collider.tag == "sphere")
                {

                    if (p != null)
                    {

                        int num = 0;
                        foreach (InitialDemo item in initial)
                        {

                            if (item != p)
                            {

                                num++;
                            }
                        }
                        if (num == initial.Count)
                        {

                            initial.Add(p);
                        }


                        if (s != null)
                        {

                            int temp = 0;
                            foreach (SphereCubeActive it in active)
                            {

                                if (it != s)
                                {

                                    temp++;
                                }
                            }
                            if (temp == active.Count)
                            {
                                active.Add(s);
                            }
                        }
                    }
                }
                if (hit.collider.tag == "cube")
                {
                    if (p != null)
                    {

                        int num = 0;
                        foreach (InitialDemo item in initial)
                        {

                            if (item != p)
                            {

                                num++;
                            }
                        }
                        if (num == initial.Count)
                        {

                            initial.Add(p);
                        }
                    }

                }

                if (hit.collider.GetComponent<MoveHands>() == null)  //当鼠标碰到的物体没有MoveHands组件时
                {
                    move = hit.collider.gameObject.AddComponent<MoveHands>();  //获取组件
                    Bag.Add(move);  //添加给数组
                }
            }
        }
    }
    void Delete()
    {



        if (Input.GetKey(KeyCode.B))  //点击b
        {
            for (int i = 0; i < initial.Count; i++)
            {
                initial[i].Back();   //执行数组里InitialDemo脚本的方法
                initial.Remove(initial[i]);  //删除数组里的脚本
            }
            for (int i = 0; i < Bag.Count; i++)  //遍历数组
            {
                Destroy(Bag[i]);  //删除物体上的脚本
                Bag.Remove(Bag[i]); //删除数组里的脚本
            }
            for (int j = 0; j < active.Count; j++)
            {
                active[j].Show();
                active.Remove(active[j]);

            }
        }

    }
    int a = 0;
    void HideShow()
    {
        switch (a)
        {
            case 0:
                for (int i = 0; i < active.Count; i++)
                {
                    if (Input.GetKeyDown(KeyCode.M))
                    {
                        active[i].Hide();
                        a = 1;
                    }
                }
                break;
            case 1:
                for (int i = 0; i < active.Count; i++)
                {
                    if (Input.GetKeyDown(KeyCode.M))
                    {
                        active[i].Show();
                        a = 0;
                    }
                }
                break;
        }
    }
}
