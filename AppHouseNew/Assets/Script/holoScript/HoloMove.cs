using HoloToolkit.Unity.InputModule;
using UnityEngine;
using System;
//移动
public class HoloMove : MonoBehaviour, IManipulationHandler
{
    public float speed = 0.1f;
   //public CharacterController player;
    private Vector3 manipulationPreviousPosition;
    // Use this for initialization
    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
        throw new NotImplementedException();
    }

    public void OnManipulationCompleted(ManipulationEventData eventData)
    {
    }

    public void OnManipulationStarted(ManipulationEventData eventData)
    {

        funcs.A.ActiveAu("Au_jiemian");
        //设置初始位置
        manipulationPreviousPosition = eventData.CumulativeDelta;
        //player = GetComponent<CharacterController>();
    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
        funcs.A.ActiveAu("Au_jiemian");
        //计算相对位移，然后更新物体的位置
        Vector3 moveVector = Vector3.zero;
        moveVector = eventData.CumulativeDelta - manipulationPreviousPosition;
        manipulationPreviousPosition = eventData.CumulativeDelta;
        transform.position += moveVector;
       // player.Move(player.transform.TransformDirection(new Vector3()))
    }
}
