using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;

public class FormatableData : MonoBehaviour
{
    [SerializeField] GameObject student;

    ResultData resultData;

    string activeSheetPath = "Assets/Student Data/ActiveData.csv";
    string checkedSheetPath = "Assets/Student Data/CheckedData.csv";

    private void Awake()
    {
        resultData = FindObjectOfType<ResultData>();    
    }

    void Start()
    {
        destroyTheChildElements();
        generateBatch(1);
        resultData.setActiveBatch(1);
    }

    public void generateBatch(int batchNo)
    {
        destroyTheChildElements();

        List<string[]> selectedRecords = new List<string[]>();
        List<Toggle> toggles = new List<Toggle>();

        StreamReader checkedReader = new StreamReader(checkedSheetPath);
        while (!checkedReader.EndOfStream)
        {
            string line = checkedReader.ReadLine();
            string[] arr = line.Split(',');
            if (arr[2] == ("Batch " + batchNo))
            {
                Toggle toggle = student.GetComponentInChildren<Toggle>();
                TextMeshProUGUI studentName = student.GetComponentInChildren<TextMeshProUGUI>();
                string goStudent = arr[0] + " (" + arr[1] + ")";
                toggle.isOn = true;
                studentName.text = goStudent;

                GameObject instantiatedStudent = Instantiate(student, transform);

                selectedRecords.Add(arr);
                toggles.Add(instantiatedStudent.GetComponentInChildren<Toggle>());

            }
        }
        checkedReader.Close();

        StreamReader reader = new StreamReader(activeSheetPath);
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            string[] arr = line.Split(',');
            if(arr[2] == "Batch "+batchNo)
            {
                Toggle toggle = student.GetComponentInChildren<Toggle>();
                TextMeshProUGUI studentName = student.GetComponentInChildren<TextMeshProUGUI>();
                string goStudent = arr[0] + " (" + arr[1] + ")";
                toggle.isOn = false;
                studentName.text = goStudent;

                GameObject instantiatedStudent = Instantiate(student, transform);

                selectedRecords.Add(arr);
                toggles.Add(instantiatedStudent.GetComponentInChildren<Toggle>());
            }
        }
        reader.Close();

        resultData.setSelectedRecords(selectedRecords);
        resultData.setToggles(toggles);

        ScrollToTop();

        resultData.setActiveBatch(batchNo);
    }

         
    void destroyTheChildElements()
    {
        foreach(Transform child in transform)
        {
            Destroy(child.gameObject);
        }
    }

    void ScrollToTop()
    {
        FindObjectOfType<ScrollRect>().normalizedPosition = new Vector2(0, 1);
    }
}
