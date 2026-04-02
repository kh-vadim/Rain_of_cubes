using UnityEngine;
using System.Collections;
using System;

public class Cube : MonoBehaviour
{
    [SerializeField] private float _minLifeTime = 2f;
    [SerializeField] private float _maxLifeTime = 5f;

    private Rigidbody _rigidbody;
    private bool _hasTouch = false;
    private CubeColorChanger _colorChanger;

    public event Action<Cube> LifeTimeEnded;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _colorChanger = GetComponent<CubeColorChanger>();
    }

    public void Init(Vector3 spawnPosition)
    {
        _hasTouch = false;

        transform.position = spawnPosition;

        _rigidbody.velocity = Vector3.zero;
        _rigidbody.angularVelocity = Vector3.zero;

        _colorChanger.ResetToDefaultColor();
    }
    
    private void OnCollisionEnter(Collision collision)
    {
        if (_hasTouch == false && collision.gameObject.TryGetComponent(out Platform _))
        {
            _hasTouch = true;
            _colorChanger.SetRandomColor();
            float lifeTime = UnityEngine.Random.Range(_minLifeTime, _maxLifeTime);
            StartCoroutine(LifeTimerCoroutine(lifeTime));
        }
    }

    private IEnumerator LifeTimerCoroutine(float lifeTime)
    {
        yield return new WaitForSeconds(lifeTime);
        LifeTimeEnded?.Invoke(this);
    }
}
