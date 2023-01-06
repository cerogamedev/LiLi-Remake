using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueSystem : MonoBehaviour
{
    [SerializeField] GameObject character;
    // ui stuff

    [SerializeField] int triggersS;
    [SerializeField] GameObject picture;
    [SerializeField] GameObject chBg;
    [SerializeField] GameObject textBg;
    [SerializeField] GameObject SButton;
    [SerializeField] Text pressS;

    //dialogue system stuff

    [SerializeField] int closeDialogeSystem = 0;
    [SerializeField] Text text;
    [SerializeField] List<string> lines;

    [SerializeField] [Range(0f, 1f)] float visibleTextPercent;

    [SerializeField] float timePerLetter = 0.04f;
    float totalTimeToType, currentTime;

    string lineToShow;

    void Start()
    {
        CycleLine();
        Ignore();
    }

    void Update()
    {
        TypeOutText();
        TriggerS();
        closeDialogueSystem();
    }
    void TriggerS()
    {
        if (triggersS == 1)
        {
            if (Input.GetKeyDown("s"))
            {
                Publish();
                PushText();
            }
        }
    }
    void UpdateText()
    {
        int totalLetterToShow = (int)(lineToShow.Length * visibleTextPercent);
        text.text = lineToShow.Substring(0, totalLetterToShow);
    }

    public void PushText()
    {
        if (visibleTextPercent < 1f)
        {
            visibleTextPercent = 1f;
            UpdateText();
            return;
        }
        CycleLine();
    }

    private void TypeOutText()
    {
        if (visibleTextPercent >= 1f)
        {
            return;
        }
        currentTime += Time.deltaTime;
        visibleTextPercent = currentTime / totalTimeToType;
        visibleTextPercent = Mathf.Clamp(visibleTextPercent, 0f, 1f);
        UpdateText();
    }
    private void CycleLine()
    {
        if (lines.Count == 0)
        {
            Debug.Log("There is nothing here");
            Ignore();
            closeDialogeSystem += 1;
            return;
        }
        lineToShow = lines[0];
        lines.RemoveAt(0);

        totalTimeToType = lineToShow.Length * timePerLetter;
        currentTime = 0f;
        visibleTextPercent = 0f;

        //clear the text object
        text.text = "";
    }

    // ui code and ui activated code

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SButton.SetActive(true);
            triggersS = 1;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        Ignore();
    }
    public void Ignore()
    {
        picture.SetActive(false);
        chBg.SetActive(false);
        textBg.SetActive(false);
        pressS.text = "";
        SButton.SetActive(false);

        triggersS = 0;
        text.text = "";
    }
    void Publish()
    {
        picture.SetActive(true);
        chBg.SetActive(true);
        textBg.SetActive(true);
        pressS.text = "Press 's'";
    }

    void closeDialogueSystem()
    {
        if (closeDialogeSystem == 3)
        {
            Ignore();
            character.SetActive(false);
        }
    }
}
