using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

namespace HoloToolkit.Unity.SpatialMapping
{
    [StructLayout(LayoutKind.Sequential)]
    public struct OrientedBoundingBox
    {
        public Vector3 Center;
        public Vector3 Extents;
        public Quaternion Rotation;
    };

    [StructLayout(LayoutKind.Sequential)]
    public struct BoundedPlane
    {
        public Plane Plane;
        public OrientedBoundingBox Bounds;
        public float Area;

        /// <summary>
        /// Builds the bounded plane to match the obb defined by xform构建有界的平面，以匹配xform定义的obb。
        /// </summary>
        public BoundedPlane(Transform xform)
        {
            Plane = new Plane(xform.forward, xform.position);
            Bounds = new OrientedBoundingBox()
            {
                Center = xform.position,
                Extents = xform.localScale / 2,
                Rotation = xform.rotation
            };
            Area = Bounds.Extents.x * Bounds.Extents.y;
        }
    };

    public class PlaneFinding
    {
        #region Public APIs

        /// <summary>
        /// PlaneFinding is an expensive task that should not be run from Unity's main thread as it will stall the thread and cause a frame rate dip.  Instead, the PlaneFinding APIs should be exclusively called from background threads.  Unfortunately, Unity's built-in data types (such as MeshFilter) are not thread safe and cannot be accessed from background threads. The MeshData struct exists to work-around this limitation.  When you want to find planes in a collection of MeshFilter objects, start by constructing a list of MeshData structs from those MeshFilters. You can then take the resulting list of MeshData structs, and safely pass it to the FindPlanes() API from a background thread.
        /// </summary>plane查找是一项昂贵的任务，它不应该从Unity的主线程中运行，因为它会使线程停止运行，导致帧率下降。相反，应该只从后台线程中调用plane查找api。不幸的是,统一的内置
        public struct MeshData
        {
            public Matrix4x4 Transform;
            public Vector3[] Verts;
            public Vector3[] Normals;
            public Int32[] Indices;

            public MeshData(MeshFilter meshFilter)
            {
                Transform = meshFilter.transform.localToWorldMatrix;
                Verts = meshFilter.sharedMesh.vertices;
                Normals = meshFilter.sharedMesh.normals;
                Indices = meshFilter.sharedMesh.triangles;
            }
        }

        /// <summary>
        /// Finds small planar patches that are contained within individual meshes.  The output of this API can then be passed to MergeSubPlanes() in order to find larger planar surfaces that potentially span across multiple meshes.
        /// </summary>发现在单个网格中包含的小平面补丁。然后可以将该API的输出传递给mergesub平面()，以便找到可能跨越多个网格的更大的平面表面
        /// <param name="meshes">
        /// List of meshes to run the plane finding algorithm on.
        /// </param>
        /// <param name="snapToGravityThreshold">
        /// Planes whose normal vectors are within this threshold (in degrees) from vertical/horizontal will be snapped to be perfectly gravity aligned.  When set to something other than zero, the bounding boxes for each plane will be gravity aligned as well, rather than rotated for an optimally tight fit. Pass 0.0 for this parameter to completely disable the gravity alignmentlogic.
        /// </param>正常的向量在垂直/水平(从垂直/水平)到这个阈值的平面将被抓拍到完全的重力对齐。当设置为大于零的东西时，每个平面的边界框将会是重力对齐的，而不是
        public static BoundedPlane[] FindSubPlanes(List<MeshData> meshes, float snapToGravityThreshold = 0.0f)
        {
            StartPlaneFinding();

            try
            {
                int planeCount;
                IntPtr planesPtr;
                IntPtr pinnedMeshData = PinMeshDataForMarshalling(meshes);
                DLLImports.FindSubPlanes(meshes.Count, pinnedMeshData, snapToGravityThreshold, out planeCount, out planesPtr);
                return MarshalBoundedPlanesFromIntPtr(planesPtr, planeCount);
            }
            finally
            {
                FinishPlaneFinding();
            }
        }

        /// <summary>
        /// Takes the subplanes returned by one or more previous calls to FindSubPlanes() and merges them together into larger planes that can potentially span across multiple meshes. Overlapping subplanes that have similar plane equations will be merged together to form larger planes.
        /// </summary>将由一个或多个先前的调用返回到findsub平面()并将它们合并到更大的平面上，这些层可能跨越多个网格。有相似平面方程的重叠子层将被合并到f
        /// <param name="subPlanes">
        /// The output from one or more previous calls to FindSubPlanes().一个或多个先前调用findsub平面()的输出。
        /// </param>
        /// <param name="snapToGravityThreshold">
        /// Planes whose normal vectors are within this threshold (in degrees) from vertical/horizontal will be snapped to be perfectly gravity aligned.  When set to something other than zero, the bounding boxes for each plane will be gravity aligned as well, rather than rotated for an optimally tight fit. Pass 0.0 for this parameter to completely disable the gravity alignment logic.
        /// </param>正常的向量在垂直/水平(从垂直/水平)到这个阈值的平面将被抓拍到完全的重力对齐。当设置为0时，每个平面的边界框将会是重力对齐的，而不是t。
        /// <param name="minArea">当将子平面合并在一起的时候，任何候选合并的平面三角形的总面积小于这个阈值的平面都被忽略了。
        /// While merging subplanes together, any candidate merged plane whose constituent mesh triangles have a total area less than this threshold are ignored.
        /// </param>
        public static BoundedPlane[] MergeSubPlanes(BoundedPlane[] subPlanes, float snapToGravityThreshold = 0.0f, float minArea = 0.0f)
        {
            StartPlaneFinding();

            try
            {
                int planeCount;
                IntPtr planesPtr;
                DLLImports.MergeSubPlanes(subPlanes.Length, PinObject(subPlanes), minArea, snapToGravityThreshold, out planeCount, out planesPtr);
                return MarshalBoundedPlanesFromIntPtr(planesPtr, planeCount);
            }
            finally
            {
                FinishPlaneFinding();
            }
        }

        /// <summary>执行 FindSubPlanes的方便包装器，然后通过单个调用进入本机代码(通过避免大量不必要的数据编组和管理到本机的转换，从而提高了性能)。
        /// Convenience wrapper that executes FindSubPlanes followed by MergeSubPlanes via a single call into native code (which improves performance by avoiding a bunch of unnecessary data marshalling and a managed-to-native transition).
        /// </summary>
        /// <param name="meshes">
        /// List of meshes to run the plane finding algorithm on.
        /// </param>
        /// <param name="snapToGravityThreshold">
        /// Planes whose normal vectors are within this threshold (in degrees) from vertical/horizontal will be snapped to be perfectly gravity aligned.  When set to something other than zero, the bounding boxes for each plane will be gravity aligned as well, rather than rotated for an optimally tight fit. Pass 0.0 for this parameter to completely disable the gravity alignment logic.
        /// </param>平面的法向量在这个阈值(在华)垂直/水平将拍摄重力是完全一致的。当设置为非零,每个平面的边界框将重力对齐
        /// <param name="minArea">
        /// While merging subplanes together, any candidate merged plane whose constituent mesh triangles have a total area less than this threshold are ignored.
        /// </param>当将子平面合并在一起的时候，任何候选合并的平面三角形的总面积小于这个阈值的平面都被忽略了。
        public static BoundedPlane[] FindPlanes(List<MeshData> meshes, float snapToGravityThreshold = 0.0f, float minArea = 0.0f)
        {
            StartPlaneFinding();

            try
            {
                int planeCount;
                IntPtr planesPtr;
                IntPtr pinnedMeshData = PinMeshDataForMarshalling(meshes);
                DLLImports.FindPlanes(meshes.Count, pinnedMeshData, minArea, snapToGravityThreshold, out planeCount, out planesPtr);
                return MarshalBoundedPlanesFromIntPtr(planesPtr, planeCount);
            }
            finally
            {
                FinishPlaneFinding();
            }
        }

        #endregion

        #region Internal

        private static bool findPlanesRunning = false;
        private static System.Object findPlanesLock = new System.Object();
        private static DLLImports.ImportedMeshData[] reusedImportedMeshesForMarshalling;
        private static List<GCHandle> reusedPinnedMemoryHandles = new List<GCHandle>();

        /// <summary>
        /// Validate that no other PlaneFinding API call is currently in progress. As a performance optimization to avoid unnecessarily thrashing the garbage collector, each call into the PlaneFinding DLL reuses a couple of static data structures. As a result, we can't handle multiple concurrent calls into these APIs.
        /// </summary>确认没有其他PlaneFinding API调用目前正在进步。作为一个性能优化,以避免不必要的垃圾收集器,每个调用PlaneFinding DLL重用几个静态数据结构。作为一个物
        private static void StartPlaneFinding()
        {
            lock (findPlanesLock)
            {
                if (findPlanesRunning)
                {
                    throw new Exception("PlaneFinding is already running. You can not call these APIs from multiple threads.");
                }
                findPlanesRunning = true;
            }
        }

        /// <summary>
        /// Cleanup after finishing a PlaneFinding API call by unpinning any memory that was pinned for the call into the driver, and then reset the findPlanesRunning bool.
        /// </summary> ///清理完成后拔掉PlaneFinding API调用的任何内存是固定电话的司机,然后重置findPlanesRunning bool。
        /// </summary>
        private static void FinishPlaneFinding()
        {
            UnpinAllObjects();
            findPlanesRunning = false;
        }

        /// <summary>针指定的对象,以便支持内存不能被重新安置,添加了跟踪固定内存句柄列表,然后返回的地址固定内存,以便它可以被传递到DLL直接从本机代码访问。
        /// Pins the specified object so that the backing memory can not be relocated, adds the pinned memory handle to the tracking list, and then returns that address of the pinned memory so that it can be passed into the DLL to be access directly from native code.
        /// </summary>
        private static IntPtr PinObject(System.Object obj)
        {
            GCHandle h = GCHandle.Alloc(obj, GCHandleType.Pinned);
            reusedPinnedMemoryHandles.Add(h);
            return h.AddrOfPinnedObject();
        }

        /// <summary>
        /// Unpins all of the memory previously pinned by calls to PinObject().
        /// </summary>
        private static void UnpinAllObjects()
        {
            for (int i = 0; i < reusedPinnedMemoryHandles.Count; ++i)
            {
                reusedPinnedMemoryHandles[i].Free();
            }
            reusedPinnedMemoryHandles.Clear();
        }

        /// <summary>
        /// Copies the supplied mesh data into the reusedMeshesForMarhsalling array. All managed arrays
        /// are pinned so that the marshalling only needs to pass a pointer and the native code can
        /// reference the memory in place without needing the marshaller to create a complete copy of
        /// the data.
        /// </summary>
        private static IntPtr PinMeshDataForMarshalling(List<MeshData> meshes)
        {
            // if we have a big enough array reuse it, otherwise create new
            if (reusedImportedMeshesForMarshalling == null || reusedImportedMeshesForMarshalling.Length < meshes.Count)
            {
                reusedImportedMeshesForMarshalling = new DLLImports.ImportedMeshData[meshes.Count];
            }

            for (int i = 0; i < meshes.Count; ++i)
            {
                reusedImportedMeshesForMarshalling[i] = new DLLImports.ImportedMeshData
                {
                    transform = meshes[i].Transform,
                    vertCount = meshes[i].Verts.Length,
                    indexCount = meshes[i].Indices.Length,
                    verts = PinObject(meshes[i].Verts),
                    normals = PinObject(meshes[i].Normals),
                    indices = PinObject(meshes[i].Indices),
                };
            }

            return PinObject(reusedImportedMeshesForMarshalling);
        }

        /// <summary>
        /// Marshals BoundedPlane data returned from a DLL API call into a managed BoundedPlane array
        /// and then frees the memory that was allocated within the DLL.
        /// </summary>
        /// <remarks>Disabling warning 618 when calling Marshal.SizeOf(), because
        /// Unity does not support .Net 4.5.1+ for using the preferred Marshal.SizeOf(T) method."/>, </remarks>
        private static BoundedPlane[] MarshalBoundedPlanesFromIntPtr(IntPtr outArray, int size)
        {
            BoundedPlane[] resArray = new BoundedPlane[size];
#pragma warning disable 618
            int structsize = Marshal.SizeOf(typeof(BoundedPlane));
#pragma warning restore 618
            IntPtr current = outArray;
            for (int i = 0; i < size; i++)
            {
#pragma warning disable 618
                resArray[i] = (BoundedPlane)Marshal.PtrToStructure(current, typeof(BoundedPlane));
#pragma warning restore 618
                current = (IntPtr)((long)current + structsize);
            }
            Marshal.FreeCoTaskMem(outArray);
            return resArray;
        }

        /// <summary>
        /// Raw PlaneFinding.dll imports
        /// </summary>
        private class DLLImports
        {
            [StructLayout(LayoutKind.Sequential)]
            public struct ImportedMeshData
            {
                public Matrix4x4 transform;
                public Int32 vertCount;
                public Int32 indexCount;
                public IntPtr verts;
                public IntPtr normals;
                public IntPtr indices;
            };

            [DllImport("PlaneFinding")]
            public static extern void FindPlanes(
                [In] int meshCount,
                [In] IntPtr meshes,
                [In] float minArea,
                [In] float snapToGravityThreshold,
                [Out] out int planeCount,
                [Out] out IntPtr planesPtr);

            [DllImport("PlaneFinding")]
            public static extern void FindSubPlanes(
                [In] int meshCount,
                [In] IntPtr meshes,
                [In] float snapToGravityThreshold,
                [Out] out int planeCount,
                [Out] out IntPtr planesPtr);

            [DllImport("PlaneFinding")]
            public static extern void MergeSubPlanes(
                [In] int subPlaneCount,
                [In] IntPtr subPlanes,
                [In] float minArea,
                [In] float snapToGravityThreshold,
                [Out] out int planeCount,
                [Out] out IntPtr planesPtr);
        }

        #endregion
    }
}