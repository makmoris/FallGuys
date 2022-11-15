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
                    
                                            // ЗАГРУЖАЕМ ТОЛЬКО ОТКРЫТ ЛИ ЭЛЕМЕНТ (АВТО, ПУШКА, ЦВЕТ И Т.Д.)
                                            // ОСТАЛЬНОЕ МОЖЕТ ИЗМЕНЯТЬСЯ (ОБНОВЛЯТЬСЯ). НО ЕСЛИ ИГРОК УЖЕ ОТКРЫЛ ЭЛЕМЕНТ, ЗНАЧИТ ОН У НЕГО НАВСЕГДА

    public bool GetAvailableStatus(string saveName)// по уникальному имени получаем бульку, открыт ли этот компонент
    {// ЭТОТ МЕТОД ВЫЗЫВАЕТ КАЖДЫЙ КОМПОНЕНТ В МОМЕНТ ONENABLE

        bool isAvailable = false;

        if (_allAvailableElementsList.ContainsKey(saveName))// если элемент есть, то получаем его инфу
        {
            isAvailable = _allAvailableElementsList[saveName];
        }
        else // если нету, то создаем 
        {
            _allAvailableElementsList.Add(saveName, isAvailable);
            Debug.Log("Dobavily   " + _allAvailableElementsList.Count);
            
            Save();
        }

        return isAvailable;
    }

    public void SaveElevemtAvailableStatus(string saveName, bool availableStatus)// вызывается при смене значения
    {
        _allAvailableElementsList[saveName] = availableStatus;

        Save();
    }

    #region SaveLoad
    private void Load()
    {
        var data = SaveManager.Load<SaveData.ElementsAvailable>(saveKey);

        if (data.allAvailableElementsList == null)
        {
            Debug.Log("data pystaya");
            data.allAvailableElementsList = _allAvailableElementsList;
        }
        else
        {
            Debug.Log("data ne pystaya");
        }
        
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
