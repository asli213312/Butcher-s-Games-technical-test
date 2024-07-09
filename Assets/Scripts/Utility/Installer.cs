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
	[SerializeField] private MoneyCounter moneyCounter;

    public override void InstallBindings() 
    {
    	Container.Bind<ItemController>().FromInstance(itemController).AsSingle();
    	Container.Bind<PlayerController>().FromInstance(playerController).AsSingle();
    	Container.Bind<SliderBehaviour>().FromInstance(sliderBehaviour).AsSingle();
    	Container.Bind<MoneyCounter>().FromInstance(moneyCounter).AsSingle();
    }
}
