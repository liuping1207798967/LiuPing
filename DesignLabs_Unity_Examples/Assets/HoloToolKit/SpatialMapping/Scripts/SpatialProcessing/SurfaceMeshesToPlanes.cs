using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if !UNITY_EDITOR && UNITY_METRO
using System.Threading;
using System.Threading.Tasks;
#endif

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace HoloToolkit.Unity.SpatialMapping
{
    /// <summary>
    /// SurfaceMeshesToPlanes will find and create planes based on the meshes returned by the SpatialMappingManager's Observer.“表面emeshestop车道”将根据空间应用管理器的观察者返回的网格来查找和创建飞机。
    /// </summary>
    public class SurfaceMeshesToPlanes : Singleton<SurfaceMeshesToPlanes>
    {
        [Tooltip("Currently active planes found within the Spatial Mapping Mesh.")]
        //当前活动的飞机发现在空间映射网格
        public List<GameObject> ActivePlanes;

        [Tooltip("Object used for creating and rendering Surface Planes.")]
       // 对象用于创建和渲染表面的飞机。
        public GameObject SurfacePlanePrefab;

        [Tooltip("Minimum area required for a plane to be created.")]//创建一个平面所需最小面积
        public float MinArea = 0.025f;

        /// <summary>
        /// Determines which plane types should be rendered.确定应该呈现哪种平面类型。
        /// </summary>
        [HideInInspector]
        public PlaneTypes drawPlanesMask =
            (PlaneTypes.Wall | PlaneTypes.Floor | PlaneTypes.Ceiling | PlaneTypes.Table);

        /// <summary>
        /// 确定应该丢弃哪些类型的平面类型。当空间映射网格更适合于表面时(例如:圆桌)时，使用它。
        ///Determines which plane types should be discarded. Use this when the spatial mapping mesh is a better fit for the surface (ex: round tables).
        /// </summary>
        [HideInInspector]
        public PlaneTypes destroyPlanesMask = PlaneTypes.Unknown;

        /// <summary>
        /// Floor y value, which corresponds to the maximum horizontal area found below the user's head position. This value is reset by SurfaceMeshesToPlanes when the max floor plane has been found.
        /// 地板y值，对应于用户头部位置下方的最大水平区域。当最大的地板被发现时，这个值会被表面的emeshestop车道重置。
        /// </summary>
        public float FloorYPosition { get; private set; }

        /// <summary>
        /// Ceiling y value, which corresponds to the maximum horizontal area found above the user's head position.This value is reset by SurfaceMeshesToPlanes when the max ceiling plane has been found.
        /// 上限y值，对应于用户头部位置上方的最大水平区域。当最大的天花板被发现时，这个值被表面的emeshestop车道重置
        /// </summary>
        public float CeilingYPosition { get; private set; }

        /// <summary>
        /// Delegate which is called when the MakePlanesCompleted event is triggered.当makeplanes竣工事件被触发时，该委托将被调用。
        /// </summary>
        /// <param name="source"></param>
        /// <param name="args"></param>
        public delegate void EventHandler(object source, EventArgs args);

        /// <summary>
        /// EventHandler which is triggered when the MakePlanesRoutine is finished.在makeplanes例行程序完成时触发的事件。
        /// </summary>
        public event EventHandler MakePlanesComplete;

        /// <summary>
        /// Empty game object used to contain all planes created by the SurfaceToPlanes class.空的游戏对象，用来包含由表面etop巷类创建的所有平面
        /// </summary>
        private GameObject planesParent;

        /// <summary>
        /// Used to align planes with gravity so that they appear more level.用于使飞机与重力保持一致，使它们看起来更有水平
        /// </summary>
        private float snapToGravityThreshold = 5.0f;

        /// <summary>
        /// Indicates if SurfaceToPlanes is currently creating planes based on the Spatial Mapping Mesh.指示表面etop车道是否正在基于空间映射网格创建平面。
        /// </summary>
        private bool makingPlanes = false;

#if UNITY_EDITOR || UNITY_STANDALONE
        /// <summary>
        /// How much time (in sec), while running in the Unity Editor, to allow RemoveSurfaceVertices to consume before returning control to the main program.
        /// </summary>在统一编辑器中运行的时间(在sec)中，在将控制权返回给主程序之前，允许删除操作。
        private static readonly float FrameTime = .016f;
#else
        /// <summary>
        /// How much time (in sec) to allow RemoveSurfaceVertices to consume before returning control to the main program.
        /// </summary>
        private static readonly float FrameTime = .008f;
#endif

        // GameObject initialization.
        private void Start()
        {
            makingPlanes = false;
            ActivePlanes = new List<GameObject>();
            planesParent = new GameObject("SurfacePlanes");
            planesParent.transform.position = Vector3.zero;
            planesParent.transform.rotation = Quaternion.identity;
        }

        /// <summary>
        /// Creates planes based on meshes gathered by the SpatialMappingManager's SurfaceObserver.根据空间应用经理的surface观测者收集的网格，创建了平面。
        /// </summary>
        public void MakePlanes()
        {
            if (!makingPlanes)
            {
                makingPlanes = true;
                // Processing the mesh can be expensive... We use Coroutine to split the work across multiple frames and avoid impacting the frame rate too much.
                //处理网格可以是昂贵的…我们使用协同程序跨多个帧分割工作,避免影响帧率太多。
                StartCoroutine(MakePlanesRoutine());
            }
        }

        /// <summary>
        /// Gets all active planes of the specified type(s).得到指定类型的所有激活的的planes(年代)。
        /// </summary>
        /// <param name="planeTypes">A flag which includes all plane type(s) that should be returned.</param>标志包括所有plane类型(s)应该返回。
        /// <returns>A collection of planes that match the expected type(s).</returns>平面的集合相匹配预期的类型(s)
        public List<GameObject> GetActivePlanes(PlaneTypes planeTypes)
        {
            List<GameObject> typePlanes = new List<GameObject>();

            foreach (GameObject plane in ActivePlanes)
            {
                SurfacePlane surfacePlane = plane.GetComponent<SurfacePlane>();

                if (surfacePlane != null)
                {
                    if ((planeTypes & surfacePlane.PlaneType) == surfacePlane.PlaneType)
                    {
                        typePlanes.Add(plane);
                        plane.AddComponent<Collider>();
                    }
                }
            }

            return typePlanes;
        }

        /// <summary>
        /// Iterator block, analyzes surface meshes to find planes and create new 3D cubes to represent each plane.迭代器块,分析表面网格找到飞机和创建新的3 d数据集来表示每架飞机。
        /// </summary>
        /// <returns>Yield result.</returns>
        private IEnumerator MakePlanesRoutine()
        {
            // Remove any previously existing planes, as they may no longer be valid.之前删除任何现有的平面,因为他们可能不再有效。
            for (int index = 0; index < ActivePlanes.Count; index++)
            {
                Destroy(ActivePlanes[index]);
            }

            // Pause our work, and continue on the next frame.暂停我们的工作,并继续下一个框架。
            yield return null;
            float start = Time.realtimeSinceStartup;

            ActivePlanes.Clear();

            // Get the latest Mesh data from the Spatial Mapping Manager.获得最新的网格空间映射的数据管理器。
            List<PlaneFinding.MeshData> meshData = new List<PlaneFinding.MeshData>();
            List<MeshFilter> filters = SpatialMappingManager.Instance.GetMeshFilters();

            for (int index = 0; index < filters.Count; index++)
            {
                MeshFilter filter = filters[index];
                if (filter != null && filter.sharedMesh != null)
                {
                    // fix surface mesh normals so we can get correct plane orientation.修复表面网格法线所以我们可以得到正确的平面方向。
                    filter.mesh.RecalculateNormals();
                    meshData.Add(new PlaneFinding.MeshData(filter));
                }

                if ((Time.realtimeSinceStartup - start) > FrameTime)
                {
                    // Pause our work, and continue to make more PlaneFinding objects on the next frame.暂停我们的工作，继续在下一个框架中找到更多的对象。
                    yield return null;
                    start = Time.realtimeSinceStartup;
                }
            }

            // Pause our work, and continue on the next frame.暂停我们的工作,并继续下一个框架。
            yield return null;

#if !UNITY_EDITOR && UNITY_METRO
            // When not in the unity editor we can use a cool background task to help manage FindPlanes().当不在unity编辑器中，我们可以使用一个很酷的后台任务来帮助管理find平面()。
            Task<BoundedPlane[]> planeTask = Task.Run(() => PlaneFinding.FindPlanes(meshData, snapToGravityThreshold, MinArea));
        
            while (planeTask.IsCompleted == false)
            {
                yield return null;
            }

            BoundedPlane[] planes = planeTask.Result;
#else         //在unity编辑器中，任务类是不可用的，但是perf通常是好的，所以我们只需要等待find平面的完成。
            // In the unity editor, the task class isn't available, but perf is usually good, so we'll just wait for FindPlanes to complete.
            BoundedPlane[] planes = PlaneFinding.FindPlanes(meshData, snapToGravityThreshold, MinArea);
#endif

            // Pause our work here, and continue on the next frame.暂停我们的工作，继续下一帧。
            yield return null;
            start = Time.realtimeSinceStartup;

            float maxFloorArea = 0.0f;
            float maxCeilingArea = 0.0f;
            FloorYPosition = 0.0f;
            CeilingYPosition = 0.0f;
            float upNormalThreshold = 0.9f;

            if (SurfacePlanePrefab != null && SurfacePlanePrefab.GetComponent<SurfacePlane>() != null)
            {
                upNormalThreshold = SurfacePlanePrefab.GetComponent<SurfacePlane>().UpNormalThreshold;
            }

            // Find the floor and ceiling.找到地板和天花板我们将地板划分为用户头部下方的最大水平面。我们将天花板划分为用户头部上方的最大水平表面。
            // We classify the floor as the maximum horizontal surface below the user's head. We classify the ceiling as the maximum horizontal surface above the user's head.
            for (int i = 0; i < planes.Length; i++)
            {
                BoundedPlane boundedPlane = planes[i];
                if (boundedPlane.Bounds.Center.y < 0 && boundedPlane.Plane.normal.y >= upNormalThreshold)
                {
                    maxFloorArea = Mathf.Max(maxFloorArea, boundedPlane.Area);
                    if (maxFloorArea == boundedPlane.Area)
                    {
                        FloorYPosition = boundedPlane.Bounds.Center.y;
                    }
                }
                else if (boundedPlane.Bounds.Center.y > 0 && boundedPlane.Plane.normal.y <= -(upNormalThreshold))
                {
                    maxCeilingArea = Mathf.Max(maxCeilingArea, boundedPlane.Area);
                    if (maxCeilingArea == boundedPlane.Area)
                    {
                        CeilingYPosition = boundedPlane.Bounds.Center.y;
                    }
                }
            }

            // Create SurfacePlane objects to represent each plane found in the Spatial Mapping mesh.创建SurfacePlane对象来表示每架平面在空间映射网格。
            for (int index = 0; index < planes.Length; index++)
            {
                GameObject destPlane;
                BoundedPlane boundedPlane = planes[index];

                // Instantiate a SurfacePlane object, which will have the same bounds as our BoundedPlane object.实例化一个SurfacePlane对象,它将具有相同的范围作为我们BoundedPlane对象。
                if (SurfacePlanePrefab != null && SurfacePlanePrefab.GetComponent<SurfacePlane>() != null)
                {
                    destPlane = Instantiate(SurfacePlanePrefab);
                }
                else
                {
                    destPlane = GameObject.CreatePrimitive(PrimitiveType.Cube);
                    destPlane.AddComponent<SurfacePlane>();
                    destPlane.GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.Off;
                }

                destPlane.transform.parent = planesParent.transform;
                SurfacePlane surfacePlane = destPlane.GetComponent<SurfacePlane>();
                //设置平面属性调整变换位置/规模/旋转并确定飞机类型。
                // Set the Plane property to adjust transform position/scale/rotation and determine plane type.
                surfacePlane.Plane = boundedPlane;

                SetPlaneVisibility(surfacePlane);

                if ((destroyPlanesMask & surfacePlane.PlaneType) == surfacePlane.PlaneType)
                {
                    DestroyImmediate(destPlane);
                }
                else
                {
                    // Set the plane to use the same layer as the SpatialMapping mesh.飞机使用相同的图层设置为SpatialMapping网。
                    destPlane.layer = SpatialMappingManager.Instance.PhysicsLayer;
                    ActivePlanes.Add(destPlane);
                    destPlane.AddComponent<Collider>();
                }
                //如果太多的时间已经过去了,我们需要返回到主游戏循环的控制。
                // If too much time has passed, we need to return control to the main game loop.
                if ((Time.realtimeSinceStartup - start) > FrameTime)
                {//我们暂停工作,继续制作额外的飞机下一帧。
                    // Pause our work here, and continue making additional planes on the next frame.
                    yield return null;
                    start = Time.realtimeSinceStartup;
                }
            }

            Debug.Log("Finished making planes.");
            //我们完成创造飞机,触发一个事件。
            // We are done creating planes, trigger an event.
            EventHandler handler = MakePlanesComplete;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }

            makingPlanes = false;
        }

        /// <summary>基于其类型设置可见性的飞机。
        /// Sets visibility of planes based on their type.
        /// </summary>
        /// <param name="surfacePlane"></param>
        private void SetPlaneVisibility(SurfacePlane surfacePlane)
        {
            surfacePlane.IsVisible = ((drawPlanesMask & surfacePlane.PlaneType) == surfacePlane.PlaneType);
        }
    }

#if UNITY_EDITOR
    /// <summary>
    /// Editor extension class to enable multi-selection of the 'Draw Planes' and 'Destroy Planes' options in the Inspector.
    /// </summary>编辑器扩展类,使多重选择的“画飞机”和“摧毁飞机”巡查员选项。
    [CustomEditor(typeof(SurfaceMeshesToPlanes))]
    public class PlaneTypesEnumEditor : Editor
    {
        public SerializedProperty drawPlanesMask;
        public SerializedProperty destroyPlanesMask;

        void OnEnable()
        {
            drawPlanesMask = serializedObject.FindProperty("drawPlanesMask");
            destroyPlanesMask = serializedObject.FindProperty("destroyPlanesMask");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            serializedObject.Update();

            drawPlanesMask.intValue = (int)((PlaneTypes)EditorGUILayout.EnumMaskField
                    ("Draw Planes", (PlaneTypes)drawPlanesMask.intValue));

            destroyPlanesMask.intValue = (int)((PlaneTypes)EditorGUILayout.EnumMaskField
                    ("Destroy Planes", (PlaneTypes)destroyPlanesMask.intValue));

            serializedObject.ApplyModifiedProperties();
        }
    }
#endif
}