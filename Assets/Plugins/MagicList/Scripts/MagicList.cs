using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using JetBrains.Annotations;
using UnityEngine;

public class MagicList : MonoBehaviour
{
    public GameObject listItemPrefab;
    public Transform listTransform;

    private bool _isDataAssigned;
    private INotifyCollectionChanged _oldData;

    private void Start()
    {
        if (listItemPrefab == null) Debug.LogWarning("You must assign a ListItemPrefab to your MagicList");
        if (listTransform == null) Debug.LogWarning("You must assign a ListTransform to your MagicList");
        if (!_isDataAssigned) Debug.LogWarning("MagicList.SetData was never called");
    }

    private void OnDestroy()
    {
        if (_oldData != null) _oldData.CollectionChanged -= OnCollectionChanged;
    }

    private void OnCollectionChanged<T>(T sender, NotifyCollectionChangedEventArgs e)
    {
        switch (e.Action)
        {
            case NotifyCollectionChangedAction.Add:
                var added = Instantiate(listItemPrefab, listTransform, false);
                added.transform.SetSiblingIndex(e.NewStartingIndex);
                BindView(e.NewStartingIndex, e.NewItems[0]);
                break;
            case NotifyCollectionChangedAction.Move:
                var moved = listTransform.GetChild(e.OldStartingIndex);
                moved.transform.SetSiblingIndex(e.NewStartingIndex);
                BindView(e.NewStartingIndex, e.NewItems[0]);
                break;
            case NotifyCollectionChangedAction.Remove:
                DestroyImmediate(listTransform.GetChild(e.OldStartingIndex).gameObject);
                break;
            case NotifyCollectionChangedAction.Replace:
                BindView(e.NewStartingIndex, e.NewItems[0]);
                break;
            case NotifyCollectionChangedAction.Reset:
                foreach (Transform o in listTransform) DestroyImmediate(o.gameObject);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void BindView<T>(int index, T item)
    {
        var type = item.GetType();
        var listType = typeof(IListItemRenderer<>).MakeGenericType(type);
        listTransform.GetChild(index).TryGetComponent(listType, out var viewRenderer);
        if (viewRenderer is null)
        {
            Debug.LogWarning("ListItemPrefab doesn't implement IListItemRenderer");
            return;
        }

        var method = listType.GetMethod("BindView");
        method?.Invoke(viewRenderer, new object[] {item});
    }

    public void SetData<T>(ICollection<T> data)
    {
        if (_oldData != null) _oldData.CollectionChanged -= OnCollectionChanged;
        _isDataAssigned = true;
        InitializeData(data);
        ((INotifyCollectionChanged) data).CollectionChanged += OnCollectionChanged;
        _oldData = (INotifyCollectionChanged) data;
    }

    private void InitializeData<T>([NotNull] ICollection<T> data)
    {
        if (data == null) throw new ArgumentNullException(nameof(data));
        foreach (Transform o in listTransform) Destroy(o.gameObject);
        foreach (var x1 in data)
        {
            var added = Instantiate(listItemPrefab, listTransform, false);
            BindView(added.transform.GetSiblingIndex(), x1);
        }
    }
}