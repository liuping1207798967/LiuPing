using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity;

public class geti : MonoBehaviour
{

    private GameObject fu;
    private Collider collide;
    private HoloZoom zoom;
    private HoloRotate rota;
    private TapToPlace_bd Places;
   
   // public GameObject zis;

    void Start()
    {
        fu = GameObject.Find("Buildings");
       // zis = GameObject.Find("An");
        collide = fu.GetComponent<Collider>();
        zoom = fu.GetComponent<HoloZoom>();
        rota = fu.GetComponent<HoloRotate>();
        Places = fu.GetComponent<TapToPlace_bd>();
        
    }
    void Update()
    {
    }
    public void beginsa()
    {
        Places.enabled = false;
        zoom.enabled = false;
        collide.enabled = true;
        rota.enabled = true;
    }
    public void nexts()
    {
        collide.enabled = true;
        zoom.enabled = false;
        Places.enabled = false;
        rota.enabled = true;
    }
    public void fase()
    {
        collide.enabled = false;
        zoom.enabled = false;
        rota.enabled = false;
        //taps.enabled = false;
        Places.enabled = false;
    }
    public void trues()
    {
        collide.enabled = true;
        zoom.enabled = true;
        rota.enabled = true;
       // taps.enabled = true;
    }
    public void zooms()
    {
        Places.enabled = false;
        collide.enabled = true;
        rota.enabled = false;
        //taps.enabled = false;
        zoom.enabled = true;
    }


    public void moves()
    {
        Places.enabled = false;
        collide.enabled = true;
        rota.enabled = false;
        zoom.enabled = false;
        //taps.enabled = true;
    }

    public void rotates()
    {
        Places.enabled = false;
        collide.enabled = true;
        zoom.enabled = false;
        //taps.enabled = false;
        rota.enabled = true;
    }
}
