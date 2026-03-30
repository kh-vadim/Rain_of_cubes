using System.Collections;
using UnityEngine;

public class SpawnController : MonoBehaviour
{
    [SerializeField] private CubesCreator _spawner;
    [SerializeField] private float _repeatRate = 1f;

    void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        while (true)
        {
            _spawner.SpawnCube();

            yield return new WaitForSeconds(_repeatRate);
        }
    }
}
