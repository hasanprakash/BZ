using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using static UnityEngine.SceneManagement.SceneManager;

public class DisplayData : MonoBehaviour
{
    [SerializeField] List<Transform> container;

    ResultData resultData;

    List<string[]> dataToShuffle;
    List<string[]> selectedRecords;
    List<Toggle> toggles;
    List<int> originalIndex;
    int totalValues;


    private void Awake()
    {
        
    }


    void Start()
    {
        ShuffleAndDisplay(0);
    }


    public void ShuffleAndDisplay(int owner)
    {
        if (owner == 1)
        {
            SaveProgress();
        }

        resultData = FindObjectOfType<ResultData>();
        totalValues = int.Parse(resultData.getCountLabel().text);
        List<string[]> finalResult = resultData.getFinalResult();
        dataToShuffle = new List<string[]>(finalResult);
        selectedRecords = new List<string[]>();
        toggles = new List<Toggle>();
        originalIndex = new List<int>();
        
        if (totalValues > dataToShuffle.Count)
        {
            Debug.Log("No enough Data");
            return;
        }
        for (int i = 0; i < totalValues; i++)
        {
            int index = Random.Range(0, dataToShuffle.Count);
            originalIndex.Add(index);

            selectedRecords.Add(dataToShuffle[index]);

            Toggle toggle = container[i].GetComponentInChildren<Toggle>();
            toggle.isOn = false;
            toggles.Add(toggle);

            container[i].GetComponentInChildren<TextMeshProUGUI>().text = dataToShuffle[index][0] + " - " + dataToShuffle[index][1];

            if (dataToShuffle.Count != 0)
            dataToShuffle.RemoveAt(index);
        }


        resultData.setSelectedRecords(selectedRecords);
        resultData.setToggles(toggles);
        resultData.setOriginalIndex(originalIndex);

        if (totalValues == 1)
        {
            Destroy(container[1].gameObject);
            Destroy(container[2].gameObject);
        }
        else if (totalValues == 2)
        {
            Destroy(container[2].gameObject);
        }
    }

    void SaveProgress()
    {
        FindObjectOfType<GameManager>().refreshWithOutGoingBack();
    }

}