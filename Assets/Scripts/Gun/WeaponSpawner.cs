using UnityEngine;

public class WeaponSpawner : MonoBehaviour
{
    public GameObject gun1Prefab;
    public GameObject gun2Prefab;

    private GameObject currentGun;

    void Start()
    {
        SpawnGun(gun1Prefab);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            SpawnGun(gun1Prefab);
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            SpawnGun(gun2Prefab);
        }
    }

    void SpawnGun(GameObject gunPrefab)
    {
        if (currentGun != null)
        {
            Destroy(currentGun);
        }

        currentGun = Instantiate(gunPrefab, transform);

        currentGun.transform.localPosition =
            new Vector3(0.4f, -0.3f, 0.7f);

        currentGun.transform.localRotation =
            Quaternion.Euler(0f, 0f, 0f);

        currentGun.transform.localScale =
            new Vector3(3f, 3f, 3f);
    }
}