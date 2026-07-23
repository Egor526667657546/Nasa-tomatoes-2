using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class SpawnEnemies : MonoBehaviour
{
    private float slowMultiplier = 0;


    public float SlowMultiplier { get => slowMultiplier; set => slowMultiplier = value; }

    public void Spawn(Transform aimPos, EnemyBasic type, float time)
    {
        GameObject enemy = Instantiate(type.gameObject, aimPos.position, aimPos.rotation);
        UnityEngine.AI.NavMeshAgent agent = enemy.GetComponent<UnityEngine.AI.NavMeshAgent>();
        Debug.Log(agent.enabled);

        Rigidbody erb = enemy.GetComponent<Rigidbody>();
        ObjectSlower os = enemy.GetComponent<ObjectSlower>();
        if (erb == null || os == null)
        {
            Debug.LogError($"{enemy.name} doesn't have Rigidbody or ObjectSlower");
            Destroy(enemy);
            return;
        }

        enemy.gameObject.transform.position += new Vector3(0, 1, 0);
        os.Init(enemy, erb, slowMultiplier, time);
    }
}
