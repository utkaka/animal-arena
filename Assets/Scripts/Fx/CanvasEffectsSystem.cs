using AnimalArena.Animals;
using AnimalArena.Assets;
using UnityEngine;
using UnityEngine.Assertions;
using VContainer.Unity;

namespace AnimalArena.Fx
{
    public class CanvasEffectsSystem : IInitializable
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
            _animalsController.AnimalEaten += OnAnimalEaten;
        }
        
        public void Initialize()
        {
            SetupCanvas();
        }

        private void SetupCanvas()
        {
            GameObject canvasObject = _assetProvider.Instantiate(_effectsConfig.CanvasPrefab);
            _canvas = canvasObject.GetComponent<Canvas>();
            Assert.IsNotNull(_canvas);
            PrewarmEffects();
        }

        private void PrewarmEffects()
        {
            GameObject animalEatenEffect = _assetProvider.Instantiate(_effectsConfig.AteEffectPrefab);
            _assetProvider.Release(animalEatenEffect);
        }

        private void OnAnimalEaten(Animal animal)
        {
            GameObject animalEatenEffect = _assetProvider.Instantiate(_effectsConfig.AteEffectPrefab, animal.transform.position);
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