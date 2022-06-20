using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BomberSpawner : MonoBehaviour
{
    private BoxCollider _collider;
    [SerializeField]
    private GameObject _bomber;
    [SerializeField]
    private int _amountToSpawnPerWave;
    [SerializeField]
    private int _amountOfWaves;
    [SerializeField]
    private float _timeBetweenWaves;
    [SerializeField]
    private BoxCollider _bomberTargetCollider;

    // Start is called before the first frame update
    void Start()
    {
        _collider = GetComponent<BoxCollider>();

        StartCoroutine(SpawnUnits());
    }

    private IEnumerator SpawnUnits()
    {
        for (int i = 1; i <= _amountOfWaves; i++)
        {
            Debug.Log($"Starting wave {i}, spawning {_amountToSpawnPerWave} units...");

            for (int a = 0; a < _amountToSpawnPerWave; a++)
            {
                // Find random position inside collider
                Vector3 randomPosInCollider = GetRandomPosOnCollider(_collider);

                // Shoot ray down to find grind position
                randomPosInCollider.y = GetGroundLevelFromPoint(randomPosInCollider);

                // Spawn bomber on ground position
                Bomber bomber = Instantiate(_bomber, randomPosInCollider, _bomber.transform.rotation).GetComponent<Bomber>();

                // Set bomber target somewhere on the castle
                bomber.SetTarget(GetRandomPosOnCollider(_bomberTargetCollider), _bomberTargetCollider.transform.GetComponent<Entity>());
                // We don't really care about the ground level here because the bomber is already spawned
            }

            Debug.Log($"Wave done spawning, waiting for {_timeBetweenWaves} seconds until next wave");
            yield return new WaitForSeconds(_timeBetweenWaves);
        }
    }

    private float GetGroundLevelFromPoint(Vector3 point)
    {
        RaycastHit hit;
        if (Physics.Raycast(point, Vector3.down, out hit))
        {
            return hit.collider.transform.position.y;
        }

        Debug.Log("Ground level not found, returning 0");
        return 0;
    }

    private Vector3 GetRandomPosOnCollider(BoxCollider collider)
    {
        Vector3 extents = collider.size / 2f;
        Vector3 point = new Vector3(
            Random.Range(-extents.x, extents.x),
            Random.Range(-extents.y, extents.y),
            Random.Range(-extents.z, extents.z)
        );

        return collider.transform.TransformPoint(point);
    }
}
