using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;

public class HoloAtap : MonoBehaviour {
    private HandDraggable move;
    private HoloRotate rotate;
    private HoloZoom zoom;
    private HomePosition home;
    private SphereCubeActive active;
    private List<HomePosition> homelist = new List<HomePosition>();
    private List<SphereCubeActive> activelist = new List<SphereCubeActive>();
    private List<HandDraggable> movelist = new List<HandDraggable>();
    private List<HoloRotate> rotatelist = new List<HoloRotate>();
    private List<HoloZoom> zoomlist = new List<HoloZoom>();
    GameObject[] Fang;
    Transform Qiu;
    HoloVoice voice;
    // Use this for initialization
    void Start()
    {
        //cu = GameObject.Find("Cursor");
        //voice = new HoloVoice();
    }
    // Update is called once per frame
    void Update()
    {
    }
    public void OnInputClicked(InputClickedEventData eventData)
    {
        //找到标签为sphere的物体
        Fang = GameObject.FindGameObjectsWithTag("Model");
        for (int i = 0; i < Fang.Length; i++)
        {
            //凝视的物体是找到的物体
            if (GazeManager.Instance.HitObject ==Fang[i])
            {
                home = Fang[i].GetComponent<HomePosition>();
                home.enabled = true;
                active = Fang[i].GetComponent<SphereCubeActive>();
                move = Fang[i].GetComponent<HandDraggable>();
                zoom = Fang[i].GetComponent<HoloZoom>();
                rotate = Fang[i].GetComponent<HoloRotate>();
                homelist.Add(home);
                active.enabled = true;
                activelist.Add(active);
                movelist.Add(move);
                zoomlist.Add(zoom);
                rotatelist.Add(rotate);
            }
        }
    }
    //旋转点击
    public void RotateWay()
    {
        // funcs.A.ActiveAu("Au_jiemian");
        for (int i = 0; i < movelist.Count; i++)
        {
            movelist[i].enabled = false;
        }
        for (int i = 0; i < zoomlist.Count; i++)
        {
            zoomlist[i].enabled = false;
        }
        for (int i = 0; i < rotatelist.Count; i++)
        {
            rotatelist[i].enabled = true;
        }
    }
    //移动点击
    public void MoveWay()
    {
        for (int i = 0; i < movelist.Count; i++)
        {
            movelist[i].enabled = true;
        }
        for (int i = 0; i < zoomlist.Count; i++)
        {
            zoomlist[i].enabled = false;
        }
        for (int i = 0; i < rotatelist.Count; i++)
        {
            rotatelist[i].enabled = false;
        }
    }
    //缩放点击
    public void ZoomWay()
    {
        //  funcs.A.ActiveAu("Au_jiemian");
        for (int i = 0; i < movelist.Count; i++)
        {
            movelist[i].enabled = false;
        }
        for (int i = 0; i < zoomlist.Count; i++)
        {
            zoomlist[i].enabled = true; ;
        }
        for (int i = 0; i < rotatelist.Count; i++)
        {
            rotatelist[i].enabled = false;
        }
    }
    //物体回到初始状态
    public void Home()
    {
        for (int i = 0; i < homelist.Count; i++)
        {
            homelist[i].Back();
            homelist.Remove(homelist[i]);
        }
        for (int i = 0; i < movelist.Count; i++)
        {
            movelist[i].enabled = false;
            movelist.Remove(movelist[i]);
        }
        for (int i = 0; i < zoomlist.Count; i++)
        {
            zoomlist[i].enabled = false;
            zoomlist.Remove(zoomlist[i]);
        }
        for (int i = 0; i < rotatelist.Count; i++)
        {
            rotatelist[i].enabled = false;
            rotatelist.Remove(rotatelist[i]);
        }
        for (int i = 0; i < homelist.Count; i++)
        {
            homelist[i].enabled = false;
            homelist.Remove(homelist[i]);
        }
        for (int i = 0; i < activelist.Count; i++)
        {
            activelist[i].Show();
            active.enabled = false;
            activelist.Remove(activelist[i]);
        }
    }
    //隐藏球显示小房间
    public void Active()
    {

        for (int i = 0; i < activelist.Count; i++)
        {
            activelist[i].Hide();
        }
    }
    //隐藏方块显示高亮
    public void Passive()
    {
        for (int i = 0; i < activelist.Count; i++)
        {
            activelist[i].Show();
        }
    }

}
