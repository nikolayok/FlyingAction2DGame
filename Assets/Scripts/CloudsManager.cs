using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudsManager : MonoBehaviour
{
    [SerializeField] private GameObject[] _clouds;
    private float _secondsForNextObstacle = 1;
    //private float currentSeconds = 0;
    private GameObject _player;
    private Transform _playerTransform;

    private bool isSpawning = false;

    private void Start()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _playerTransform = _player.transform;

    }

    public void StartSpawnObstacles()
    {
        if (!isSpawning)
        {
            Invoke("SpawnObstacle", 0);
            isSpawning = true;
        }
    }

    public void StopSpawnObstacles()
    {
        CancelInvoke();
        isSpawning = false;
    }

    private void SpawnObstacle()
    {
        int randomYPosition = Random.Range(-3, 2);
        int randomObstacle = Random.Range(0, _clouds.Length);

        float xPosition = _playerTransform.position.x + 10;

        Instantiate(_clouds[randomObstacle], new Vector3(xPosition, randomYPosition, 0), Quaternion.identity, transform);

        _secondsForNextObstacle = Random.Range(10, 15) / 10;
        Invoke("SpawnObstacle", _secondsForNextObstacle);
    }

    public void DestroyClouds()
    {
        GameObject[] allClouds = GameObject.FindGameObjectsWithTag("Cloud");

        foreach (GameObject cloud in allClouds)
        {
            Destroy(cloud);
        }
    }

}
