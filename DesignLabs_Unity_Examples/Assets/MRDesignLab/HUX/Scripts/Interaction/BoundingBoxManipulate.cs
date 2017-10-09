//
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
//
using HUX.Buttons;
using HUX.Focus;
using HUX.Receivers;
using System.Collections.Generic;
using UnityEngine;

namespace HUX.Interaction
{
    /// <summary>
    /// Listens to messages from attached BoundingBoxHandle objects and manipulates the target
    /// </summary>
    public class BoundingBoxManipulate : BoundingBox
    {
        [System.Flags]
        public enum OperationEnum
        {
            None = 0,
            Drag = 1,
            ScaleUniform = 2,
            RotateX = 4,
            RotateZ = 8,
            RotateY = 16,
            ScaleX = 32,
            ScaleY = 64,
            ScaleZ = 128,
        }

        #region public

        /// <summary>
        /// Makes bounding box manipulatable by user使边界框运算由用户
        /// </summary>
        public bool AcceptInput
        {
            get
            {
                return acceptInput;
            }
            set
            {
                if (value & !acceptInput)
                {
                    // Reset to drag operation重置为拖动操作
                    CurrentOperation = OperationEnum.Drag;
                }
                acceptInput = value;
            }
        }

        /// <summary>
        /// Which operations this bounding box is allowed to perform 操作这个边界框是可以执行吗
        /// </summary>
        public OperationEnum PermittedOperations
        {
            get
            {
                return permittedOperations;
            }
            set
            {
                if (permittedOperations != value)
                {
                    permittedOperations = value;
                    RefreshActiveHandles();
                }
            }
        }

        /// <summary>
        /// How quickly objects move when being dragged当被拖动物体移动速度
        /// </summary>
        public float DragMultiplier = 10f;

        /// <summary>
        /// How much to scale rotation input旋转输入规模是多少
        /// </summary>
        public float RotateMultiplier = 10f;

        /// <summary>
        /// How much to scale scale input物体缩放比例
        /// </summary>
        public float ScaleMultiplier = 10f;

        /// <summary>
        /// The smallest an object can be scaled with one gesture最小的一个对象可以按比例缩小的一个姿态
        /// </summary>
        public float MinScalePercentage = 0.05f;

        /// <summary>
        /// The current operation of the bounding box 当前操作的边界框
        /// </summary>
        public OperationEnum CurrentOperation
        {
            get
            {
                if (target == null || activeHandle == null)
                {
                    return OperationEnum.None;
                }

                return GetBoundingBoxOperationFromHandleType(activeHandle.HandleType);
            }
            set
            {
                SetHandleByOperation(value);
            }
        }

        /// <summary>
        /// Whether the user is manipulating the bb using a handle用户是否使用handle柄操纵bb
        /// </summary>
        public bool ManipulatingNow
        {
            get
            {
                if (Application.isPlaying)
                {
                    return manipulatingNow;
                }
                else
                {
                    return true;
                }
            }
            set
            {
                if (value)
                {
                    StartManipulating();
                }
                else
                {
                    StopManipulating();
                }
            }
        }

        /// <summary>
        /// The handle being manipulated by the user, if any处理用户被操纵的,如果有的话
        /// </summary>
        public BoundingBoxHandle ActiveHandle
        {
            get
            {
                return activeHandle;
            }
            set
            {
                activeHandle = value;
            }
        }

        /// <summary>
        /// The target object being manipulated目标对象被操纵
        /// </summary>
        public override GameObject Target
        {
            get
            {
                return target;
            }
            set
            {
                if (target != value)
                {
                    // Send a message to the new / old targets
                    if (value != null)
                    {
                        value.SendMessage("OnTargetSelected", SendMessageOptions.DontRequireReceiver);
                    }
                    if (target != null)
                    {
                        target.SendMessage("OnTargetDeselected", SendMessageOptions.DontRequireReceiver);
                    }
                    target = value;
                    // Reset active handle to drag
                    SetHandleByOperation(OperationEnum.Drag);
                    ManipulatingNow = false;
                }
                if (target != null)
                {
                    CreateTransforms();
                    // Set our transforms to the target immediately我们立即转换到目标
                    targetStandIn.position = target.transform.position;
                    targetStandIn.rotation = target.transform.rotation;
                    targetStandIn.localScale = target.transform.lossyScale;
                    RefreshTargetBounds();
                }
                else
                {
                    ActiveHandle = null;
                }
            }
        }

        /// <summary>
        /// Convenience function to help determine what function a handle serves便利功能,以帮助确定什么函数处理服务
        /// </summary>
        /// <param name="handleType"></param>
        /// <returns></returns>
        public static OperationEnum GetBoundingBoxOperationFromHandleType(BoundingBoxHandle.HandleTypeEnum handleType)
        {
            switch (handleType)
            {
                case BoundingBoxHandle.HandleTypeEnum.Drag:
                    return OperationEnum.Drag;

                //TODO - break this up into axis scalesTODO -把这个分成轴尺度
                case BoundingBoxHandle.HandleTypeEnum.Scale_LBF:
                case BoundingBoxHandle.HandleTypeEnum.Scale_LBB:
                case BoundingBoxHandle.HandleTypeEnum.Scale_LTF:
                case BoundingBoxHandle.HandleTypeEnum.Scale_LTB:
                case BoundingBoxHandle.HandleTypeEnum.Scale_RBF:
                case BoundingBoxHandle.HandleTypeEnum.Scale_RBB:
                case BoundingBoxHandle.HandleTypeEnum.Scale_RTF:
                case BoundingBoxHandle.HandleTypeEnum.Scale_RTB:
                    return OperationEnum.ScaleUniform;

                case BoundingBoxHandle.HandleTypeEnum.Rotate_LTF_RTF:
                case BoundingBoxHandle.HandleTypeEnum.Rotate_LBF_RBF:
                case BoundingBoxHandle.HandleTypeEnum.Rotate_RTB_LTB:
                case BoundingBoxHandle.HandleTypeEnum.Rotate_RBB_LBB:
                    return OperationEnum.RotateX;

                case BoundingBoxHandle.HandleTypeEnum.Rotate_LTF_LBF:
                case BoundingBoxHandle.HandleTypeEnum.Rotate_RTB_RBB:
                case BoundingBoxHandle.HandleTypeEnum.Rotate_LTB_LBB:
                case BoundingBoxHandle.HandleTypeEnum.Rotate_RTF_RBF:
                    return OperationEnum.RotateY;

                case BoundingBoxHandle.HandleTypeEnum.Rotate_RBF_RBB:
                case BoundingBoxHandle.HandleTypeEnum.Rotate_RTF_RTB:
                case BoundingBoxHandle.HandleTypeEnum.Rotate_LBF_LBB:
                case BoundingBoxHandle.HandleTypeEnum.Rotate_LTF_LTB:
                    return OperationEnum.RotateZ;

                default:
                    return OperationEnum.None;
            }
        }

        public override void OnEnable()
        {
            base.OnEnable();
            manipulatingNow = false;
        }

        #endregion

        #region manipulation events
        protected override void OnFocusEnter(GameObject obj, FocusArgs args)
        {
            if (!ManipulatingNow)
            {
                //TODO show handle meshTODO显示处理网格
            }
            base.OnFocusEnter(obj, args);
        }

        protected override void OnFocusExit(GameObject obj, FocusArgs args)
        {
            if (!ManipulatingNow)
            {
                //TODO hide handle mesh做隐藏处理网
            }
            base.OnFocusExit(obj, args);
        }

        /// <summary>选择我们的操作处理,所以我们知道如何解释未来的输入事件
        /// Chooses our manipulation handle so we know how to interpret future input events
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="eventArgs"></param>
        protected override void OnManipulationStarted(GameObject obj, InteractionManager.InteractionEventArgs eventArgs)
        {
            base.OnManipulationStarted(obj, eventArgs);
            TryToSetHandle(obj, eventArgs.Position, eventArgs.Focuser);
        }

        protected override void OnManipulationCanceled(GameObject obj, InteractionManager.InteractionEventArgs eventArgs)
        {
            ManipulatingNow = false;
            base.OnManipulationCanceled(obj, eventArgs);
        }

        protected override void OnManipulationCompleted(GameObject obj, InteractionManager.InteractionEventArgs eventArgs)
        {
            ManipulatingNow = false;
            base.OnManipulationCompleted(obj, eventArgs);
        }

        protected override void OnManipulationUpdated(GameObject obj, InteractionManager.InteractionEventArgs eventArgs)
        {
            base.OnManipulationUpdated(obj, eventArgs);

            if (!acceptInput)
                return;

            if (target == null)
                return;

            if (!manipulatingNow)
                return;


            Vector3 eventPos = eventArgs.Position;
            // Transform the direction if necessary 必要时变换方向
            if (eventArgs.IsPosRelative)
            {
                eventPos = Veil.Instance.HeadTransform.TransformDirection(eventPos);
            }
            // See how much our position has changed 看到我们的立场改变了多少
            navigateVelocity = lastNavigatePos - eventPos;
            lastNavigatePos = eventPos;
            smoothVelocity = Vector3.Lerp(smoothVelocity, navigateVelocity, 0.5f);
        }

        #endregion

        #region private

        /// <summary>
        /// Override so we're not overwhelmed by button gizmos 覆盖我们不被按钮小玩意
        /// </summary>
#if UNITY_EDITOR
        protected override void OnDrawGizmos()
        {
            // nothing
            if (!Application.isPlaying)
            {
                // Do this here to ensure continuous updates in editor这样做来确保持续更新编辑器
                UpdateTargetManipulation();
                RefreshTargetBounds();
            }
        }
#endif

        protected override void Update()
        {
            if (!Application.isPlaying)
                return;

            // Check to see if our hands have exited the screen If they have, stop manipulating 看看我们的手已经退出屏幕是否有,停止操作
            if (!Veil.Instance.HandVisible)
            {
                ManipulatingNow = false;
            }

            UpdateUserManipulation();
            UpdateTargetManipulation();

            base.Update();
        }

        /// <summary>
        /// Applies changes gathered during manipulation udpate events适用于聚集在操纵udpate事件变化
        /// </summary>
        private void UpdateUserManipulation()
        {
            if (ManipulatingNow)
            {//改变transform辅助基于当前操作我们使用一些神奇的数字在这里继续乘数直观的范围
                // Change the transform helper based on the current operation We're using some magic numbers in here to keep the multiplier ranges intuitive
                switch (CurrentOperation)
                {//

                    case OperationEnum.Drag://操作 按下
                        //transformHelper.position -= (smoothVelocity * DragMultiplier);
                        Vector3 temp = (smoothVelocity * DragMultiplier);//目标点的位置 DragMultiplier当被拖动物体移动速度
                        Vector3 target = ManipulationManager.Instance.ActiveBoundingBox.transform.position;//射线的起始点
                        Vector3 targetFrom = Vector3.zero;//记录方向
                        Vector3 targetRayTo = target;//射线的目标点 首先要画射线。当检测到transformHelper的position有更改的时候 在相应的方向射射线。
                        //这里要完成一个决定画射线方向的目标点
                        targetRayTo = target + targetFrom;//这里要画线， 决定碰撞点的Y轴 如果撞到，就不对该方向作操作  
                        RaycastHit hitInfor;
                        //            射线的起始点  射线的方向  射线的长度   选择投射的层蒙版
                        if (Physics.Raycast(target, targetRayTo, out hitInfor, 0.1f, 1 << 31))
                        {
                            Vector3 hitDirection = hitInfor.point - target;
                            float hitlength = hitDirection.magnitude;
                            if (hitlength > 0.0001)
                            {
                                targetRayTo = target + (hitDirection * 0.99f);
                            }
                        }
                        else
                        {
                            targetRayTo = target;
                            //hitInfor.collider.gameObject.AddComponent<Collider>();
                            //hitInfor.collider.gameObject.AddComponent<Rigidbody>().useGravity = false;
                        }
                            transformHelper.position -= temp;
                        break;

                    case OperationEnum.ScaleUniform:
                    case OperationEnum.ScaleX:
                    case OperationEnum.ScaleY:
                    case OperationEnum.ScaleZ:
                        // Translate velocity direction based on camera移动速度方向基于相机
                        Vector3 orientedVelocity = Camera.main.transform.TransformDirection(smoothVelocity);
                        // See whether handle is to left or right of gizmo center是否处理是左边或右边的小发明中心
                        Vector3 handleScreenPoint = Camera.main.WorldToScreenPoint(activeHandle.transform.position);
                        Vector3 gizmoScreenPoint = Camera.main.WorldToScreenPoint(targetBoundsWorldCenter);
                        float dragAmount = orientedVelocity.x;
                        if (handleScreenPoint.x > gizmoScreenPoint.x)
                        {
                            dragAmount = -dragAmount;
                        }
                        transformHelper.localScale += Vector3.one * (dragAmount * ScaleMultiplier);
                        break;

                    case OperationEnum.RotateX:
                        transformHelper.Rotate(-smoothVelocity.y * RotateMultiplier * 360, 0f, 0f, Space.World);
                        break;

                    case OperationEnum.RotateY:
                        transformHelper.Rotate(0f, smoothVelocity.x * RotateMultiplier * 360, 0f, Space.World);
                        break;

                    case OperationEnum.RotateZ:
                        transformHelper.Rotate(0f, 0f, smoothVelocity.x * RotateMultiplier * 360, Space.World);
                        break;

                    default:
                        break;
                }
            }
        }

        /// <summary>
        /// Alters the target based on changes applied to bounding box根据对边框的变化改变目标
        /// </summary>
        private void UpdateTargetManipulation()
        {
            // Goes without saying
            if (!acceptInput)
                return;

            // If we don't have a target, nothing to do here
            if (target == null)
                return;

            // If we're NOT actively manipulating the target, nothing to do here
            if (!ManipulatingNow)
                return;

            if (!Application.isPlaying)
                return;

            CreateTransforms();

            // Apply the target stand-in's transform info to the target
            Target.transform.position = targetStandIn.position;
            Target.transform.rotation = targetStandIn.rotation;
            Target.transform.localScale = targetStandIn.lossyScale;
        }

        /// <summary>
        /// Stores target under our transform helper and prepares for manipulation存储目标在我们的转换助手下，并准备进行操作
        /// </summary>
        private void StartManipulating()
        {
            // Goes without saying
            if (!acceptInput)
                return;

            if (target == null)
                return;

            if (manipulatingNow)
                return;

            manipulatingNow = true;

            if (!Application.isPlaying)
                return;

            CreateTransforms();

            // Reset the transform helper to 1,1,1 / idenity将 transform helpe重设为1,1,1/登
            transformHelper.localScale = Vector3.one;
            transformHelper.rotation = Quaternion.identity;
            adjustedScaleTarget = Vector3.one;
            smoothVelocity = Vector3.zero;//原点

            // Set up our transforms and gestures based on the operation we're performing根据我们正在执行的操作来设置我们的转换和手势
            OperationEnum operation = GetBoundingBoxOperationFromHandleType(ActiveHandle.HandleType);
            switch (operation)
            {
                case OperationEnum.ScaleUniform:
                case OperationEnum.ScaleX:
                case OperationEnum.ScaleY:
                case OperationEnum.ScaleZ:
                    // If we're scaling, move the transform helper to the position OPPOSITE the scale handle
                    // That way the object will grow in the right direction  如果我们在缩放，将转换助手移到磅柄对面的位置 这样，物体就会向正确的方向发展
                    BoundingBoxHandle oppositeHandle = null;
                    BoundingBoxHandle.HandleTypeEnum oppositeHandleType = BoundingBoxHandle.GetOpposingHandle(ActiveHandle.HandleType);
                    foreach (GameObject bbhGo in Interactibles)
                    {
                        BoundingBoxHandle bbh = bbhGo.GetComponent<BoundingBoxHandle>();
                        if (bbh != null && bbh.HandleType == oppositeHandleType)
                        {
                            oppositeHandle = bbh;
                            break;
                        }
                    }
                    if (oppositeHandle == null)
                    {//无法找到类型的相反句柄
                        Debug.LogWarning("Couldn't find opposing handle for type " + ActiveHandle.HandleType);
                        transformHelper.position = transform.position;
                        targetStandIn.position = target.transform.position;
                    }
                    else
                    {
                        transformHelper.position = oppositeHandle.transform.position;
                        targetStandIn.position = target.transform.position;
                    }
                    break;

                case OperationEnum.Drag:
                    // If we're rotating or moving, move the transform helper to the center of the gizmo如果我们旋转或移动,移动变换助手小发明的中心
                    transformHelper.position = transform.position;
                    targetStandIn.position = target.transform.position;
                    break;

                case OperationEnum.RotateX:
                case OperationEnum.RotateY:
                case OperationEnum.RotateZ:
                default:
                    // Rotation
                    // If we're rotating or moving, move the transform helper to the center of the gizmo如果我们旋转或移动,移动变换助手小发明的中心
                    transformHelper.position = transform.position;
                    targetStandIn.position = target.transform.position;
                    break;
            }

            scaleOnStartManipulation = targetStandIn.localScale;

            if (target != null)
            {
                // Set our transforms to the target immediately
                targetStandIn.position = target.transform.position;
                targetStandIn.rotation = target.transform.rotation;
                targetStandIn.localScale = target.transform.lossyScale;
            }
        }

        private void StopManipulating()
        {
            if (!manipulatingNow)
                return;

            manipulatingNow = false;
            if (focuser != null)
            {
                focuser.ReleaseFocus();
                focuser = null;
            }
        }

        private void SetHandleByOperation(OperationEnum operation)
        {
            switch (operation)
            {
                case OperationEnum.None:
                    ActiveHandle = null;
                    break;

                case OperationEnum.Drag:
                    foreach (GameObject obj in Interactibles)
                    {
                        BoundingBoxHandle h = obj.GetComponent<BoundingBoxHandle>();
                        if (h.HandleType == BoundingBoxHandle.HandleTypeEnum.Drag)
                        {
                            ActiveHandle = h;
                            break;
                        }
                    }
                    break;

                default:
                    //TODO link up other operations here
                    break;
            }

        }

        private void RefreshActiveHandles()
        {
            foreach (GameObject handleGo in Interactibles)
            {
                BoundingBoxHandle handle = handleGo.GetComponent<BoundingBoxHandle>();
                OperationEnum handleOperation = GetBoundingBoxOperationFromHandleType(handle.HandleType);
                handleGo.SetActive((handleOperation & permittedOperations) != 0);
            }
        }

        private void TryToSetHandle(GameObject obj, Vector3 position, AFocuser newFocuser)
        {
            if (!acceptInput)
            {
                Debug.Log("Not accepting input");
                return;
            }

            BoundingBoxHandle newHandle = obj.GetComponent<BoundingBoxHandle>();
            if (newHandle != null)
            {
                activeHandle = newHandle;
                lastNavigatePos = position;
                ManipulatingNow = true;
                focuser = newFocuser;
                focuser.LockFocus();
            }
        }

        [SerializeField]
        private BoundingBoxHandle activeHandle;

        [SerializeField]
        private bool acceptInput = true;

        [SerializeField]
        private bool manipulatingNow = false;

        [SerializeField]
        private OperationEnum permittedOperations = OperationEnum.Drag | OperationEnum.RotateY | OperationEnum.ScaleUniform;

        private Vector3 lastNavigatePos = Vector3.zero;
        private Vector3 navigateVelocity = Vector3.zero;

        private Vector3 smoothVelocity = Vector3.zero;
        private Vector3 adjustedScaleTarget = Vector3.one;
        private Vector3 targetPosition = Vector3.zero;

        private Vector3 scaleOnStartManipulation = Vector3.one;
        private AFocuser focuser = null;

        #endregion
    }
}