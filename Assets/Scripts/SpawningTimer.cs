using System.Collections;
using UnityEngine;

public class SpawningTimer : MonoBehaviour
{
    [SerializeField] private CubePool _spawner;
    [SerializeField] private float _repeatRate = 1f;

    private void Start()
    {
        StartCoroutine(SpawnRoutine());
    }

    private IEnumerator SpawnRoutine()
    {
        WaitForSeconds wait = new WaitForSeconds(_repeatRate);

        while (enabled)
        {
            _spawner.SpawnCube();
            yield return wait;
        }
    }
}
