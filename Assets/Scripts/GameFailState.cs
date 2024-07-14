using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameFailState : MonoBehaviour
{
	[SerializeField] private Button retryButton;
	[SerializeField] private Image failScreen;
	[SerializeField] private RectTransform failHeader;

	[Header("Options")]
	[SerializeField] private float animDuration;
	[SerializeField] private float fadeScreenValue;

	private Vector3 _retryBtnStart;
	private Vector3 _retryBtnEnd;
	private Vector3 _failHeaderStart; 
	private Vector3 _failHeaderEnd; 

	private void Start() 
	{
		_retryBtnStart = retryButton.GetComponent<RectTransform>().anchoredPosition;
		_failHeaderStart = failHeader.GetComponent<RectTransform>().anchoredPosition;

		_retryBtnEnd = retryButton.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition;
		_failHeaderEnd = failHeader.transform.GetChild(0).GetComponent<RectTransform>().anchoredPosition;
		
		retryButton.onClick.AddListener(OnClickRetryButton);
	}

	private void OnDestroy() 
	{
		retryButton.onClick.RemoveListener(OnClickRetryButton);
	}

	public void Handle() 
	{
		failScreen.Fade(fadeScreenValue, animDuration);
		retryButton.transform.EaseOutBounce(_retryBtnEnd, animDuration);
		failHeader.transform.EaseOutBounce(_failHeaderEnd, animDuration);

	}

	public void Reset(Action onComplete) 
	{
		failScreen.Fade(0f, animDuration);
		retryButton.transform.EaseOutBounce(_retryBtnStart, animDuration);
		failHeader.transform.EaseOutBounce(_failHeaderStart, animDuration);	 

		onComplete.Invoke();
	}

	private void OnClickRetryButton() 
	{
		Reset(() => this.InlineWaitForSeconds(0.5f, () => SceneManager.LoadScene(SceneManager.GetActiveScene().name)));
	}
}
