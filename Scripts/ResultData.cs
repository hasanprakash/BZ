using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ResultData : MonoBehaviour
{

    List<string[]> finalResult;
    static ResultData instance;
    TextMeshProUGUI countLabel;
    List<string[]> selectedRecords;
    List<Toggle> toggles;
    int activeBatch;
    List<int> originalIndex;

    private void Awake()
    {
        PreventDestroy();
    }

    void PreventDestroy()
    {
        if(instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            instance = this;
        }
    }

    // FINAL RANDOM RESULT
    public void setFinalResult(List<string[]> data)
    {
        finalResult = data;
    }

    public List<string[]> getFinalResult()
    {
        return finalResult;
    }

    // STUDENT COUNT LABEL
    public void setCountLabel(TextMeshProUGUI label)
    {
        countLabel = label;
    }

    public TextMeshProUGUI getCountLabel()
    {
        return countLabel;
    }

    // SELECTED RECORDS
    public void setSelectedRecords(List<string[]> selectedRecords)
    {
        this.selectedRecords = selectedRecords;
    }
    public List<string[]> getSelectedRecords()
    {
        return selectedRecords;
    }

    // TOGGLES
    public void setToggles(List<Toggle> toggles)
    {
        this.toggles = toggles;
    }
    public List<Toggle> getToggles()
    {
        return toggles;
    }

    // ORIGINAL INDICES
    public void setOriginalIndex(List<int> originalIndex)
    {
        this.originalIndex = originalIndex;
    }
    public List<int> getOriginalIndex()
    {
        return originalIndex;
    }


    // ACTIVE BATCH IN FORMATTER SCENE
    public void setActiveBatch(int batchNo)
    {
        activeBatch = batchNo;
    }
    public int getActiveBatch()
    {
        return activeBatch;
    }
}
