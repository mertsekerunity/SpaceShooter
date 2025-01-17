using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    [SerializeField] GameObject projectilePrefab;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileLifeTime = 5f;
    [SerializeField] float firingRate = 0.2f;

    public bool isFiring;

    Coroutine firingCoroutine;

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
            yield return new WaitForSeconds(firingRate);
        }
    }
}
