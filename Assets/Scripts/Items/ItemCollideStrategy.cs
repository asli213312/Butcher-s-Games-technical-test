using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public abstract class ItemCollideStrategy : MonoBehaviour
{
	[Zenject.Inject] private PlayerController _playerController;
	[Zenject.Inject] private ItemController _itemController;
	[Zenject.Inject] protected SliderBehaviour SliderBehaviour;

	[SerializeField, SerializeReference] private AbstractItem item;
	[SerializeField] protected RectTransform ItemValuePoint;
	[SerializeField] protected TextMeshProUGUI ItemValueText;

	[Header("Options")]
	[SerializeField] protected float itemCheckTime;
	[SerializeField] private float textValueFlyTime;
	[SerializeField] private float textValueFadeTime;
	[SerializeField] private Vector3 textOffset;

	public ICollidableItem Item { get => ItemType; }

	protected Action<int> ItemValueChanged;

	protected abstract ICollidableItem ItemType { get; set; }
	protected abstract string ItemValuePattern { get; }
	protected AbstractItem ItemBase { get; set; }
	protected int ItemValue 
	{
		get => _itemValue;
		set
		{
			if (value == 0)
			{
				_itemValue = 0;
			}
			else
			{
				_itemValue += ItemBase.Data.itemValue;
			}
		}
	}
	protected int _itemValue;
	private SimpleTimer _timer;

	private Vector3 _textValueInitialPosition;

	private void Start() 
	{
		ItemType = item;
		ItemBase = item;

		_timer = new SimpleTimer(itemCheckTime, () => OnTimerComplete());
		_textValueInitialPosition = ItemValueText.rectTransform.anchoredPosition;

		ItemValueChanged = OnItemValueChanged;

		_itemController.ItemCollideAction += HandleCheckoutItem;
	}

	private void OnDestroy() 
	{
		_itemController.ItemCollideAction -= HandleCheckoutItem;
	}

	public void Handle() 
	{
		Invoke();
	}

	protected abstract void Invoke();

	private void OnItemValueChanged(int value) 
	{
		ItemValueText.text = ItemValuePattern + " " + ItemValue.ToString() + " " + "$";
	}

	private void HandleCheckoutItem(ICollidableItem item) 
	{
		if (ItemType.GetType() != item.GetType()) return;

		ItemValueText.transform.position = textOffset;
		CreateItemTextValue(item);

		if (_timer.IsStarted == false) 
		{
			_timer.Start();
			//CreateItemTextValue(item);
		}
		else if (_timer.IsCompleted == false) 
		{
			_timer.Stop();
			_timer.Start();
			CreateItemTextValue(item);
		}
	}

	private void OnTimerComplete() 
	{
		ItemValueText.Fade(0f, textValueFadeTime);
	}

	private void CreateItemTextValue(ICollidableItem item) 
	{
		ItemValue++;
		ItemValueChanged?.Invoke(ItemValue);

		ItemValueText.Fade(1f, textValueFadeTime, () =>
		{
			this.WaitForSeconds(textValueFlyTime, () =>
			{
				_itemValue = 0;
				OnTimerComplete();
			});
		});

		ItemValueText.rectTransform.MoveTo(ItemValuePoint, textValueFlyTime);
	}
}
