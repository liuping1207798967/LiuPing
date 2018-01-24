using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class clickbuild : MonoBehaviour, IInputClickHandler
{

    private TapToPlace_bd move;
    private HoloRotate rotate;
    private HoloZoom zoom;
    private HomePosition home;
    private SphereCubeActive active;
    private List<HomePosition> homelist = new List<HomePosition>();
    private List<TapToPlace_bd> movelist = new List<TapToPlace_bd>();
    private List<HoloRotate> rotatelist = new List<HoloRotate>();
    private List<HoloZoom> zoomlist = new List<HoloZoom>();
    GameObject a;

    public void OnInputClicked(InputClickedEventData eventData)
    {
        a = GameObject.FindGameObjectWithTag("a");
        print(a);
        if (GazeManager.Instance.HitObject == a)
        {
            home = a.GetComponent<HomePosition>();
            home.enabled = true;
            homelist.Add(home);
            move = a.GetComponent<TapToPlace_bd>();
            movelist.Add(move);
            zoom = a.GetComponent<HoloZoom>();
            zoomlist.Add(zoom);
            rotate = a.GetComponent<HoloRotate>();
            rotatelist.Add(rotate);
        }
    }
    //旋转点击
    public void RotateWay()
    {
        rotatelist.Add(rotate);
        move.enabled = false;
        zoom.enabled = false;
        rotate.enabled = true;

    }
    //移动点击
    public void MoveWay()
    {
        movelist.Add(move);
        move.enabled = true;
        zoom.enabled = false;
        rotate.enabled = false;
    }
    //缩放点击
    public void ZoomWay()
    {
        zoomlist.Add(zoom);
        move.enabled = false;
        movelist.Remove(move);
        zoom.enabled = true;
        zoomlist.Remove(zoom);
        rotate.enabled = false;
        rotatelist.Remove(rotate);
    }
    //将物体返回到初始状态
    public void Home()
    {
        homelist.Add(home);
        home.Back();
        move.enabled = false;
        zoom.enabled = false;
        rotate.enabled = false;
        home.enabled = false;
    }

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
