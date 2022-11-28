using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsAvailableData : MonoBehaviour
{
    public static ElementsAvailableData Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }

        Load();
        Debug.Log("Dictionary size = " + _allAvailableElementsList.Count);
    }

    private const string saveKey = "ElementsAvailableDataSave";

    private Dictionary<string, bool> _allAvailableElementsList = new();
                    
                                            // ÇÀÃĞÓÆÀÅÌ ÒÎËÜÊÎ ÎÒÊĞÛÒ ËÈ İËÅÌÅÍÒ (ÀÂÒÎ, ÏÓØÊÀ, ÖÂÅÒ È Ò.Ä.)
                                            // ÎÑÒÀËÜÍÎÅ ÌÎÆÅÒ ÈÇÌÅÍßÒÜÑß (ÎÁÍÎÂËßÒÜÑß). ÍÎ ÅÑËÈ ÈÃĞÎÊ ÓÆÅ ÎÒÊĞÛË İËÅÌÅÍÒ, ÇÍÀ×ÈÒ ÎÍ Ó ÍÅÃÎ ÍÀÂÑÅÃÄÀ

    public bool GetAvailableStatus(string saveName)// ïî óíèêàëüíîìó èìåíè ïîëó÷àåì áóëüêó, îòêğûò ëè ıòîò êîìïîíåíò
    {// İÒÎÒ ÌÅÒÎÄ ÂÛÇÛÂÀÅÒ ÊÀÆÄÛÉ ÊÎÌÏÎÍÅÍÒ Â ÌÎÌÅÍÒ ONENABLE

        bool isAvailable = false;

        if (_allAvailableElementsList.ContainsKey(saveName))// åñëè ıëåìåíò åñòü, òî ïîëó÷àåì åãî èíôó
        {
            isAvailable = _allAvailableElementsList[saveName];
        }
        else // åñëè íåòó, òî ñîçäàåì 
        {
            _allAvailableElementsList.Add(saveName, isAvailable);       // ÍÀÏÈÑÀÒÜ ÏĞÎÂÅĞÊÓ ÍÀ ÓÍÈÊÀËÜÍÎÑÒÜ ÈÌÅÍÈ. ÅÑËÈ ÒÀÊÎÅ ÓÆÅ ÅÑÒÜ, ÒÎ ÄÎÁ. ÖÈÔĞÛ
            
            Save();
        }

        return isAvailable;
    }

    public void SaveElevemtAvailableStatus(string saveName, bool availableStatus)// âûçûâàåòñÿ ïğè ñìåíå çíà÷åíèÿ
    {
        _allAvailableElementsList[saveName] = availableStatus;

        Save();
    }

    #region SaveLoad
    private void Load()
    {
        var data = SaveManager.Load<SaveData.ElementsAvailable>(saveKey);

        if (data.allAvailableElementsList == null) data.allAvailableElementsList = _allAvailableElementsList;

        _allAvailableElementsList = data.allAvailableElementsList;
    }

    private void Save()
    {
        SaveManager.Save(saveKey, GetElementsAvailable());
    }

    private SaveData.ElementsAvailable GetElementsAvailable()
    {
        var data = new SaveData.ElementsAvailable()
        {
            allAvailableElementsList = _allAvailableElementsList
        };

        return data;
    }
    #endregion
}
