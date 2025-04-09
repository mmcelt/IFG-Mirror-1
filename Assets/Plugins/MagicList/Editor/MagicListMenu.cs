using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class MagicListMenu : MonoBehaviour
{
    private enum MagicListType
    {
        Horizontal,
        Vertical,
        Grid
    }

    private static readonly Dictionary<MagicListType, string> AssetPrefabName = new Dictionary<MagicListType, string>()
    {
        {MagicListType.Horizontal, "HorizontalMagicList"},
        {MagicListType.Vertical, "VerticalMagicList"},
        {MagicListType.Grid, "GridMagicList"}
    };

    private static readonly Dictionary<MagicListType, string> PrefabName = new Dictionary<MagicListType, string>()
    {
        {MagicListType.Horizontal, "Horizontal List"},
        {MagicListType.Vertical, "Vertical List"},
        {MagicListType.Grid, "Grid List"}
    };

    [MenuItem("GameObject/UI/MagicList/Vertical", false, 10)]
    private static void CreateVMagicList(MenuCommand menuCommand)
    {
        CreateMagicList(MagicListType.Vertical);
    }

    [MenuItem("GameObject/UI/MagicList/Horizontal", false, 10)]
    private static void CreateHMagicList(MenuCommand menuCommand)
    {
        CreateMagicList(MagicListType.Horizontal);
    }

    [MenuItem("GameObject/UI/MagicList/Grid", false, 10)]
    private static void CreateGMagicList(MenuCommand menuCommand)
    {
        CreateMagicList(MagicListType.Grid);
    }

    private static void CreateMagicList(MagicListType listType)
    {
        var assets = AssetDatabase.FindAssets(AssetPrefabName[listType]);
        var assetPath = AssetDatabase.GUIDToAssetPath(assets[0]);
        var prefab =
            (GameObject) PrefabUtility.InstantiatePrefab(AssetDatabase.LoadAssetAtPath(assetPath, typeof(GameObject)));
        prefab.name = PrefabName[listType];
        PrefabUtility.UnpackPrefabInstance(prefab, PrefabUnpackMode.Completely, InteractionMode.AutomatedAction);
        var canvas = SelectBestParent();
        prefab.transform.SetParent(canvas.transform, false);
        Selection.activeTransform = prefab.transform;
    }

    private static Transform SelectBestParent()
    {
        var activeTransform = Selection.activeTransform;

        Canvas canvas;
        if (activeTransform == null)
        {
            canvas = (Canvas) FindObjectOfType(typeof(Canvas));
            if (canvas != null) return canvas.transform;
        }
        else
        {
            canvas = activeTransform.GetComponent<Canvas>();
            if (canvas != null) return activeTransform;
            canvas = activeTransform.GetComponentInParent<Canvas>();
            if (canvas != null) return activeTransform;
            canvas = activeTransform.GetComponentInChildren<Canvas>();
            if (canvas != null) return canvas.transform;
        }

        EditorApplication.ExecuteMenuItem("GameObject/UI/Canvas");
        return Selection.activeTransform;
    }

    public static GameObject GenerateListItem(GameObject content)
    {
        var listItem = new GameObject("List Item");

        var image = listItem.AddComponent<Image>();
        image.GetComponent<RectTransform>().sizeDelta =
            new Vector2(100, 50);

        var listItemText = new GameObject("Text");

        var text = listItemText.AddComponent<Text>();
        text.text = "Item text";
        text.color = Color.black;
        text.alignment = TextAnchor.MiddleCenter;

        var textRect = listItemText.GetComponent<RectTransform>();
        textRect.anchorMin = new Vector2(0, 0);
        textRect.anchorMax = new Vector2(1, 1);
        textRect.sizeDelta = new Vector2(0, 0);

        GameObjectUtility.SetParentAndAlign(listItemText, listItem);
        GameObjectUtility.SetParentAndAlign(listItem, content);

        return listItem;
    }
}