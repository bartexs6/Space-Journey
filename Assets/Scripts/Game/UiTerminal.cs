using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UiTerminal : MonoBehaviour
{
    private static GameObject canvas;
    private static GameObject terminalPanel;
    private static GameObject AcceptButton;
    private static List<GameObject> Advermisments = new List<GameObject>();
    private static TMP_Text TitleText;
    private static TMP_Text DescriptionText;
    private static GameObject ClickedButton;

    private static QuestGiver Npc;
    

    private static List<Sprite> sprites = new List<Sprite>();

    private static List<Quest> questList = new List<Quest>();
    private static Quest SelectedQuest;

    public static bool GetUIElements()
    {
        try
        {
            canvas = GameObject.Find("Canvas");
            terminalPanel = canvas.transform.Find("TerminalPanel").gameObject;
            Advermisments.Add(terminalPanel.transform.Find("QuestButton_1").gameObject);
            Advermisments.Add(terminalPanel.transform.Find("QuestButton_2").gameObject);
            Advermisments.Add(terminalPanel.transform.Find("QuestButton_3").gameObject);
            Advermisments.Add(terminalPanel.transform.Find("QuestButton_4").gameObject);

            TitleText = terminalPanel.transform.Find("QuestTitle").GetComponent<TMP_Text>();
            DescriptionText = terminalPanel.transform.Find("QuestDescription").GetComponent<TMP_Text>();

            AcceptButton = terminalPanel.transform.Find("AcceptButton").gameObject;

            sprites.Add(Resources.Load<Sprite>("Materials/PNG/TerminalUI/Quest_Paper1"));
            sprites.Add(Resources.Load<Sprite>("Materials/PNG/TerminalUI/Quest_Paper2"));

            terminalPanel.SetActive(false);

            return true;
        }
        catch (Exception e)
        {
            // Wyśiwetl błąd
            Debug.LogError("Couldn't find gameobject in UiTerminal.cs");
            Debug.LogError(e);
            return false;
        }
    }

    public static void SetUpTerminal(List<Quest> quests)
    {
        Time.timeScale = 0f;
        questList = quests;
        terminalPanel.SetActive(true);
        TitleText.text = "_Witamy_";
        foreach (GameObject item in Advermisments)
        {
            item.SetActive(false);
        }

        for (int i = 0; i < quests.Count; i++)
        {
            Advermisments[i].SetActive(true);
        }

        if (quests.Count > 0)
        {
            DescriptionText.text = "Prosze wybrać zadanie z listy obok ;)";
        }
        else
        {
            DescriptionText.text = "Brak zadań :(((";
        }

        AcceptButton.SetActive(false);

    }

    public static void UpdateGiver(QuestGiver sender)
    {
        Npc = sender;
    }

    //ale gowno
    public void SelectingQuest()
    {
        if(ClickedButton != null) ClickedButton.GetComponent<Button>().interactable = true;

        ClickedButton = EventSystem.current.currentSelectedGameObject.gameObject;
        int index = Advermisments.FindInstanceID<GameObject>(ClickedButton);

        if (ClickedButton != null) SelectedQuest = questList[index];

        if (SelectedQuest != null)
        {
            ClickedButton.GetComponent<Button>().interactable = false;
            UpdateTerminal();
        }
    }

    private static void UpdateTerminal()
    {
        TitleText.text = SelectedQuest.title;
        DescriptionText.text = SelectedQuest.description;
        AcceptButton.SetActive(true);
    }

    public void AcceptQuest()
    {
        QuestGoal.ActivateQuest(SelectedQuest);
        questList.Remove(SelectedQuest);
        Npc.RemoveQuest(SelectedQuest);
        SetUpTerminal(questList);
    }

    public void ExitTerminal()
    {
        if (ClickedButton != null) ClickedButton.GetComponent<Button>().interactable = true;
        Time.timeScale = 1f;
        terminalPanel.SetActive(false);
    }

}