using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;

public class TextureRenderer : MonoBehaviour {

    public string textureName;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {


        if (Input.GetKeyDown(KeyCode.K))
        {
            ScreenShot();
        }
	}

    void ScreenShot()
    {
        RenderTexture rt = new RenderTexture(Screen.width, Screen.height, 0);
        Camera mainCam = Camera.main;
        mainCam.targetTexture = rt;
        mainCam.Render();
        RenderTexture.active = rt;
        Texture2D screenShot = new Texture2D(Screen.width, Screen.height, TextureFormat.RGB24, false);
        screenShot.ReadPixels(new Rect(0, 0, rt.width, rt.height), 0, 0, false);
        screenShot.Apply();
        mainCam.targetTexture = null;
        RenderTexture.active = null;
        byte[] bytes = screenShot.EncodeToPNG();
        string path = "/Texture/" + textureName+ ".png";
        FileStream file = File.Open("Assets" + path, FileMode.Create);
        BinaryWriter writer = new BinaryWriter(file);
        writer.Write(bytes);
        file.Close();
        string assetPath = "Assets" + path;
        AssetDatabase.ImportAsset(assetPath);
        TextureImporter texture = AssetImporter.GetAtPath(assetPath) as TextureImporter;
        texture.alphaIsTransparency = true;
        texture.isReadable = true;
        texture.mipmapEnabled = false;
        AssetDatabase.ImportAsset(assetPath);
    }
}
