using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SliderBehaviourData : ScriptableObject
{
	[SerializeField] public float initialValue;
    [SerializeField] public SliderBehaviourState[] states;
}

[Serializable]
public class SliderBehaviourState 
{
	public Sprite fillSprite;
	public Mesh stateMesh;
	public Vector2 stateRange;
	public int stepValue;
	public string stateText;
}
