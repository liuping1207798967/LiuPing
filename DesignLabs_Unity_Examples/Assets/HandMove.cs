using HoloToolkit.Unity.InputModule;
using UnityEngine;

public class HandMove : MonoBehaviour, INavigationHandler, IManipulationHandler
{
    [Tooltip("旋转速度")]
    public float RotationSensitivity = 10.0f;
  //  Conteabel Teab;
    [Tooltip("移动速度")]
    public float Move = 1.5f;

    //Cube移动前的位置
    private Vector3 Position;

    void Start()
    {

    }
    void Update()
    {

    }

    public void OnManipulationCanceled(ManipulationEventData eventData)
    {
        //do nothing
    }

    public void OnManipulationCompleted(ManipulationEventData eventData)
    {
        //do nothing
    }

    public void OnManipulationStarted(ManipulationEventData eventData)
    {
        // 开始移动前，保存Cube原始位置
        Position = transform.position;
    }

    public void OnManipulationUpdated(ManipulationEventData eventData)
    {
        // 获取拖拽的总偏移量
        Vector3 newPos = new Vector3(eventData.CumulativeDelta.x, eventData.CumulativeDelta.y, eventData.CumulativeDelta.z);

        // 设置Cube新的位置
        transform.position = Position + newPos * Move;
    }
    
    public void OnNavigationCanceled(NavigationEventData eventData)
    {
        //do nothing
    }

    public void OnNavigationCompleted(NavigationEventData eventData)
    {
        //do nothing
    }

    public void OnNavigationStarted(NavigationEventData eventData)
    {
        //do nothing
    }

    public void OnNavigationUpdated(NavigationEventData eventData)
    {
        // 计算旋转值，其中：eventData的CumulativeDelta返回导航差值，值域[-1, 1]
        float rotationFactor = eventData.CumulativeDelta.x * Move;
        transform.Rotate(new Vector3(0, -1 * rotationFactor, 0));
    }
}