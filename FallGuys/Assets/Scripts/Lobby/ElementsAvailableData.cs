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
                    
                                            // ��������� ������ ������ �� ������� (����, �����, ���� � �.�.)
                                            // ��������� ����� ���������� (�����������). �� ���� ����� ��� ������ �������, ������ �� � ���� ��������

    public bool GetAvailableStatus(string saveName)// �� ����������� ����� �������� ������, ������ �� ���� ���������
    {// ���� ����� �������� ������ ��������� � ������ ONENABLE

        bool isAvailable = false;

        if (_allAvailableElementsList.ContainsKey(saveName))// ���� ������� ����, �� �������� ��� ����
        {
            isAvailable = _allAvailableElementsList[saveName];
        }
        else // ���� ����, �� ������� 
        {
            _allAvailableElementsList.Add(saveName, isAvailable);       // �������� �������� �� ������������ �����. ���� ����� ��� ����, �� ���. �����
            
            Save();
        }

        //Debug.LogError($"Get {saveName} is Available status = {isAvailable}");

        return isAvailable;
    }

    public void SaveElevemtAvailableStatus(string saveName, bool availableStatus)// ���������� ��� ����� ��������
    {
        _allAvailableElementsList[saveName] = availableStatus;

        //Debug.LogError($"Save {saveName} is Available {availableStatus}");

        Save();
    }

    #region SaveLoad
    private void Load()
    {
        var data = PunchCars.SaveData.SaveManager.Load<PunchCars.SaveData.ElementsAvailable>(saveKey);

        if (data.allAvailableElementsList == null) data.allAvailableElementsList = _allAvailableElementsList;

        _allAvailableElementsList = data.allAvailableElementsList;
    }

    private void Save()
    {
        PunchCars.SaveData.SaveManager.Save(saveKey, GetElementsAvailable());
    }

    private PunchCars.SaveData.ElementsAvailable GetElementsAvailable()
    {
        var data = new PunchCars.SaveData.ElementsAvailable()
        {
            allAvailableElementsList = _allAvailableElementsList
        };

        return data;
    }
    #endregion
}
