using HoloToolkit.Unity.InputModule;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class obj : MonoBehaviour
{
    void Start()
    {
        
    }

    //public void Scales()
    //{
    //    funcs.A.ActiveAu("Au_OK");
    //    adds.A.objbd.GetComponent<TapToPlace_bd>().enabled = false;
    //    adds.A.objbd.GetComponent<HoloRotate>().enabled = false;
    //    adds.A.objbd.GetComponent<beforepos>().enabled = false;
    //    adds.A.objbd.GetComponent<HoloZoom>().enabled = true;
    //    print("ss");
    //}
    //public void rotates()
    //{
    //    funcs.A.ActiveAu("Au_OK");
    //    adds.A.objbd.GetComponent<TapToPlace_bd>().enabled = false;
    //    adds.A.objbd.GetComponent<HoloZoom>().enabled = false;
    //    adds.A.objbd.GetComponent<beforepos>().enabled = false;
    //    adds.A.objbd.GetComponent<HoloRotate>().enabled = true;
    //    print("rr");
    //}

    public void nextscene()
    {

        //funcs.A.ActiveAu("Au_OK");
        //adds.A.objbd.GetComponent<beforepos>().enabled = true;
        //adds.A.objbd.GetComponent<TapToPlace_bd>().enabled = false;
        //adds.A.objbd.GetComponent<HoloZoom>().enabled = false;
        //adds.A.objbd.GetComponent<HoloRotate>().enabled = false;
        StartCoroutine(nexts());
    }
    IEnumerator nexts()//come 到新的 2 场景
    {
        funcs.A.ActiveAu("Au_Wait");
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(2);
    }
    // Update is called {once per frame
   public  void lasts()
    {
        StartCoroutine(lastexts());
        print("back");
    }
    IEnumerator lastexts()
    {
        yield return new WaitForSeconds(1f);
        SceneManager.LoadScene(0);
    }
}
