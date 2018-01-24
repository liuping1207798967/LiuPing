using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Allsay : MonoBehaviour
{
    public Dictionary<string, GameObject> Di;

    public Dictionary<string, AudioClip> Di_Au;


    public GameObject sliderss;
    public GameObject tiao;
    public GameObject rights;

    public static Allsay A = null;
    public AudioSource Au_sc;

    public AudioClip Au_Hello;
    public AudioClip Au_Wait;
    public AudioClip Au_OK;
    public AudioClip Au_jiemian;
    public AudioClip Au_check;
    void Awake()
    {
        A = this;
    }
    public void Start()
    {
        Di = new Dictionary<string,GameObject >();

        Di.Add("sliderss", sliderss);
        Di.Add("tiao", tiao);
        Di.Add("rights", rights);

        Di_Au = new Dictionary<string, AudioClip>();
        Di_Au.Add("Au_check", Au_check);
        Di_Au.Add("Au_Hello", Au_Hello);
        Di_Au.Add("Au_Wait", Au_Wait);
        Di_Au.Add("Au_OK", Au_OK);
        Di_Au.Add("Au_jiemian", Au_jiemian);
    }
    void Update()
    {

    }
}
