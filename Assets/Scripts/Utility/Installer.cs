using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class Installer : MonoInstaller
{
	[SerializeField] private ItemController itemController;
	[SerializeField] private PlayerController playerController;
	[SerializeField] private SliderBehaviour sliderBehaviour;
	[SerializeField] private GameStateHandler gameStateHandler;
	[SerializeField] private TickController tickController;
	[SerializeField] private SoundHandler soundHandler;

    public override void InstallBindings() 
    {
    	Container.Bind<ItemController>().FromInstance(itemController).AsSingle();
    	Container.Bind<PlayerController>().FromInstance(playerController).AsSingle();
    	Container.Bind<SliderBehaviour>().FromInstance(sliderBehaviour).AsSingle();
		Container.Bind<TickController>().FromInstance(tickController).AsSingle();
		Container.Bind<SoundHandler>().FromInstance(soundHandler).AsSingle();
    	Container.BindInterfacesAndSelfTo<GameStateHandler>().FromInstance(gameStateHandler).AsSingle();
    }
}
