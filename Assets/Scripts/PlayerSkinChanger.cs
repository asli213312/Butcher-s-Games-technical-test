using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSkinChanger : MonoBehaviour
{
    [SerializeField] private SliderBehaviour sliderBehaviour;
	[SerializeField] private SkinnedMeshRenderer[] stateMeshes;
    [SerializeField] private SkinnedMeshRenderer startMesh;

    private SkinnedMeshRenderer _currentMesh;

    private void Awake() 
    {
        sliderBehaviour.StateChanged += OnNewState;
    }

    private void Start() 
    {
        _currentMesh = startMesh;
    }

    private void OnDestroy() 
    {
        sliderBehaviour.StateChanged -= OnNewState;
    }

    private void OnNewState(SliderBehaviourState state) 
    {
        foreach (var mesh in stateMeshes) 
        {
            if (state.stateMesh != mesh.sharedMesh) continue;

            _currentMesh.gameObject.SetActive(false);

            _currentMesh.sharedMesh = mesh.sharedMesh;

            _currentMesh.gameObject.SetActive(true);
        }
    }
}
