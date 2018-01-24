using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveHands : MonoBehaviour
{
    public float speed =0.1f;  //移动速度
    public float cubeSpeed = 10f;  //旋转速度
    public CharacterController player; //获取移动器，
     //声明摄像机变量
    public float Distance = 1f;  //缩放的初始值
    public float ZoomSpeed = 1f;  //缩放速度
    public float MinDistance = 0.1f; //缩放的最小值
    public float MaxDistance = 10f;  //缩放的最大值
    void Start () {
        player = GetComponent<CharacterController>();     
    }
	// Update is called once per frame
	void Update () {
        PlayerRotateDemo();
        PlayerMoveDemo();
        PlayerZoomDemo();       
    }
       //控制移动
    public void PlayerMoveDemo() 
    { //camera.transform.eulerAngles = new Vector3(45, 0, 0);
        float vx = Input.GetAxis("Horizontal");  //水平方向
        float vz = Input.GetAxis("Vertical");    //纵轴方向
       
        //控制主角移动
        player.Move(player.transform.TransformDirection(new Vector3(vx, 0, vz) * speed));
    }

    //控制缩放
    public void PlayerZoomDemo()
    {
        Distance += Input.GetAxis("Mouse ScrollWheel") * ZoomSpeed;
        Distance = Mathf.Clamp(Distance, MinDistance, MaxDistance);
        transform.localScale = new Vector3(Distance, Distance, Distance);
    }

    //控制旋转
    public void PlayerRotateDemo()
    {
        Vector3 cmEuler = this.transform.eulerAngles;
        if (Input.GetKey(KeyCode.Mouse0))
            { 
                cmEuler.y += Input.GetAxis("Mouse X") * cubeSpeed;
                this.transform.eulerAngles = cmEuler;              
            }
         //调整方向
        cmEuler.x = cmEuler.z = 0;
        this.transform.eulerAngles = cmEuler;
    }
}
