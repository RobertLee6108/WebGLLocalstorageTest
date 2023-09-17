using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LocalStoragetest : MonoBehaviour
{
    [Space(20),Header("저장")]
    public TMP_InputField _saveKey;
    public TMP_InputField _saveValue;
    public Button summit;
    
    [Space(20),Header("로드")]
    public TMP_InputField loadkey;
    public TextMeshProUGUI loadData;
    public Button load;

    [Space(20),Header("삭제")]
    public TMP_InputField delKey;
    public TextMeshProUGUI delData;
    public Button del;
    
    private void Awake () 
    {
        summit.onClick.AddListener(Summit);
        load.onClick.AddListener(Load);
        del.onClick.AddListener(DeleteKey);
    }
    
    public void Summit()
    {
        SaveData(_saveKey.text, _saveValue.text);
    }
    
    public void SaveData(string key, string value)
    {
        if(string.IsNullOrEmpty(key))
        {
            Debug.Log("키값 비었음");
            return;
        }
#if UNITY_WEBGL && !UNITY_EDITOR
        saveToLocalStorage( key,value);
        Debug.Log($"js call {key}\n" +
                  $"{value}"); 
#elif UNITY_EDITOR
        PlayerPrefs.SetString(key,value);
        Debug.Log($"{key}\n" +
                  $"{value}");
#endif
    }
    
    public void Load()
    {
        if(string.IsNullOrEmpty(loadkey.text))
        {
            Debug.Log("키값 비었음");
            return;
        }
#if UNITY_WEBGL && !UNITY_EDITOR
        if (checkLocalStorageKey(loadkey.text))
        {
            var playerPrefesDsta = loadFromLocalStorage(loadkey.text);
            loadData.text = playerPrefesDsta;
            Debug.Log(playerPrefesDsta);
        }
        else
        {
            loadData.text = "no data";
            Debug.Log("no data");
        }
#elif UNITY_EDITOR
        if (PlayerPrefs.HasKey(loadkey.text))
        {
            PlayerPrefs.GetString(loadkey.text);
            loadData.text = PlayerPrefs.GetString(loadkey.text);
            Debug.Log(PlayerPrefs.GetString(loadkey.text));
        }
        else
        {
            loadData.text = "no data";
            Debug.Log("no data");
        }
#endif
    }

    public void DeleteKey()
    {
        if(string.IsNullOrEmpty(delKey.text))
        {
            Debug.Log("키값 비었음");
            return;
        }
#if UNITY_WEBGL && !UNITY_EDITOR
        if (checkLocalStorageKey(delKey.text))
        {
            removeFromLocalStorage(delKey.text);
            delData.text = "Key removed from JS localStorage";
            Debug.Log("Key removed from JS localStorage");
        }
        else
        {
            delData.text = "Key does not exist localStorage";
            Debug.Log("Key does not exist JS localStorage");
        }
#elif UNITY_EDITOR
        if (PlayerPrefs.HasKey(delKey.text)) 
        {
            PlayerPrefs.DeleteKey(delKey.text);
            delData.text = "Key removed from PlayerPrefs";
            Debug.Log("Key removed from PlayerPrefs");
        } 
        else 
        {
            delData.text = "Key does not exist in PlayerPrefs";
            Debug.Log("Key does not exist in PlayerPrefs");
        }
#endif
    }

#if UNITY_WEBGL && !UNITY_EDITOR
    [DllImport("__Internal")]
    private static extern bool checkLocalStorageKey(string key);

    [DllImport("__Internal")]
    private static extern void saveToLocalStorage(string key, string value);
    
    [DllImport("__Internal")]
    private static extern string loadFromLocalStorage(string key);
    
      [DllImport("__Internal")]
    private static extern void removeFromLocalStorage(string key);
#endif
}
