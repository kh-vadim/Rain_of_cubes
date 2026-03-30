using UnityEngine;
using UnityEngine.Pool;
using System.Collections;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _minLifeTime = 2f;
    [SerializeField] private float _maxLifeTime = 5f;

    private Rigidbody _rigidbody;
    private Renderer _renderer;
    private bool _hasTouch = false;

    private ObjectPool<Cube> _pool;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _renderer = GetComponent<Renderer>();
    }

    public void Init(ObjectPool<Cube> pool, Vector3 spawnPosition, Color defaultColor)
    {
        _pool = pool;
        _hasTouch = false;

        transform.position = spawnPosition;

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        _renderer.material.color = defaultColor;
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (_hasTouch == false && collision.gameObject.CompareTag("Platform"))
        {
            _hasTouch = true;

            _renderer.material.color = Random.ColorHSV();
            
            float lifeTime = Random.Range(_minLifeTime, _maxLifeTime);

            StartCoroutine(LifeTimerCoroutine(lifeTime));
        }
    }

    private IEnumerator LifeTimerCoroutine(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);

        if (_pool != null)
            _pool.Release(this);
    }
}
