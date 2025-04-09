using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

[CustomEditor(typeof(MagicList))]
public class MagicListEditor : Editor
{
    private SerializedProperty _listItemPrefab;
    private SerializedProperty _listTransform;
    private Color _backgroundColor;
    private Image _backgroundImage;
    private Color _scrollbarColor;
    private Scrollbar _scrollbarImage;

    private Color BackgroundColor
    {
        get => _backgroundColor;
        set
        {
            _backgroundColor = value;
            _backgroundImage.color = value;
            EditorApplication.QueuePlayerLoopUpdate();
        }
    }

    private Color ScrollbarColor
    {
        get => _scrollbarColor;
        set
        {
            _scrollbarColor = value;
            _scrollbarImage.GetComponent<Image>().color = value * new Color( .9f, .9f,  .9f, 1f);
            _scrollbarImage.colors = ChangeBlockBaseColor(_scrollbarImage.colors, value);
            EditorApplication.QueuePlayerLoopUpdate();
        }
    }

    private static ColorBlock ChangeBlockBaseColor(ColorBlock originalBlock, Color newColor)
    {
        originalBlock.normalColor = newColor;
        originalBlock.highlightedColor = newColor * new Color( .9f, .9f,  .9f, 1f);
        originalBlock.pressedColor = newColor * new Color( .8f, .8f,  .8f, 1f);
        originalBlock.selectedColor =  newColor * new Color( .9f, .9f,  .9f, 1f);
        originalBlock.disabledColor = newColor * new Color( .8f, .8f,  .8f, .5f);
        return originalBlock;
    }

    private void OnEnable()
    {
        _listTransform = serializedObject.FindProperty("listTransform");
        _listItemPrefab = serializedObject.FindProperty("listItemPrefab");
        _backgroundImage = ((MagicList) target).gameObject.GetComponent<Image>();
        BackgroundColor = _backgroundImage.color;
        _scrollbarImage = ((MagicList) target).gameObject.GetComponentInChildren<Scrollbar>();
        ScrollbarColor = _scrollbarImage.colors.normalColor;
    }

    public override void OnInspectorGUI()
    {
        serializedObject.Update();

        var textStyle = EditorStyles.label;
        textStyle.richText = true;
        textStyle.wordWrap = true;

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("<b>List Item Prefab</b>", textStyle);
        EditorGUILayout.LabelField("The prefab that will be instantiated for each list item", textStyle);
        EditorGUILayout.HelpBox(new GUIContent("Drag it from your Assets, not from your scene Hierarchy"));
        EditorGUILayout.PropertyField(_listItemPrefab);
        if (GUILayout.Button(new GUIContent("Generate",
                "Creates a generic ListItemPrefab that you can modify, to save you some time")))
        {
            GenerateListItemPrefab();
            EditorGUIUtility.PingObject(((MagicList) target).listItemPrefab.gameObject);
        }

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("<b>List Transform</b>", textStyle);
        EditorGUILayout.LabelField("The list itself, parent of the list items", textStyle);
        EditorGUILayout.HelpBox(new GUIContent("Drag it from your scene Hierarchy, often called \"Content\""));
        EditorGUILayout.PropertyField(_listTransform);
        if (GUILayout.Button(new GUIContent("Locate",
                "Locates the list Transform in your scene")))
            EditorGUIUtility.PingObject(((MagicList) target).listTransform.gameObject);

        EditorGUILayout.Space(10);
        EditorGUILayout.LabelField("<b>Quick Colors Picker</b>", textStyle);
        EditorGUILayout.LabelField("Change list colors quickly", textStyle);
        BackgroundColor = EditorGUILayout.ColorField("Background", _backgroundColor);
        ScrollbarColor = EditorGUILayout.ColorField("Scrollbar", _scrollbarColor);

        EditorGUILayout.Space(10);

        serializedObject.ApplyModifiedProperties();
    }


    private void GenerateListItemPrefab()
    {
        var t = (MagicList) target;
        var item = MagicListMenu.GenerateListItem(t.listTransform.gameObject);
        var prefab =
            PrefabUtility.SaveAsPrefabAssetAndConnect(item, "Assets/ListItemPrefab.prefab",
                InteractionMode.UserAction);
        t.listItemPrefab = prefab;
        serializedObject.ApplyModifiedProperties();
    }
}