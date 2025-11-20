using System;
using System.Collections;
using UnityEngine;

namespace AnimalArena.Fx.Components
{
    public class TastyEffect : MonoBehaviour, IEffect
    {
        public event Action<IEffect> Complete;

        public GameObject GameObject => gameObject;

        public void Play()
        {
            StartCoroutine(HideCoroutine());
        }

        private IEnumerator HideCoroutine()
        {
            yield return new WaitForSeconds(1.0f);
            Complete?.Invoke(this);
        }
    }
}