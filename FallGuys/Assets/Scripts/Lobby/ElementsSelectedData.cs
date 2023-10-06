using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementsSelectedData : MonoBehaviour
{
    public static ElementsSelectedData Instance { get; private set; }

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
        Debug.Log("Dictionary size = " + _allSelectedElementsList.Count);
    }

    private const string saveKey = "ElementsSelectedDataSave";

    private Dictionary<string, int> _allSelectedElementsList = new();


    public int GetActiveSelectedIndex(string saveName)
    {
        int activeIndex = 0;

        if (_allSelectedElementsList.ContainsKey(saveName))// ���� ������� ����, �� �������� ��� ����
        {
            activeIndex = _allSelectedElementsList[saveName];
        }
        else // ���� ����, �� ������� 
        {
            _allSelectedElementsList.Add(saveName, activeIndex);       // �������� �������� �� ������������ �����. ���� ����� ��� ����, �� ���. �����

            Save();
        }

        return activeIndex;
    }

    public void SaveElevemtSelectedIndex(string saveName, int selectedIndex)// ���������� ��� ����� ��������
    {
        _allSelectedElementsList[saveName] = selectedIndex;

        Save();
    }


    #region SaveLoad
    private void Load()
    {
        var data = PunchCars.SaveData.SaveManager.Load<PunchCars.SaveData.ElementsSelect>(saveKey);

        if (data.allSelectedElementsList == null) data.allSelectedElementsList = _allSelectedElementsList;

        _allSelectedElementsList = data.allSelectedElementsList;
    }

    private void Save()
    {
        PunchCars.SaveData.SaveManager.Save(saveKey, GetElementsSelect());
    }

    private PunchCars.SaveData.ElementsSelect GetElementsSelect()
    {
        var data = new PunchCars.SaveData.ElementsSelect()
        {
            allSelectedElementsList = _allSelectedElementsList
        };

        return data;
    }
    #endregion
}
