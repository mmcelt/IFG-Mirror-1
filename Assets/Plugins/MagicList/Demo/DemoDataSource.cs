using System.Collections;
using System.Collections.ObjectModel;
using UnityEngine;

public class DemoDataSource : MonoBehaviour
{
    public MagicList magicList;

    [Range(0, 100)] public int maxItems = 10;

    private readonly ObservableCollection<string> _data = new();

    private void Awake()
    {
        magicList.SetData(_data);
        StartCoroutine(RandomizeData());
    }

    private IEnumerator RandomizeData()
    {
        for (var i = 0; i < Random.Range(0, _data.Count); i++)
            _data.RemoveAt(i);

        for (var i = 0; i < Random.Range(0, maxItems); i++)
            _data.Add(Random.Range(1, 1000).ToString());

        yield return new WaitForSeconds(.1f);
        yield return RandomizeData();
    }
}