using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    public GameObject playerSelectScreen;

    private UIActions _uiActions;


    private void Awake()
    {
        _uiActions = new UIActions();
    }

    private void Update()
    {
        if (_uiActions.Start.WasPressed)
        {
            playerSelectScreen.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
