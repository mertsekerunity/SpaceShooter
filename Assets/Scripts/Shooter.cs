using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [Header("General")]
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifeTime = 5f;
    [SerializeField] float baseFiringRate = 0.2f;


    [Header("Enemy")]
    [SerializeField] bool isEnemy;
    [SerializeField] float firingRateVariance = 0.3f;
    [SerializeField] float minimumFiringRate = 0.1f;


    [HideInInspector] public bool isFiring;

    Coroutine firingCoroutine;

    private void Start()
    {
        if (isEnemy)
        {
            isFiring = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        Fire();
    }

    void Fire()
    {
        if (isFiring && firingCoroutine == null)
        {
            firingCoroutine = StartCoroutine(FireContinuously());
        }
        else if (!isFiring && firingCoroutine != null)
        {
            StopCoroutine(firingCoroutine);
            firingCoroutine = null;
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject instance = Instantiate(projectilePrefab,
                transform.position,Quaternion.identity);

            Rigidbody2D rb2d = instance.GetComponent<Rigidbody2D>();
            if(rb2d != null)
            {
                rb2d.velocity = transform.up * projectileSpeed;
            }

            Destroy(instance, projectileLifeTime);

            float timeToNextProjectile = Random.Range(baseFiringRate-firingRateVariance,
                baseFiringRate+firingRateVariance);

            timeToNextProjectile = Mathf.Clamp(timeToNextProjectile, minimumFiringRate, float.MaxValue);


            yield return new WaitForSeconds(baseFiringRate);
        }
    }
}
