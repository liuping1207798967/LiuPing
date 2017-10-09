//
// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License. See LICENSE in the project root for license information.
//
using HUX.Buttons;
using UnityEngine;

namespace HUX.Interaction
{
    /// <summary>
    /// This script assists in using a bounding box to target objects Bounding boxes and manipulation toolbars can both be used without this script But this makes it easier to use a single bounding box to target multiple objects as well as to specify per-target display options and operations
    /// </summary>这个脚本帮助使用一个边界框来定位对象的边界框，并且操作工具栏可以在没有这个脚本的情况下使用，但是这使得使用一个单一的边界框来定位多个对象以及指定每个目标的di变得更加容易。
    [RequireComponent(typeof(CompoundButton))]
    public class BoundingBoxTarget : MonoBehaviour
    {

        /// <summary>标记时使用选中/去掉的时候这应该设置为FocusManager将忽略否则对撞机从这个对象可能封闭边界框
        /// Tags to use when selected / deselected This should be set to something the FocusManager will ignore Otherwise the colliders from this object may occlude bounding box
        /// </summary>
        public FilterTag TagOnSelected;
        public FilterTag TagOnDeselected;

        /// <summary>
        /// Which operations will be permitted when the bounding box targets this object哪些操作时将允许边界框的目标物体
        /// </summary>
        [HideInInspector]
        public BoundingBoxManipulate.OperationEnum PermittedOperations = BoundingBoxManipulate.OperationEnum.Drag | BoundingBoxManipulate.OperationEnum.ScaleUniform | BoundingBoxManipulate.OperationEnum.RotateY;

        /// <summary>
        /// Whether to show the manipulation display when the bounding box targets this object是否显示操作显示在这个边界框的目标对象
        /// </summary>
        [HideInInspector]
        public bool ShowAppBar = true;

        /// <summary>
        /// Bounding box to use. If this is not set, the first bounding box in the scene will be used.边界框使用。如果没有设置,第一个场景边界框将被使用。
        /// </summary>
        private BoundingBoxManipulate boundingBox;

        /// <summary>
        /// Manipulation toolbar to use. If this is not set, the first toolbar in the scene will be uesd.操作工具栏使用。如果没有设置,第一场景中工具栏将适用。
        /// </summary>
        private AppBar toolbar;

        private void Start()
        {
            Button button = GetComponent<Button>();
            button.FilterTag = TagOnDeselected;
        }

        public void OnTargetSelected()
        {
            //Debug.Log("Selecting target" + name);选中目标的名字
            GetComponent<Button>().FilterTag = TagOnSelected;
        }

        public void OnTargetDeselected()
        {
            //Debug.Log("Deselecting target " + name);没有选中目标的名字
            GetComponent<Button>().FilterTag = TagOnDeselected;
        }

        public void Tapped()
        {

            // Return if there isn't a Manipulation Manager 如果没有操作经理
            if (ManipulationManager.Instance == null)
            {
                Debug.LogError("No manipulation manager for " + name);
                return;
            }

            // Try to find our bounding box 尝试去找框框
            if (boundingBox == null)
            {
                boundingBox = ManipulationManager.Instance.ActiveBoundingBox;
            }

            // Try to find our toolbar尝试去找框框上的 工具栏
            if (toolbar == null)
            {
                toolbar = ManipulationManager.Instance.ActiveAppBar;
            }

            // If we've already got a bounding box and it's pointing to us, do nothing如果我们已经有了一个边界框指向我们,什么也不做
            if (boundingBox != null && boundingBox.Target == this.gameObject)
                return;

            // Set the bounding box's target and permitted operations设置边界框的目标和允许操作
            boundingBox.PermittedOperations = PermittedOperations;
            boundingBox.Target = gameObject;

            if (ShowAppBar)
            {
                // Show it and set its bounding box object显示它并设置其边界框对象
                toolbar.BoundingBox = boundingBox;
                //gameObject.AddComponent<Collider>();
                //Rigidbody ri = gameObject.GetComponent<Rigidbody>();
                //ri.useGravity = false;
                toolbar.Reset();
            }
            else if (toolbar != null)
            {
                // Set its bounding box to null to hide it设置隐藏它的边界框为空
                toolbar.BoundingBox = null;
                // Set to accept input immediately
                boundingBox.AcceptInput = true;
            }
        }
      
        private void OnDestroy()
        { 
            if (boundingBox != null && boundingBox.Target == this)
                boundingBox.Target = null;
        }
    }
}
