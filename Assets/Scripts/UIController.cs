using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] MainManager mainManager;
    [SerializeField] Button allAtOnceButton;
    [SerializeField] Button oneByOneButton;
    [SerializeField] Button whenReadyButton;

    private void Start()
    {
        allAtOnceButton.onClick.AddListener(OnAllAtOnceButoonPresed);
        oneByOneButton.onClick.AddListener(OnOneByOneButtonPressed);
        whenReadyButton.onClick.AddListener(OnWhenReadyButtonPressed);
    }

    private void OnDestroy()
    {
        allAtOnceButton.onClick.RemoveListener(OnAllAtOnceButoonPresed);
        oneByOneButton.onClick.RemoveListener(OnOneByOneButtonPressed);
        whenReadyButton.onClick.RemoveListener(OnWhenReadyButtonPressed);
    }

    public void EnableButtons()
    {
        allAtOnceButton.interactable = true;
        oneByOneButton.interactable = true;
        whenReadyButton.interactable = true;
    }

    public void DisableButtons()
    {
        allAtOnceButton.interactable = false;
        oneByOneButton.interactable = false;
        whenReadyButton.interactable = false;
    }
    
    private void OnAllAtOnceButoonPresed()
    {
        mainManager.ShowAllAtOnce();
    }

    private void OnWhenReadyButtonPressed()
    {
        mainManager.ShowWhenReady();
    }

    private void OnOneByOneButtonPressed()
    {
        mainManager.ShowOneByOne();
    }
}
