using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIController : MonoBehaviour
{
    [SerializeField] MainManager _mainManager;
    [SerializeField] Button _button;

    private void Start()
    {
        _button.onClick.AddListener(OnDownloadButtonPressed);
    }

    private void OnDestroy()
    {
        _button.onClick.RemoveListener(OnDownloadButtonPressed);
    }
    public void OnDownloadButtonPressed()
    {
        Debug.Log("ButtonPressed");
        _mainManager.ShowCard();
    }
}
