using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[System.Serializable]
public class SerializableDictionary : Dictionary<string, Language>,ISerializationCallbackReceiver {
    [SerializeField]private List<string> _keys=new List<string>();
    [SerializeField]private List<Language> _values=new List<Language>();
    public void OnBeforeSerialize()
    {
        _keys.Clear();
        _values.Clear();
        foreach (var item in this)
        {
            _keys.Add(item.Key);
            _values.Add(item.Value);
        }
    }

    public void OnAfterDeserialize()
    {
        this.Clear();
        for (int i = 0; i < Mathf.Min(_keys.Count, _values.Count); i++)
        {
            this.Add(_keys[i], _values[i]);
        }
    }

    
}
