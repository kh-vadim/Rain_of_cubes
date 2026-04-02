using UnityEngine;
using UnityEngine.Pool;

public class CubePool : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Transform _minPosition;
    [SerializeField] private Transform _maxPosition;

    private int _poolCapacity = 20;
    private int _poolMaxSize = 20;

    private ObjectPool<Cube> _pool;
    
    private void Awake()
    {
        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_cubePrefab),
            actionOnGet: PrepareCube,
            actionOnRelease: HideCube,
            actionOnDestroy: (cube) => Destroy(cube.gameObject),
            collectionCheck: false,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    public void SpawnCube()
    {
        if (_pool.CountAll < _poolMaxSize || _pool.CountInactive > 0)
            _pool.Get();
    }

    private void PrepareCube(Cube cube)
    {
        Vector3 randomPosition = new Vector3 (
            Random.Range(_minPosition.position.x, _maxPosition.position.x),
            _minPosition.position.y,
            Random.Range(_minPosition.position.z, _maxPosition.position.z)
        );

        cube.Init(randomPosition);

        cube.LifeTimeEnded += ReturnCubeToPool;

        cube.gameObject.SetActive(true);
    }

    private void HideCube(Cube cube)
    {
        cube.LifeTimeEnded -= ReturnCubeToPool;

        cube.gameObject.SetActive(false);
    }

    private void ReturnCubeToPool(Cube returnedCube)
    {
        _pool.Release(returnedCube);
    }
}
