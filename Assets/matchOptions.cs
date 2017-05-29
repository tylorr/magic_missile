using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class matchOptions : MonoBehaviour {

	public GameObject matchOptionsScreen;

	private UIActions _uiActions;


	private void Awake()
	{
		_uiActions = new UIActions();
	}

	private void Update()
	{
		if (_uiActions.Start.WasPressed)
		{
			matchOptionsScreen.SetActive(true);
			gameObject.SetActive(false);
		}
	}
}
