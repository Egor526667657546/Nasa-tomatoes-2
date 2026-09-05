using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimManager : MonoBehaviour
{
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

    private List<EnemyBasic> spawnedEnemies = new List<EnemyBasic>();

    private bool lastAnim = false;
    private bool firstAnim = false;
    private float time = 1.3f;
    private void Awake()
    {
        spawnEnemies.SlowMultiplier = slowMultiplier;
    }

    public void StartAnim()
    {
        StartCoroutine(PreAnim());
    }
    private IEnumerator PreAnim()
    {
        aimCrosshair.SetActive(false);
        crosshair.SetActive(false);
        circleCrosshair.SetActive(false);
        playerMovement.LockOrNotMovement(false);
        firstAnim = true;
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
                Debug.Log("unlocked?");
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
            for (int i = 0; i < aimDots.Count; i++)
            {
                EnemyBasic enemyToSpawn = typesOfEnemies[Random.Range(0, typesOfEnemies.Count)];
                if (lastAnim)
                {
                    spawnEnemies.Spawn(aimDots[i], enemyToSpawn, 0);
                }
                else
                {
                    spawnEnemies.Spawn(aimDots[i], enemyToSpawn, time);
                }
                spawnedEnemies.Add(enemyToSpawn);
            }
            firstAnim = false;
        }
        else
        {
            for (int i = 0; i < aimDots.Count; i++)
            {
                EnemyBasic enemyToSpawn = spawnedEnemies[i];
                if (lastAnim)
                {
                    spawnEnemies.Spawn(aimDots[i], enemyToSpawn, 0);
                }
                else
                {
                    spawnEnemies.Spawn(aimDots[i], enemyToSpawn, time);
                }
            }
        }
       
        yield return new WaitForSeconds(time);

        if (lastAnim)
        {
            spawnedEnemies.Clear();
        }
        //yield return new WaitForSeconds(animationTime / cameras.Count);
    }
    // если 1 м = 0.7с, то 1 замедленый м = 0.7 += 0.7 * slowMultiplier (секунд). мне надо 1 м = 1 с. тогда  0.3 = х% от 0.7. тогда 0.7 = 100, 0.3 = х. 30 = 0.7х, х = 30/0.7. х = 43% мультиплаер 
}
