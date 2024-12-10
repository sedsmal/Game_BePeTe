using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BizzyBeeGames;
using UnityEngine.UI;

public class StudentMarkManager : SingletonComponent<StudentMarkManager>
{
    public ScrollRect scrollRect;
    public GameObject countPanel,timePanel,gradePanel, compareOthersPanel;
    public GameObject countText, timeText, gradeText, compareOthersText;
    public GameObject countButton, timeButton, gradeButton,compareOthersButton;

    private float defaultY;

    private void Start()
    {
        defaultY = countButton.transform.position.y;
    }
    public void Initialize()
    {
        scrollRect.content = gradePanel.GetComponent<RectTransform>();
        scrollRect.horizontalScrollbar.value = 1f;

        gradePanel.SetActive(true);
        timePanel.SetActive(false);
        countPanel.SetActive(false);
        compareOthersPanel.SetActive(false);

        gradeText.SetActive(true);
        timeText.SetActive(false);
        countText.SetActive(false);
        compareOthersText.SetActive(false);

        gradeButton.transform.position = new Vector3(gradeButton.transform.position.x, defaultY+18, gradeButton.transform.position.z);
        timeButton.transform.position =  new Vector3(timeButton.transform.position.x, defaultY, timeButton.transform.position.z);
        countButton.transform.position = new Vector3(countButton.transform.position.x, defaultY, countButton.transform.position.z);
        compareOthersButton.transform.position = new Vector3(compareOthersButton.transform.position.x, defaultY, compareOthersButton.transform.position.z);
    }

    public void ActiveGrade()
    {
        scrollRect.content = gradePanel.GetComponent<RectTransform>();
        scrollRect.horizontalScrollbar.value = 1f;

        gradePanel.SetActive(true);
        timePanel.SetActive(false);
        countPanel.SetActive(false);
        compareOthersPanel.SetActive(false);

        gradeText.SetActive(true);
        timeText.SetActive(false);
        countText.SetActive(false);
        compareOthersText.SetActive(false);

        gradeButton.transform.position = new Vector3(gradeButton.transform.position.x, defaultY+18, gradeButton.transform.position.z);
        timeButton.transform.position = new Vector3(timeButton.transform.position.x, defaultY, timeButton.transform.position.z);
        countButton.transform.position = new Vector3(countButton.transform.position.x, defaultY, countButton.transform.position.z);
        compareOthersButton.transform.position = new Vector3(compareOthersButton.transform.position.x, defaultY, compareOthersButton.transform.position.z);
    }

    public void ActiveTime()
    {
        scrollRect.content = timePanel.GetComponent<RectTransform>();
        scrollRect.horizontalScrollbar.value = 1f;

        gradePanel.SetActive(false);
        timePanel.SetActive(true);
        countPanel.SetActive(false);
        compareOthersPanel.SetActive(false);

        gradeText.SetActive(false);
        timeText.SetActive(true);
        countText.SetActive(false);
        compareOthersText.SetActive(false);

        gradeButton.transform.position = new Vector3(gradeButton.transform.position.x, defaultY, gradeButton.transform.position.z);
        timeButton.transform.position = new Vector3(timeButton.transform.position.x, defaultY+18, timeButton.transform.position.z);
        countButton.transform.position = new Vector3(countButton.transform.position.x, defaultY, countButton.transform.position.z);
        compareOthersButton.transform.position = new Vector3(compareOthersButton.transform.position.x, defaultY, compareOthersButton.transform.position.z);
    }

    public void ActiveCount()
    {
        scrollRect.content = countPanel.GetComponent<RectTransform>();
        scrollRect.horizontalScrollbar.value = 1f;

        gradePanel.SetActive(false);
        timePanel.SetActive(false);
        countPanel.SetActive(true);
        compareOthersPanel.SetActive(false);

        gradeText.SetActive(false);
        timeText.SetActive(false);
        countText.SetActive(true);
        compareOthersText.SetActive(false);

        gradeButton.transform.position = new Vector3(gradeButton.transform.position.x, defaultY, gradeButton.transform.position.z);
        timeButton.transform.position = new Vector3(timeButton.transform.position.x, defaultY, timeButton.transform.position.z);
        countButton.transform.position = new Vector3(countButton.transform.position.x, defaultY+18, countButton.transform.position.z);
        compareOthersButton.transform.position = new Vector3(compareOthersButton.transform.position.x, defaultY, compareOthersButton.transform.position.z);
    }

    public void CompareOthers()
    {
        scrollRect.content = compareOthersPanel.GetComponent<RectTransform>();
        scrollRect.horizontalScrollbar.value = 1f;

        gradePanel.SetActive(false);
        timePanel.SetActive(false);
        countPanel.SetActive(false);
        compareOthersPanel.SetActive(true);

        gradeText.SetActive(false);
        timeText.SetActive(false);
        countText.SetActive(false);
        compareOthersText.SetActive(true);

        gradeButton.transform.position = new Vector3(gradeButton.transform.position.x, defaultY, gradeButton.transform.position.z);
        timeButton.transform.position = new Vector3(timeButton.transform.position.x, defaultY, timeButton.transform.position.z);
        countButton.transform.position = new Vector3(countButton.transform.position.x, defaultY, countButton.transform.position.z);
        compareOthersButton.transform.position = new Vector3(compareOthersButton.transform.position.x, defaultY+18, compareOthersButton.transform.position.z);
    }
}
