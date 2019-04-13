using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

//  This script will be updated in Part 2 of this 2 part series.
public class ModalPanel : MonoBehaviour
{
    public Text question;
    public Button yesButton;
    public Button noButton;
    public Button cancelButton;
    public GameObject modalPanelObject;

    private static ModalPanel modalPanel;

    public static ModalPanel Instance()
    {
        if (!modalPanel)
        {
            modalPanel = FindObjectOfType(typeof(ModalPanel)) as ModalPanel;
            if (!modalPanel)
                Debug.LogError("There needs to be one active ModalPanel script on a GameObject in your scene.");
        }

        return modalPanel;
    }

    // Yes/No/Cancel: A string, a Yes event, a No event and Cancel event
    public void Choice(string question, UnityAction yesEvent, UnityAction noEvent = default(UnityAction),
        UnityAction cancelEvent = default(UnityAction))
    {
        yesButton.gameObject.SetActive(false);
        noButton.gameObject.SetActive(false);
        cancelButton.gameObject.SetActive(false);
        
        modalPanelObject.SetActive(true);

        yesButton.onClick.RemoveAllListeners();
        yesButton.onClick.AddListener(yesEvent);
        yesButton.onClick.AddListener(ClosePanel);
        if (noEvent != default(UnityAction))
        {
            noButton.onClick.RemoveAllListeners();
            noButton.onClick.AddListener(noEvent);
            noButton.onClick.AddListener(ClosePanel);
            noButton.gameObject.SetActive(true);
        }
        if (cancelEvent != default(UnityAction))
        {
            cancelButton.onClick.RemoveAllListeners();
            cancelButton.onClick.AddListener(cancelEvent);
            cancelButton.onClick.AddListener(ClosePanel);
            cancelButton.gameObject.SetActive(true);
        }

        this.question.text = question;

        yesButton.gameObject.SetActive(true);
    }

    void ClosePanel()
    {
        modalPanelObject.SetActive(false);
    }
}