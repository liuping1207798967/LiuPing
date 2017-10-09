using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HoloToolkit.Unity.InputModule;
//引用holotoolkit中输入模块的命名空间

public class Rotate_Bd : MonoBehaviour,INavigationHandler {
    //"INavigationHandler"处理手势导航的接口 I代表Interface就是接口的意思

    private float Difference;

    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnNavigationStarted(NavigationEventData eventData) {

    }

    public void OnNavigationUpdated(NavigationEventData eventData) {
        Difference = eventData.CumulativeDelta.x;
        //获取手势导航中 导航数据内手势操作在x轴上的偏移值

        transform.Rotate(new Vector3(0,Difference*-1,0));
        //使用手势的差值作为标准让物体旋转起来
    }

    public void OnNavigationCompleted(NavigationEventData eventData) {

    }

    public void OnNavigationCanceled(NavigationEventData eventData) {

    }
}
