using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
using HoloToolkit.Unity;

using UnityEngine.SceneManagement;


public class Scene_tiao : MonoBehaviour
{

    void Start()
    {
    }
    public void tiaosceneO()
    {
        //funcs.A.ActiveAu("Au_Wait");
        //funcs.A.ActiveAu("Au_Suitable");
        SceneManager.LoadScene(0);//firstui
    }

    public void tiaosceneone()
    {
        funcs.A.ActiveAu("Au_Wait");

        //funcs.A.ActiveAu("Au_Wait");
        //funcs.A.ActiveAu("Au_Suitable");
        SceneManager.LoadScene(1);
    }
    public void tiaosceneHuxin()
    {
        
        SceneManager.LoadScene(2);
    }
   
    void Update()
    {

    }
}
