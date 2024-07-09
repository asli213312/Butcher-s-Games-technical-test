using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public abstract class AbstractCounter : MonoBehaviour
{
	[SerializeField] protected TextMeshProUGUI counterText;

	protected event Action<int> CountChanged;
	protected int Counter;

	private void Start() 
	{
		CountChanged += UpdateText;
	}

	private void OnDestroy() 
	{	
		CountChanged -= UpdateText;
	}

	protected void InvokeCount() 
	{
		Counter++;
		CountChanged?.Invoke(Counter);
	}

	protected virtual bool NeedIncrease() => true;

	private void UpdateText(int count) 
	{
		counterText.text = count.ToString();
	}
}
