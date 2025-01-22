using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GasSpawner : MonoBehaviour
{
    [SerializeField] private GameObject _gasPrefab;
    [SerializeField] private List<Transform> _spawnPoints;
    [SerializeField] private float _spawnRate = 2.8f;

    public IEnumerator SpawnGas()
    {
        while (!GameManager.Instance.IsDead)
        {           
            yield return new WaitForSeconds(_spawnRate);
            int randomIndex = Random.Range(0, _spawnPoints.Count);
            Instantiate(_gasPrefab, _spawnPoints[randomIndex].position, Quaternion.identity);
        }
    }
}
