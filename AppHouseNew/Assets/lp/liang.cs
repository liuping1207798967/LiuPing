using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using System;

public class liang : MonoBehaviour, IInputClickHandler
{

    private liangshow active;
    private List<liangshow> activelist = new List<liangshow>();
    public GameObject[] build;

    private HandDraggable move;
    private HoloRotate rotate;
    private HoloZoom zoom;
    private HomePosition home;
    private List<HomePosition> homelist = new List<HomePosition>();
    private List<HandDraggable> movelist = new List<HandDraggable>();
    private List<HoloRotate> rotatelist = new List<HoloRotate>();
    private List<HoloZoom> zoomlist = new List<HoloZoom>();
    // Use this for initialization
    void Start()
    {
        build = GameObject.FindGameObjectsWithTag("Build");
    }

    // Update is called once per frame
    void Update()
    {


    }
    public void OnInputClicked(InputClickedEventData eventData)
    {
       
        build = GameObject.FindGameObjectsWithTag("Build");
        for (int i = 0; i < build .Length; i++)
        {
            //如果凝视到的物体是找到的物体
            if (GazeManager.Instance.HitObject == build [i])
            {
               

                home = build [i].GetComponent<HomePosition>();
                home.enabled = true;  
                homelist.Add(home);  
           
                if (build[i].GetComponent<HandDraggable>() == null && build [i].GetComponent<HoloZoom>() == null && build[i].GetComponent<HoloRotate>() == null)
                {
                    //就添加这三个脚本，让他们都不激活，并且添加到数组
                    move = build [i].AddComponent<HandDraggable>();//移动
                    move.enabled = false;
                    movelist.Add(move);
                    zoom = build [i].AddComponent<HoloZoom>();//缩放
                    zoom.enabled = false;
                    zoomlist.Add(zoom);
                    rotate = build [i].AddComponent<HoloRotate>();//旋转
                    rotate.enabled = false;
                    rotatelist.Add(rotate);
                }
            }
        }
        HideOfShowDemo();
    }
    void HideOfShowDemo()
    {
        build = GameObject.FindGameObjectsWithTag("Build");
        for (int i = 0; i < build.Length; i++)
        {
            if (GazeManager.Instance.HitObject == build [i])
            {
                active = build [i].GetComponent<liangshow>();
                active.enabled = true;
                activelist.Add(active);
            }
        }
    }

    //实现旋转
    public void RotateWay()
    {
        for (int i = 0; i < rotatelist.Count; i++)
        {
            rotatelist[i].enabled = true;
        }
        for (int i = 0; i < movelist.Count; i++)
        {
            movelist[i].enabled = false;
        }
        for (int i = 0; i < zoomlist.Count; i++)
        {
            zoomlist[i].enabled = false;
        }
    }
    //实现移动
    public void MoveWay()
    {
        for (int i = 0; i < movelist.Count; i++)
        {
            movelist[i].enabled = true;
        }
        for (int i = 0; i < rotatelist.Count; i++)
        {
            rotatelist[i].enabled = false;
        }
        for (int i = 0; i < zoomlist.Count; i++)
        {
            zoomlist[i].enabled = false;
        }
    }
    //实现缩放
    public void ZoomWay()
    {
        for (int i = 0; i < zoomlist.Count; i++)
        {
            zoomlist[i].enabled = true;
        }
        for (int i = 0; i < rotatelist.Count; i++)
        {
            rotatelist[i].enabled = false;
        }
        for (int i = 0; i < movelist.Count; i++)
        {
            movelist[i].enabled = false;
        }
    }
    public void GoBack()
    {
        for (int i = 0; i < homelist.Count; i++)
        {
            homelist[i].Back();
            homelist[i].enabled = false;
            homelist.Remove(homelist[i]);
        }
        for (int i = 0; i < activelist.Count; i++)
        {
            activelist[i].Show();
            activelist[i].enabled = false;
            activelist.Remove(activelist[i]);
        }
        for (int i = 0; i < zoomlist.Count; i++)
        {
            zoomlist.Remove(zoomlist[i]);
            //Destroy(zoomlist[i]);
        }
        for (int i = 0; i < rotatelist.Count; i++)
        {
            rotatelist.Remove(rotatelist[i]);
            //Destroy(rotatelist[i]);
        }
        for (int i = 0; i < movelist.Count; i++)
        {
            movelist.Remove(movelist[i]);
            // Destroy(movelist[i]);
        }
        for (int i = 0; i < build .Length; i++)
        {
            Destroy(build [i].GetComponent<HandDraggable>());
            Destroy(build [i].GetComponent<HoloRotate>());
            Destroy(build [i].GetComponent<HoloZoom>());
        }
    }

    public void HideDemo()
    {
        for (int i = 0; i < activelist.Count; i++)
        {
            activelist[i].Hide();
        }
    }
    public void ShowDemo()
    {
        for (int i = 0; i < activelist.Count; i++)
        {
            activelist[i].Show();
        }
    }


}
