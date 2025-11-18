using AnimalArena.Assets;

namespace AnimalArena.Animals
{
    public class AnimalsController : IAnimalsController
    {
        private IAssetProvider _assetProvider;

        public AnimalsController(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }
        
        public void OnSpawned(Animal animal)
        {
            
        }

        public void Kill(Animal animal)
        {
            if (animal.IsDead) return;
            animal.MarkAsDead();
            _assetProvider.Release(animal.gameObject);
        }
    }
}