using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;


public class buildmanager
    : MonoBehaviour, IInputClickHandler
{

    public GameObject[] Build;
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
    }

    // Update is called once per frame
    void Update()
    {

    }
    //实现点击接口
    public void OnInputClicked(InputClickedEventData eventData)
    {
        Build = GameObject.FindGameObjectsWithTag("Build");
        for (int i = 0; i < Build.Length; i++)
        {
            //如果凝视到的物体是fu
            if (GazeManager.Instance.HitObject == Build[i])
            {
               

                home = Build[i].GetComponent<HomePosition>();
                home.enabled = true; 
                homelist.Add(home); 
                
                if (Build[i].GetComponent<HandDraggable>() == null && Build[i].GetComponent<HoloZoom>() == null && Build[i].GetComponent<HoloRotate>() == null)
                {
                    //就添加这三个脚本，
                    move = Build[i].AddComponent<HandDraggable>();
                    move.enabled = false;//让他们都不激活
                    movelist.Add(move);//添加到数组
                    zoom = Build[i].AddComponent<HoloZoom>();
                    zoom.enabled = false;
                    zoomlist.Add(zoom);
                    rotate = Build[i].AddComponent<HoloRotate>();
                    rotate.enabled = false;
                    rotatelist.Add(rotate);
                }
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
    //移动
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
    //缩放
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
    //脚本
    public void Remove()
    {
        for (int i = 0; i < zoomlist.Count; i++)
        {
            zoomlist.Remove(zoomlist[i]);
        }
        for (int i = 0; i < rotatelist.Count; i++)
        {
            rotatelist.Remove(rotatelist[i]);
        }
        for (int i = 0; i < movelist.Count; i++)
        {
            movelist.Remove(movelist[i]);
        }
        Destroy(move);
        Destroy(rotate);
        Destroy(zoom);
    }

    //回到初始状态
    public void GoHome()
    {
        for (int i = 0; i < homelist.Count; i++)
        {
            homelist[i].Back();
            homelist[i].enabled = false;
            homelist.Remove(homelist[i]);
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
            //Destroy(movelist[i]);
        }
        for (int i = 0; i < Build.Length; i++)
        {
            Destroy(Build[i].GetComponent<HandDraggable>());
            Destroy(Build[i].GetComponent<HoloRotate>());
            Destroy(Build[i].GetComponent<HoloZoom>());
        }
    }
}
