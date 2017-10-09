﻿using UnityEngine;
using System.Collections;
using UnityEditor;

enum CubeMapFaces
{
    right = 0,
    left,
    top,
    bottom,
    front,
    back
}

public class GetTexture : EditorWindow {

    [MenuItem("Window/CubeMapTool %m")]
    static void AddWindow()
    {
        //创建窗口
        Rect wr = new Rect(0, 0, 200,200);
        GetTexture window = (GetTexture)EditorWindow.GetWindowWithRect(typeof(GetTexture), wr, false, "CubeMapTool");
        window.Show();

    }

    private Camera renderCamera;
    private Cubemap renderCubeMap;
    CubeMapFaces faces;
    private CubemapFace offcialFaces;

    void OnGUI()
    {
        GUILayout.BeginVertical();
        GUILayout.Space(10f);
        renderCamera = EditorGUILayout.ObjectField("渲染相机", renderCamera, typeof(Camera), true) as Camera;
        GUILayout.Space(10f);
        renderCubeMap = EditorGUILayout.ObjectField("CubeMap", renderCubeMap, typeof(Cubemap), false) as Cubemap;


        GUILayout.Space(10f);
        if (GUILayout.Button("渲染") && CheckCamAndCubeMap())
        {

            if (renderCamera.RenderToCubemap(renderCubeMap))
            {



                Debug.LogWarningFormat("渲染完成!");
                CheckSingleCubeMapFaces();

            }
            else
            {
                Debug.LogWarningFormat("渲染失败!");
            }
        }

        GUILayout.Space(10f);

        GUILayout.BeginHorizontal();
        faces = (CubeMapFaces)EditorGUILayout.EnumPopup("保存CubeMap某个面", faces);



        if (GUILayout.Button("保存") && CheckCamAndCubeMap())
        {


            CheckSingleCubeMapFaces();
            SaveSingelTexture();

        }

        GUILayout.EndHorizontal();
        GUILayout.Space(10f);
        if (GUILayout.Button("全部保存"))
        {


            SaveMultiTexture();
        }

        GUILayout.Space(10f);

        EditorGUILayout.HelpBox("请务必确认要渲染的CubeMap可以读写!\n (属性面板中的 Readable 勾上即可)", MessageType.Warning);


        GUILayout.EndVertical();

    }

    bool CheckCamAndCubeMap()
    {
        if (renderCamera == null || renderCubeMap == null)
        {
            Debug.LogError("渲染相机和CubeMap不能为空!");
            return false;
        }
        else
        {
            return true;
        }
    }

    #region 检测单张保存时对应cubemap面
    /// <summary>
    /// Checks the cube map faces.
    /// </summary>
    void CheckSingleCubeMapFaces()
    {
        switch (faces)
        {
            case CubeMapFaces.right:
                offcialFaces = CubemapFace.PositiveX;
                break;
            case CubeMapFaces.left:
                offcialFaces = CubemapFace.NegativeX;
                break;
            case CubeMapFaces.top:
                offcialFaces = CubemapFace.PositiveY;
                break;
            case CubeMapFaces.bottom:
                offcialFaces = CubemapFace.NegativeY;
                break;
            case CubeMapFaces.front:
                offcialFaces = CubemapFace.PositiveZ;
                break;
            case CubeMapFaces.back:
                offcialFaces = CubemapFace.NegativeZ;
                break;

        }
    }
    #endregion

    #region 检测多张保存时对应的cubemap面
    /// <summary>
    /// Checks the multi cube map faces.
    /// </summary>
    void CheckMultiCubeMapFaces(int index)
    {
        switch (index)
        {
            case (int)CubeMapFaces.right:
                offcialFaces = CubemapFace.PositiveX;
                break;
            case (int)CubeMapFaces.left:
                offcialFaces = CubemapFace.NegativeX;
                break;
            case (int)CubeMapFaces.top:
                offcialFaces = CubemapFace.PositiveY;
                break;
            case (int)CubeMapFaces.bottom:
                offcialFaces = CubemapFace.NegativeY;
                break;
            case (int)CubeMapFaces.front:
                offcialFaces = CubemapFace.PositiveZ;
                break;
            case (int)CubeMapFaces.back:
                offcialFaces = CubemapFace.NegativeZ;
                break;

        }
    }

    #endregion

    #region 由cubemap 朝向反推前后左右方向
    string CheckDir()
    {
        string name = "TestTexture";
        switch (offcialFaces)
        {
            case CubemapFace.PositiveX:
                name = "left";
                break;
            case CubemapFace.NegativeX:
                name = "right";
                break;
            case CubemapFace.PositiveY:
                name = "top";
                break;
            case CubemapFace.NegativeY:
                name = "bottom";
                break;
            case CubemapFace.PositiveZ:
                name = "front";
                break;
            case CubemapFace.NegativeZ:
                name = "back";
                break;

        }
        return name;
    }

    #endregion

    #region 保存单张图片
    /// <summary>
    ///保存单张图片
    /// </summary>
    private void SaveSingelTexture()
    {

        Texture2D screenShot = new Texture2D(renderCubeMap.width,
                                             renderCubeMap.height,
                                             TextureFormat.ARGB32,
                                             false);
        for (int i = 0; i < renderCubeMap.width; i++)
        {
            for (int j = 0; j < renderCubeMap.height; j++)
            {
                screenShot.SetPixel(i, j, renderCubeMap.GetPixel(offcialFaces, renderCubeMap.width - i, renderCubeMap.height - j)); 
            }
        }

        screenShot.Apply();
        byte[] bytes = screenShot.EncodeToPNG();

        string path = EditorUtility.SaveFilePanel("图片保存", Application.dataPath, faces.ToString(), "png");

        System.IO.File.WriteAllBytes(path, bytes);

        AssetDatabase.Refresh();
    }
    #endregion

    #region 保存多张图片
    /// <summary>
    ///保存单张图片
    /// </summary>
    private void SaveMultiTexture()
    {

        Texture2D screenShot;
        string path = Application.dataPath + "/" + "CubeMapTextures/";
        for (int k = 0; k < 6; k++)
        {

            screenShot = new Texture2D(renderCubeMap.width,
                                                  renderCubeMap.height,
                                                  TextureFormat.ARGB32,
                                                  false);
            CheckMultiCubeMapFaces(k);
            for (int i = 0; i < renderCubeMap.width; i++)
            {
                for (int j = 0; j < renderCubeMap.height; j++)
                {
                    screenShot.SetPixel(i, j, renderCubeMap.GetPixel(offcialFaces, renderCubeMap.width - i, renderCubeMap.height - j));
                }
            }

            screenShot.Apply();
            // byte[] bytes = screenShot.EncodeToPNG();
            byte[] bytes = screenShot.EncodeToJPG();
            if (!System.IO.Directory.Exists(path))
            {
                System.IO.Directory.CreateDirectory(path);
            }
            System.IO.File.WriteAllBytes(path + CheckDir() + ".jpg", bytes);

        }

        AssetDatabase.Refresh();

        Debug.LogWarning("六张图片已经保存在 " + path + " 下!");

    }
    #endregion

    void OnInspectorUpdate()
    {
        this.Repaint();
    }

    void OnFocus()
    {
        //Debug.Log("当窗口获得焦点时调用一次");
    }

    void OnLostFocus()
    {
        //Debug.Log("当窗口丢失焦点时调用一次");
        Caching.CleanCache();
    }
}

