using UnityEngine;
using UnityEngine.EventSystems;

namespace HoloToolkit.Unity.InputModule
{
    /// <summary>
    /// Describes an input event that involves content manipulation.描述了一个输入事件,手势涉及内容操作
    /// </summary>
    public class ManipulationEventData : InputEventData
    {
        /// <summary>发生的操作。通常的形式三角洲手的位置
        /// The amount of manipulation that has occurred. Usually in the form of delta position of a hand. 
        /// </summary>
        public Vector3 CumulativeDelta { get; private set; }

        public ManipulationEventData(EventSystem eventSystem) : base(eventSystem)
        {
        }
        //          . 初始化                    输入来源                                 累积的
        public void Initialize(IInputSource inputSource, uint sourceId, Vector3 cumulativeDelta)
        {
            BaseInitialize(inputSource, sourceId);
            CumulativeDelta = cumulativeDelta;
        }
    }
}