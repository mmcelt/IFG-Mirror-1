using UnityEngine;
using UnityEngine.UI;

public class DemoListItemRenderer : MonoBehaviour, IListItemRenderer<string>
{
    private Text _itemText;

    private void Awake()
    {
        _itemText = transform.Find("Text").GetComponent<Text>();
    }

    public void BindView(string value)
    {
        _itemText.text = value;
    }
}