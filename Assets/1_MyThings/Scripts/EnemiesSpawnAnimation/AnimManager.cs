using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class AnimManager : MonoBehaviour
{
    public static AnimManager ActiveManager;

    [SerializeField] private SpawnEnemies spawnEnemies;
    [SerializeField] private Movement1 playerMovement;

    [SerializeField] private Camera mainCamera;
    [SerializeField] private List<Camera> cameras;

    [SerializeField] private List<EnemyBasic> typesOfEnemies;
    [SerializeField] private List<Transform> aimDots;

    [SerializeField] private GameObject aimCrosshair;
    [SerializeField] private GameObject crosshair;
    [SerializeField] private GameObject circleCrosshair;

    [SerializeField] private float slowMultiplier;
    [SerializeField] private float animationTime;

    [SerializeField] private TMP_Text enemiesText;

    [SerializeField] private List<Animator> doorAnimators;

    [SerializeField] private GameObject levelCompleteText;

    [SerializeField] private ItemSpawner itemSpawner;

    [SerializeField] private Camera itemCamera;

    [SerializeField] private float itemCameraTime = 5f;

    private List<EnemyBasic> spawnedEnemies = new List<EnemyBasic>();

    public int aliveEnemies = 0;

    private bool lastAnim = false;
    private bool firstAnim = false;
    private bool levelCompleted = false;

    private float time = 1.3f;

    private void Awake()
    {
        spawnEnemies.SlowMultiplier = slowMultiplier;

        if (levelCompleteText != null)
        {
            levelCompleteText.SetActive(false);
        }

        if (itemCamera != null)
        {
            itemCamera.gameObject.SetActive(false);
        }

        UpdateEnemiesText();
    }

    public void StartAnim()
    {
        ActiveManager = this;

        firstAnim = true;
        lastAnim = false;
        levelCompleted = false;

        StartCoroutine(PreAnim());
    }

    private IEnumerator PreAnim()
    {
        aimCrosshair.SetActive(false);
        crosshair.SetActive(false);
        circleCrosshair.SetActive(false);

        playerMovement.LockOrNotMovement(false);

        mainCamera.gameObject.SetActive(false);

        for (int i = 0; i < cameras.Count; i++)
        {
            if (i == cameras.Count - 1)
            {
                lastAnim = true;
            }

            cameras[i].gameObject.SetActive(true);

            yield return StartCoroutine(Animation());

            cameras[i].gameObject.SetActive(false);

            if (lastAnim)
            {
                circleCrosshair.SetActive(true);

                mainCamera.gameObject.SetActive(true);

                playerMovement.LockOrNotMovement(true);
            }
        }
    }

    private IEnumerator Animation()
    {
        if (firstAnim)
        {
            aliveEnemies = aimDots.Count;

            for (int i = 0; i < aimDots.Count; i++)
            {
                EnemyBasic enemyToSpawn =
                    typesOfEnemies[Random.Range(0, typesOfEnemies.Count)];

                if (lastAnim)
                {
                    spawnEnemies.Spawn(
                        aimDots[i],
                        enemyToSpawn,
                        0
                    );
                }
                else
                {
                    spawnEnemies.Spawn(
                        aimDots[i],
                        enemyToSpawn,
                        time
                    );
                }

                spawnedEnemies.Add(enemyToSpawn);
            }

            firstAnim = false;

            UpdateEnemiesText();
        }
        else
        {
            for (int i = 0; i < aimDots.Count; i++)
            {
                if (i >= spawnedEnemies.Count)
                {
                    continue;
                }

                EnemyBasic enemyToSpawn = spawnedEnemies[i];

                if (lastAnim)
                {
                    spawnEnemies.Spawn(
                        aimDots[i],
                        enemyToSpawn,
                        0
                    );
                }
                else
                {
                    spawnEnemies.Spawn(
                        aimDots[i],
                        enemyToSpawn,
                        time
                    );
                }
            }
        }

        yield return new WaitForSeconds(time);
    }

    public void EnemyDied()
    {
        if (ActiveManager != this)
        {
            return;
        }

        aliveEnemies--;

        if (aliveEnemies < 0)
        {
            aliveEnemies = 0;
        }

        UpdateEnemiesText();

        Debug.Log(
            "Âðàã óìåð. " +
            gameObject.name +
            ". Îñòàëîñü: " +
            aliveEnemies
        );

        if (aliveEnemies == 0 && !levelCompleted)
        {
            levelCompleted = true;

            LevelComplete();
        }
    }

    private void LevelComplete()
    {
        Debug.Log(
            "ÓÐÎÂÅÍÜ ÏÐÎÉÄÅÍ: " +
            gameObject.name
        );

        foreach (Animator doorAnimator in doorAnimators)
        {
            if (doorAnimator != null)
            {
                doorAnimator.SetTrigger("Open");
            }
        }

        if (levelCompleteText != null)
        {
            levelCompleteText.SetActive(true);
        }

        if (itemSpawner != null)
        {
            itemSpawner.SpawnItem();
        }

        if (itemCamera != null)
        {
            StartCoroutine(ShowItemCamera());
        }
    }

    private IEnumerator ShowItemCamera()
    {

        if (mainCamera != null)
        {
            mainCamera.gameObject.SetActive(false);
        }

        foreach (Camera cam in cameras)
        {
            if (cam != null)
            {
                cam.gameObject.SetActive(false);
            }
        }

        itemCamera.gameObject.SetActive(true);

        yield return new WaitForSeconds(itemCameraTime);

        itemCamera.gameObject.SetActive(false);

        if (mainCamera != null)
        {
            mainCamera.gameObject.SetActive(true);
        }
    }

    private void UpdateEnemiesText()
    {
        if (enemiesText != null)
        {
            enemiesText.text =
                "Enemy: " +
                aliveEnemies;
        }
    }
}