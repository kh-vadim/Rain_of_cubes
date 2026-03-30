using UnityEngine;
using UnityEngine.Pool;

public class CubesCreator : MonoBehaviour
{
    [SerializeField] private Cube _cubePrefab;
    [SerializeField] private Transform _minPosition;
    [SerializeField] private Transform _maxPosition;

    private int _poolCapacity = 20;
    private int _poolMaxSize = 20;

    private ObjectPool<Cube> _pool;

    private Color _defaultColor;

    private float _repeatRate = 1f;
    
    private void Awake()
    {
        _defaultColor = _cubePrefab.GetComponent<Renderer>().sharedMaterial.color;

        _pool = new ObjectPool<Cube>(
            createFunc: () => Instantiate(_cubePrefab),
            actionOnGet: (cube) => ActionOnGet(cube),

            actionOnRelease: (cube) => cube.gameObject.SetActive(false),
            actionOnDestroy: (cube) => Destroy(cube.gameObject),
            collectionCheck: false,
            defaultCapacity: _poolCapacity,
            maxSize: _poolMaxSize);
    }

    private void Start()
    {
        InvokeRepeating(nameof(GetCube), 0.0f, _repeatRate);
    }

    private void GetCube()
    {
        if (_pool.CountActive < _poolMaxSize)
            _pool.Get();
    }

    private void ActionOnGet(Cube cube)
    {
        Vector3 randomPosition = new Vector3 (
            Random.Range(_minPosition.position.x, _maxPosition.position.x),
            _minPosition.position.y,
            Random.Range(_minPosition.position.z, _maxPosition.position.z)
        );

        cube.Init(_pool, randomPosition, _defaultColor);

        cube.gameObject.SetActive(true);
    }
}
