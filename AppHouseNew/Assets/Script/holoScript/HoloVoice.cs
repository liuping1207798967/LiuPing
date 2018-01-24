using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
public class HoloVoice : MonoBehaviour
{
    public GameObject fu;
    public GameObject a;
    public GameObject b;
    private HoloBTap holoB;
    private HoloAtap holoA;
    //HoloClick clik;
    //HoloBTap Taps;
    // Use this for initialization
    void Start () {
        fu = GameObject.Find("Buildings");
        a = fu.transform.Find("An").gameObject;
        b = fu.transform.Find("Liang").gameObject;
        holoA = b.GetComponent<HoloAtap>();
        holoB = b.GetComponent<HoloBTap>();
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void RoomShow()
    {
        //funcs.A.ActiveAu("Au_Hello");
        holoB.enabled = false;
        holoA.enabled = true;
    }
    public void hignShow()
    {
        //funcs.A.ActiveAu("Au_OK");
        holoA.enabled =false;
        holoB.enabled = true ;
    }
    public void AandVoice()
    {
        //funcs.A.ActiveAu("Au_OK");
        a.SetActive(true);//an
        b.SetActive(false);
    }
    public void BandVoice()
    {
        //funcs.A.ActiveAu("Au_OK");
        a.SetActive(false);
        b.SetActive(true);//liang
    }
}
