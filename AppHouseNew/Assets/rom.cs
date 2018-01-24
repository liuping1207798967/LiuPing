using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rom : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	public void showsaa1()
    {
        Func_Mnr.A.Au_Ctrl("Au_Wait");
        StartCoroutine(roomss1());
    }
    IEnumerator roomss1()
    {
        yield return new WaitForSeconds(1f);
        Func_Mnr.A.Au_Ctrl("Au_Suitable");
        Func_Mnr.A.Active_Obj("model1", false);
        Func_Mnr.A.Active_Obj("rooms1", true);
    }


    public void showsaa2()
    {
        Func_Mnr.A.Au_Ctrl("Au_Wait");
        StartCoroutine(roomss2());
    }
    IEnumerator roomss2()
    {
        yield return new WaitForSeconds(1f);
        Func_Mnr.A.Au_Ctrl("Au_Suitable");
        Func_Mnr.A.Active_Obj("model2", false);
        Func_Mnr.A.Active_Obj("rooms2", true);
    }



    public void showsaa3()
    {
        Func_Mnr.A.Au_Ctrl("Au_Wait");
        StartCoroutine(roomss3());
    }
    IEnumerator roomss3()
    {
        yield return new WaitForSeconds(1f);
        Func_Mnr.A.Au_Ctrl("Au_Suitable");
        Func_Mnr.A.Active_Obj("model3", false);
        Func_Mnr.A.Active_Obj("rooms3", true);
    }

    void Update () {
		
	}
}
