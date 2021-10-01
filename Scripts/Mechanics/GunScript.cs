using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunScript : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] AudioSource shootSource;

    [Header("Shooting and reloading")]
    [SerializeField] float currentAmmo = 0;
    [SerializeField] float maxAmmo = 25;
    [SerializeField] float reloadTime = 1.5f;
    [SerializeField] float shootDelay = 0.3f;
    private bool shooting;
    private bool canShoot;
    private bool reloading;

    [Header("Animations")]
    [SerializeField] string aimParameter = "aim";
    [SerializeField] string shootParameter = "shoot";
    [SerializeField] string reloadParameter = "reload";

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();

        currentAmmo = maxAmmo;

        canShoot = true;
        reloading = false;
    }

    void Update()
    {
        if(Input.GetMouseButtonDown(0)) animator.SetBool("shoot", true);
        else if(Input.GetMouseButtonUp(0)) animator.SetBool("shoot", false);

        if (Input.GetMouseButton(0) && canShoot) Shoot();

        if (Input.GetMouseButtonDown(1)) animator.SetBool(aimParameter, true);
        else if (Input.GetMouseButtonUp(1)) animator.SetBool(aimParameter, false);

        if (Input.GetKey(KeyCode.R) && !reloading) StartCoroutine(Reload());
    }

    private void Shoot()
    {
        canShoot = false;
        StartCoroutine(ShootDelay());

        shootSource.Play();
    }
    
    private IEnumerator Reload()
    {
        animator.SetBool(reloadParameter, true);

        reloading = true;
        canShoot = false;

        yield return new WaitForSeconds(reloadTime);

        animator.SetBool(reloadParameter, false);
        currentAmmo = maxAmmo;

        reloading = false;
        canShoot = true;
    }

    private IEnumerator ShootDelay()
    {
        yield return new WaitForSeconds(shootDelay);

        if(!reloading) canShoot = true;
    }
}
