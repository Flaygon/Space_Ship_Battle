using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ScriptableObjectCreator : MonoBehaviour {

    [MenuItem("Assets/Create/Ship Info")]
    public static void Create_ShipInfo()
    {
        ShipInfo asset = ScriptableObject.CreateInstance<ShipInfo>();

        AssetDatabase.CreateAsset(asset, "Assets/Resources/ShipInfo/ShipInfo_.asset");
        AssetDatabase.SaveAssets();

        EditorUtility.FocusProjectWindow();

        Selection.activeObject = asset;
    }
}