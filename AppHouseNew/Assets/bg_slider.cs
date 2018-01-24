using HoloToolkit.Unity.InputModule;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class bg_slider : MonoBehaviour//, IInputClickHandler
{
    public GameObject slide;
  public  void wellpictures()
    {
        StartCoroutine(Sw_slider());
    }
    IEnumerator Sw_slider()
    {
        funcs.A.ActiveAu("Au_Wait");
        yield return new WaitForSeconds(3f);
        funcs.A.Activeobj("sliderss", true);
        StartCoroutine(slidershow());
    }

    IEnumerator slidershow()
    {
        yield return new WaitForSeconds(1f);
        funcs.A.Activeobj("tiao", true);
        UI_Mnr.A.Bl_Load = true;
        StartCoroutine(Sw_Load());
        print(slide + "1");
    }
    IEnumerator Sw_Load()
    {
        yield return new WaitForSeconds(5f);
        funcs.A.ActiveAu("Au_OK");
        funcs.A.Activeobj("rights", true);
        funcs.A.Activeobj("sliderss", false);
        funcs.A.Activeobj("tiao", false);
    }

    void Update()
    {
        //pinputlicked();
    }


}
