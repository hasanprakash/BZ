using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;
using static UnityEngine.SceneManagement.SceneManager;

public class GameManager : MonoBehaviour
{
    [SerializeField] GameObject panel;

    //DisplayData displayData;
    static GenerateResult generateResult;
    static GameManager gameInstance;

    private void Awake()
    {
        PreventDestroy();
        
    }

    private void Start()
    {
        generateResult = FindObjectOfType<GenerateResult>();
    }

    void PreventDestroy()
    {
        if(gameInstance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
            gameInstance = this;
        }
    }

    public static void goBack()
    {
        generateResult.SplitDataAcrossTheSheets(true);
        LoadScene(0);        
    }
    public void refreshWithOutGoingBack()
    {
        generateResult.SplitDataAcrossTheSheets(true);
    }

    public static void formatterToHome()
    {
        generateResult.ReformatDataAcrossTheSheets();
        LoadScene(0);
    }

    public static void goToHome()
    {
        LoadScene(0);
    }

    public static void getAbout(GameObject panel)
    {
        panel.SetActive(true);
    }

    public static void closeAbout(GameObject panel)
    {
        panel.SetActive(false);
    }

    public void getStudents()
    {
        LoadScene(1);
    }

    public static void goToFormatStudents()
    {
        LoadScene(2);
    }

    public static void onExit()
    {
        Application.Quit();
    }


    //public static void PasteDataToSheet()
    //{
    //    Debug.Log("Pasting Data");
    //    StreamWriter writer = new StreamWriter(checkedSheetPath);
    //    for(int i=0;i<displayData.selectedRecords.Count;i++)
    //    {
    //        Debug.Log(displayData.selectedRecords[i][0] + " " + displayData.selectedRecords[i][2]);
    //    }
    //}

}
