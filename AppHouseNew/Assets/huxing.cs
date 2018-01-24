using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class huxing : MonoBehaviour {
    private GameObject fu;
    //private Collider collide;
    private HoloZoom zoom;
    private HoloRotate rota;
    private HandDraggable taps;
    // Use this for initialization
    void Start()
    {

        fu = GameObject.Find("Buildings");
       // collide = fu.GetComponent<Collider>();
        zoom = fu.GetComponent<HoloZoom>();
        rota = fu.GetComponent<HoloRotate>();
        taps = fu.GetComponent<HandDraggable>();
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void zooms()
    {

       // collide.enabled = true;
        rota.enabled = false;
        taps.enabled = false;
        zoom.enabled = true;
    }

    public void moves()
    {

       // collide.enabled = true;
        rota.enabled = false;
        zoom.enabled = false;
        taps.enabled = true;
    }

    public void rotates()
    {

       // collide.enabled = true;
        zoom.enabled = false;
        taps.enabled = false;
        rota.enabled = true;
    }
}
