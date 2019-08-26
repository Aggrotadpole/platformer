using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(CanvasGroup))]
public class Dialog : MonoBehaviour
{

    public Text title;
    public Text message;
    public Text accept, decline;
    public Button acceptButton, declineButton;

    private CanvasGroup cg;

    void Awake()
    {
        cg = GetComponent<CanvasGroup>();
    }

    public Dialog OnAccept(string text, UnityAction action)
    {
        accept.text = text;
        acceptButton.onClick.RemoveAllListeners();
        acceptButton.onClick.AddListener(action);
        return this;
    }



    public Dialog OnDecline(string text, UnityAction action)
    {
        decline.text = text;
        declineButton.onClick.RemoveAllListeners();
        declineButton.onClick.AddListener(action);
        return this;
    }

    public Dialog Title(string title)
    {
        this.title.text = title;
        return this;
    }

    public Dialog Message(string message)
    {
        this.message.text = message;
        return this;
    }

    // show the dialog, set it's canvasGroup.alpha to 1f or tween like here
    public void Show()
    {
        this.transform.SetAsLastSibling();
        cg.interactable = true;
        cg.alpha = 1f;
        cg.blocksRaycasts = true;
    }

    public void Hide()
    {
        
        cg.interactable = false;
        cg.alpha = 0f;
        cg.blocksRaycasts = false;
    }

    private static Dialog instance;
    public static Dialog Instance()
    {
        if (!instance)
        {
            instance = FindObjectOfType(typeof(Dialog)) as Dialog;
            if (!instance)
                Debug.Log("There need to be at least one active Dialog on the scene");
        }

        return instance;
    }

}