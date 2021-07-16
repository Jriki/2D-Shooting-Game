using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // Config parameters
    [Header("Player Movement")]
    [SerializeField] float moveSpeed = 10f;
    [SerializeField] float xPadding = 1f;
    [SerializeField] float yPadding = 1f;

    [Header("Player Health")]
    [SerializeField] int pHealth = 1000;

    [Header("Player Projectile")]
    [SerializeField] GameObject laserPrefab;
    [SerializeField] float projectileSpeed = 20f;
    [SerializeField] float projectileFiringPeriod = 0.1f;

    [Header("Player SFX")]
    [SerializeField] AudioClip pDeathSound;
    [SerializeField] [Range(0, 1)] float pDeathSVolume = 0.5f;
    [SerializeField] AudioClip pProjectileSound;
    [SerializeField] [Range(0, 1)] float pProjectileSVolume = 0.2f;


    Coroutine firingCoroutine;

    // For Game Camera
    float xMin;
    float xMax;
    float yMin;
    float yMax;   
    // Start is called before the first frame update
    void Start()
    {
        SetUpMoveBoundaries();
    }
    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
    }
    private void Fire()
    {
        if (Input.GetButtonDown("Fire1")) // press space button
        {
           firingCoroutine = StartCoroutine(FireContinuously());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingCoroutine);
        }
    }

    IEnumerator FireContinuously()
    {
        while (true)
        {
            GameObject laser = Instantiate(laserPrefab,
                transform.position,
                Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, projectileSpeed);
            AudioSource.PlayClipAtPoint(pProjectileSound, Camera.main.transform.position, pProjectileSVolume);
            yield return new WaitForSeconds(projectileFiringPeriod);
        }
    }

    private void Move() // player movement
    {
        var deltaX = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed; //from Unity Input Manager from (edit) Project Settings 
        var deltaY = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed; //from Unity Input Manager from (edit) Project Settings 

        var newXPos = Mathf.Clamp(transform.position.x + deltaX, xMin, xMax); //for x axis 
        var newYPos = Mathf.Clamp(transform.position.y + deltaY, yMin, yMax); //for y axis 
        transform.position = new Vector2(newXPos, newYPos);
    }
    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).x + xPadding;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(1, 0, 0)).x - xPadding;
        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0, 0)).y + yPadding;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, 1, 0)).y - yPadding;
    }

    //Player Health Process
    private void OnTriggerEnter2D(Collider2D other) 
    {
        Damage dealDamage = other.gameObject.GetComponent<Damage>();
        if (!dealDamage) { return; }  // protecting against Null
        PlayerProcessHit(dealDamage);
    }

    private void PlayerProcessHit(Damage dealDamage)
    {
        pHealth -= dealDamage.GetDamage();
        dealDamage.Hit();
        if (pHealth <= 0)
        {
            death();
        }
    }
    private void death()
    {
        FindObjectOfType<Level>().LoadGameOver();
        Destroy(gameObject);
        AudioSource.PlayClipAtPoint(pDeathSound, Camera.main.transform.position, pDeathSVolume);
    }

    public int GetHealth()
    {
        return pHealth;
    }


}
