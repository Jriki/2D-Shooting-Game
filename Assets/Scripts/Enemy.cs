using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [Header("Enemy Health")]
    [SerializeField] float eHealth = 100; //enemy health

    [Header("Enemy Stats")]
    [SerializeField] int scoreValue = 150;

    [Header("Enemy Attack")]
    float eAttackCounter;
    [SerializeField] float eMinTimeBetweenAttacks = 0.3f;
    [SerializeField] float eMaxTimeBetweenAttacks = 3f;

    [Header("Enemy Projectile")]
    [SerializeField] GameObject enemyProjectile;
    [SerializeField] float projectileSpeed = 10f;
    [SerializeField] float projectileFiringPeriod = 0.1f;

    [Header("Enemy Projectile VFX")]
    [SerializeField] GameObject deathVFX;
    [SerializeField] float durationOfExplosion = 1f;

    [Header("Enemy Sound Effect")]
    [SerializeField] AudioClip enemyProjectileSound;
    [SerializeField] [Range(0,1)] float projectileVolume = 0.1f;
    [SerializeField] AudioClip enemyDeathSound;
    [SerializeField] [Range(0,1)] float deaathSVolume = 0.2f;

    // Start is called before the first frame update
    void Start()
    {
        eAttackCounter = Random.Range(eMinTimeBetweenAttacks, eMaxTimeBetweenAttacks);
    }

    // Update is called once per frame
    void Update()
    {
        CountDownAndAttack();
    }
    private void CountDownAndAttack()
    {
        eAttackCounter -= Time.deltaTime;
        if(eAttackCounter <= 0f)
        {
            EFire1();
            eAttackCounter = Random.Range(eMinTimeBetweenAttacks, eMaxTimeBetweenAttacks);
        }
    }
    private void EFire1()
    {
        GameObject eLaser = Instantiate(enemyProjectile,
        transform.position,
        Quaternion.identity) as GameObject;
        eLaser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -projectileSpeed); // negative projectile to shoot downward 
        AudioSource.PlayClipAtPoint(enemyProjectileSound, Camera.main.transform.position);
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        Damage dealDamage = other.gameObject.GetComponent<Damage>();
        if (!dealDamage) { return; } // protecting against Null
        ProcessHit(dealDamage);
    }

    private void ProcessHit(Damage dealDamage)
    {
        eHealth -= dealDamage.GetDamage();
        dealDamage.Hit();
        if (eHealth <= 0)
        {
            Dead();
        }

    }
    private void Dead()
    {
        FindObjectOfType<GameSession>().addToScore(scoreValue); //calling addtoscore method from GameSession
        Destroy(gameObject);
        GameObject explosionParticle = Instantiate(deathVFX,
         transform.position, transform.rotation);
        Destroy(explosionParticle, durationOfExplosion);
        AudioSource.PlayClipAtPoint(enemyDeathSound, Camera.main.transform.position, deaathSVolume);
    }

}
