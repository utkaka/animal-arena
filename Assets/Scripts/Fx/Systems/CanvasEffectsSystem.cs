using System;
using AnimalArena.Animals.Core;
using AnimalArena.Assets.Core;
using AnimalArena.Fx.Core;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer.Unity;
using Object = UnityEngine.Object;

namespace AnimalArena.Fx.Systems
{
    //FX on Canvas is a bad idea, but, probably, the easiest to implement quickly
    public class CanvasEffectsSystem : IInitializable, IDisposable
    {
        private readonly IEffectsConfig _effectsConfig;
        private readonly IAssetProvider _assetProvider;
        private readonly IAnimalsController _animalsController;
        private Canvas _canvas;
        
        public CanvasEffectsSystem(IEffectsConfig effectsConfig, IAssetProvider assetProvider, IAnimalsController animalsController)
        {
            _effectsConfig = effectsConfig;
            _assetProvider = assetProvider;
            _animalsController =  animalsController;
        }
        
        public void Initialize()
        {
            _animalsController.AnimalEaten += OnAnimalEaten;
            SetupCanvas();
        }
        
        public void Dispose()
        {
            _animalsController.AnimalEaten -= OnAnimalEaten;
        }

        private void SetupCanvas()
        {
            GameObject canvasObject = Object.Instantiate(_effectsConfig.CanvasPrefab);
            _canvas = canvasObject.GetComponent<Canvas>();
            Assert.IsNotNull(_canvas);
            PrewarmEffects();
        }

        private void PrewarmEffects()
        {
            GameObject animalEatenEffect = _assetProvider.Instantiate(_effectsConfig.AteEffectPrefab);
            _assetProvider.Release(animalEatenEffect);
        }

        private void OnAnimalEaten(IAnimal animal)
        {
            GameObject animalEatenEffect = _assetProvider.Instantiate(_effectsConfig.AteEffectPrefab, animal.GameObject.transform.position);
            IEffect effect =  animalEatenEffect.GetComponent<IEffect>();
            Assert.IsNotNull(effect);
            effect.Complete += OnEffectComplete;
            Transform effectTransform = animalEatenEffect.transform;
            effectTransform.SetParent(_canvas.transform);
            effect.Play();
        }

        private void OnEffectComplete(IEffect effect)
        {
            effect.Complete -= OnEffectComplete;
            _assetProvider.Release(effect.GameObject);
        }
    }
}