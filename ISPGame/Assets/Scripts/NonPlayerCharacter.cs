 using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayerCharacter : MonoBehaviour
{
    public string charName;
    public GameObject dialogCanvas;
    public QuestGiver questGiver;
    public float displayTime;
    float timerDisplay;

    public GameObject unsaidTextSprite;
    public GameObject currentlyHasQuestSprite;

    bool hasSaidThing;

    bool currentlyHasQuest;
    public bool completedQuest;

    void Start()
    {
        currentlyHasQuest = false;
        currentlyHasQuestSprite.SetActive(false);

        hasSaidThing = false;
        unsaidTextSprite.SetActive(true);

        clearCanvas();
        dialogCanvas.SetActive(false);
        timerDisplay = -1.0f;
    }

    void Update()
    {

        currentlyHasQuest = questGiver.quest.isActive;

        if (timerDisplay >= 0)
        {
            timerDisplay -= Time.deltaTime;
            if (timerDisplay < 0)
            {
                dialogCanvas.SetActive(false);
            }
        }

        if(currentlyHasQuest && questGiver.quest.questGoal.IsReached())
        {
            questGiver.quest.Complete();
            questGiver.OnQuestCompleteShowCheck();
            completeQuest1();
        }

    }

    public void DisplayDialog()
    {
        timerDisplay = displayTime;
        dialogCanvas.SetActive(true);
        activateCorrectText();

        if(!hasSaidThing)
        {
            unsaidTextSprite.SetActive(false);
            hasSaidThing = true;
        }
    }

    public void giveQuest1()
    {
        questGiver.OpenQuestWindow();
        currentlyHasQuest = true;
        currentlyHasQuestSprite.SetActive(true);
    }


    void activateCorrectText()
    {
        clearCanvas();
        if(!currentlyHasQuest && !completedQuest)
        {
            dialogCanvas.transform.Find("PreQuestText").gameObject.SetActive(true);
        }else if (currentlyHasQuest && !completedQuest)
        {
            dialogCanvas.transform.Find("QuestText").gameObject.SetActive(true);
        }
        else if (!currentlyHasQuest && completedQuest)
        {
            dialogCanvas.transform.Find("PostQuestText").gameObject.SetActive(true);
        }
    }

    void clearCanvas()
    {
        dialogCanvas.transform.Find("PreQuestText").gameObject.SetActive(false);
        dialogCanvas.transform.Find("QuestText").gameObject.SetActive(false);
        dialogCanvas.transform.Find("PostQuestText").gameObject.SetActive(false);
    }

    void completeQuest1()
    {
        currentlyHasQuest = false;
        currentlyHasQuestSprite.SetActive(false);
        completedQuest = true;
    }
}