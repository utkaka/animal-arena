using UnityEngine;

public class AnimalSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _preyPrefab;

    [SerializeField]
    private GameObject _predatorPrefab;

    [SerializeField]
    private float _minInterval = 1f;

    [SerializeField]
    private float _maxInterval = 2f;

    [SerializeField]
    [Range(0f, 1f)]
    private float _predatorChance = 0.35f;

    float nextSpawnTime;

    private void Start()
    {
        ScheduleNext();
    }

    private void Update()
    {
        if (Time.time >= nextSpawnTime)
        {
            SpawnAnimal();
            ScheduleNext();
        }
    }

    private void ScheduleNext()
    {
        nextSpawnTime = Time.time + Random.Range(_minInterval, _maxInterval);
    }

    private void SpawnAnimal()
    {
        var cam = Camera.main;
        if (!cam)
            return;

        Vector3 vp = new Vector3(Random.value, Random.value, 10f);
        Vector3 pos = cam.ViewportToWorldPoint(vp);
        pos.y = 0f;

        var prefab = (Random.value < _predatorChance) ? _predatorPrefab : _preyPrefab;
        var a = Instantiate(prefab, pos, Quaternion.identity);

        var rb = a.GetComponent<Rigidbody>();
        if (rb)
            rb.AddForce(
                new Vector3(Random.Range(-3f, 3f), 0f, Random.Range(-3f, 3f)),
                ForceMode.VelocityChange
            );
    }
}
