using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using TMPro;
using UnityEngine.UI;
using System.Linq;
using static UnityEngine.SceneManagement.SceneManager;

public class GenerateResult : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI countLabel, batchLabel;

    List<string[]> data;
    List<string[]> selectedRecords;
    List<Toggle> toggles;
    ResultData resultData;
    DisplayData displayData;
    string activeSheetPath = "Assets/Student Data/ActiveData.csv";
    string checkedSheetPath = "Assets/Student Data/CheckedData.csv";
    private void Start()
    {
        resultData = FindObjectOfType<ResultData>();
        displayData = FindObjectOfType<DisplayData>();
        TakeDataFromSheet();
    }

    void TakeDataFromSheet()
    {
        data = new List<string[]>();
        StreamReader reader = new StreamReader(activeSheetPath);
        while (!reader.EndOfStream)
        {
            string line = reader.ReadLine();
            string[] arr = line.Split(',');
            data.Add(arr);
            //Debug.Log(arr[0]);
        }

        resultData.setCountLabel(countLabel);
        reader.Close();
    }

    public void SplitDataAcrossTheSheets(bool isAppend)
    {
        List<string[]> selectedRecords = resultData.getSelectedRecords();
        List<Toggle> toggles = resultData.getToggles();
        List<int> originalIndex = resultData.getOriginalIndex();
        List<string[]> finalResult = resultData.getFinalResult();
        List<int> indexesToDelete = new List<int>();

        List<string> ids = new List<string>();
        for (int i = 0; i < selectedRecords.Count; i++)
        {
            if (toggles[i].isOn)
            {
                ids.Add(selectedRecords[i][1]);
                indexesToDelete.Add(originalIndex[i]);
            }
        }

        foreach(int i in indexesToDelete.OrderByDescending(l => l))
        {
            finalResult.RemoveAt(i);
        }

        //resultData.setFinalResult(finalResult); //no need of this, because of list in c# has 2 way binding.

        if(isAppend)
            File.AppendAllLines(checkedSheetPath, File.ReadAllLines(activeSheetPath).Where(l => ids.Contains(l.Split(',')[1])).ToList());
        else
            File.WriteAllLines(checkedSheetPath, File.ReadAllLines(activeSheetPath).Where(l => ids.Contains(l.Split(',')[1])).ToList());
        File.WriteAllLines(activeSheetPath, File.ReadAllLines(activeSheetPath).Where(l => !ids.Contains(l.Split(',')[1])).ToList());
    }

    public void ReformatDataAcrossTheSheets()
    {
        List<string[]> selectedRecords = resultData.getSelectedRecords();
        List<Toggle> toggles = resultData.getToggles();

        List<string> pendingStudents = new List<string>();
        List<string> checkedStudents = new List<string>();

        for (int i = 0; i < selectedRecords.Count; i++)
        {
            string fileFormat = selectedRecords[i][0] + "," + selectedRecords[i][1] + "," + selectedRecords[i][2];
            if (toggles[i].isOn)
            {
                checkedStudents.Add(fileFormat);
            }
            else
            {
                pendingStudents.Add(fileFormat);
            }
        }

        List<string> existedActiveBatches = File.ReadAllLines(activeSheetPath).Where(l => l.Split(',')[2] != ("Batch "+resultData.getActiveBatch())).ToList();
        List<string> existedCheckedBatches = File.ReadAllLines(checkedSheetPath).Where(l => l.Split(',')[2] != ("Batch " + resultData.getActiveBatch())).ToList();
        //using (TextWriter ASheet = new StreamWriter(activeSheetPath))
        //{
            //int length = pendingStudents.Count + existedActiveBatches.Count;
            //int index = 0;
            //foreach (string s in pendingStudents.Concat(existedActiveBatches))
            //{
                //ASheet.WriteLine(s);

                // if(index == length-1)
                //index++;
            //}
            //AddCheckedData(checkedStudents, existedCheckedBatches);

        //}

        File.WriteAllLines(checkedSheetPath, checkedStudents.Concat(existedCheckedBatches));
        File.WriteAllLines(activeSheetPath, pendingStudents.Concat(existedActiveBatches));
    }

    //void AddCheckedData(List<string> checkedStudents, List<string> existedCheckedBatches)
    //{
     //   using (TextWriter CSheet = new StreamWriter(checkedSheetPath))
     //   {
    //        foreach (string s in checkedStudents.Concat(existedCheckedBatches))
    //            CSheet.WriteLine(s);
    //    }
    //}

    public void generateResult()
    {
        IEnumerable<string[]> finalData = from i in data
                      where i[2] == batchLabel.text
                      select i;
        resultData.setFinalResult(finalData.ToList());
        LoadScene(1);
    }

}
