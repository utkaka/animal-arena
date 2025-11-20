using AnimalArena.Fx.Core;
using UnityEngine;

namespace AnimalArena.Fx.Data
{
    [CreateAssetMenu(fileName = "EffectsConfig", menuName = "AnimalArena/Effects Config", order = 0)]
    public class ScriptableEffectsConfig : ScriptableObject, IEffectsConfig
    {
        [SerializeField]
        private GameObject _effectsCanvasPrefab;
        [SerializeField]
        private GameObject _ateEffectPrefab;

        public GameObject CanvasPrefab => _effectsCanvasPrefab;
        public GameObject AteEffectPrefab => _ateEffectPrefab;
    }
}