using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class liangshow : MonoBehaviour
{

    private MeshRenderer mesh;
    private Collider coll;
    private GameObject obj;
    // Use this for initialization
    void Start()
    {

        mesh = this.gameObject.GetComponent<MeshRenderer>();
        coll = this.gameObject.GetComponent<Collider>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    //隐藏高亮的建筑，显示户型
    public void Hide()
    {
        coll.enabled = false;
        obj = this.transform.Find("Room").gameObject;
        obj.GetComponent<MeshRenderer>().enabled = true;
        obj.GetComponent<Collider>().enabled = true;
    }
    //隐藏户型，显示高亮的建筑
    public void Show()
    {
        coll.enabled = true;
        obj = this.transform.Find("Room").gameObject;
        obj.GetComponent<MeshRenderer>().enabled = false;
        obj.GetComponent<Collider>().enabled = false;
    }
}
