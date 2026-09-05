using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using UnityEngine.VFX;
using UnityEngine.AI;

public class ObjectSlower : MonoBehaviour
{
    [SerializeField] private GameObject effect;

    private Rigidbody rb;

    private float slowMultiplier;
    private float lifeTime;

    private bool toSLow = false;

    private GameObject spawnedEffect;

    private void Start()
    {
        
        //ParticleSystem ps = spawnedEffect.GetComponent<ParticleSystem>();
        //ps.Play();
    }

    private void FixedUpdate()
    {
        if (toSLow)
        {
            SLow();
        }
    }

    private void SLow()
    {
        //if (rb.linearVelocity.y < 0)
        //{
        //    Vector3 counterForce = slowMultiplier * -Physics.gravity;
        //    rb.AddForce(counterForce, ForceMode.Acceleration);
        //}
    }
    private void CheckAndDestroy()
    {
        Destroy(spawnedEffect, 1.3f);
        if (lifeTime > 0)
        {
            Destroy(gameObject, lifeTime);
        }
        else
        {
            StartCoroutine(Wait());
        }
    }
    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(1.4f);
        EndSlowing();
    }

    public void EndSlowing()
    {
        NavMeshAgent agent = GetComponent<NavMeshAgent>();
        agent.enabled = true;
        Debug.Log(agent.isOnNavMesh);
        gameObject.GetComponent<EnemyBasic>().StopOrResumeAI();
        toSLow = false;
    }
    public void Init(GameObject enemy, Rigidbody rb, float slowMultiplier, float lifeTime)
    {
        this.rb = rb;
        this.slowMultiplier = slowMultiplier;
        this.lifeTime = lifeTime;
        toSLow = true;

        spawnedEffect = Instantiate(effect, gameObject.transform.position, Quaternion.identity);
        CheckAndDestroy();
    }
}
