using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class weaponScript : MonoBehaviour
{
    //public Camera playercam;



    //shooting 
    public bool isShooting, readyToShoot;
    bool allowRest = true;
    public float shootingDelay=2f;


    //burst
    public int bulletsPerBurst = 3;
    public int currentBurst;


    //spread
    public float spreadIntensity;


    //bullet

    public GameObject bulletPrefab;
    public Transform bulletspawn;

    public float bulletvelocity = 30f;
    public float bulletLifespan = 3f;




    public GameObject muzzleEffect;


    private Animator animator;


    public float Reloadtime;
    public int magazineSize, bulletsLeft;
    public bool IsReloading;


    public TextMeshProUGUI ammoDisplay;

    public enum shootingMode
    {
        single,
        burst,
        auto

    }

    public shootingMode currentShootingMode;


    private void Awake()
    {
        readyToShoot = true;
        currentBurst = bulletsPerBurst;

        animator=GetComponent<Animator>();
        bulletsLeft=magazineSize;
    }

   

    // Update is called once per frame
    void Update()
    {
        //if (Input.GetKeyDown(KeyCode.Mouse0))
        //{
        //    Fireweapon();
        //}


        //get key as firing should be done on all time mouse button is clicked
        if (currentShootingMode == shootingMode.auto)
        {
            isShooting = Input.GetKey(KeyCode.Mouse0);

        }


        //get key down as fire only once on key down
        else if (currentShootingMode == shootingMode.burst || currentShootingMode == shootingMode.single)
        {
            isShooting = Input.GetKeyDown(KeyCode.Mouse0);
        }


        if(readyToShoot && isShooting)
        {
            currentBurst = bulletsPerBurst;
            Fireweapon();
        }


        if (ammoDisplay != null)
        { 
            ammoDisplay.text()
        }


        if (Input.GetKey(KeyCode.R) && bulletsLeft < magazineSize && IsReloading == false)
        {
            Reload();
        }



        if (readyToShoot && isShooting == false && IsReloading == false && bulletsLeft <= 0)
        {
            Reload();
        }
    }

    private void Fireweapon()
    {
        bulletsLeft--;
        muzzleEffect.GetComponent<ParticleSystem>().Play();


        animator.SetTrigger("recoil");

        soundManager.instance.shootingSound.Play();


        readyToShoot = false;

        Vector3 shootingDirection=CalculateDirectionAndSpread().normalized;


       GameObject bullet=Instantiate(bulletPrefab, bulletspawn.position,Quaternion.identity);


        //pointing the bullet to face the shooting direction
        bullet.transform.forward = shootingDirection;

        bullet.GetComponent<Rigidbody>().AddForce(shootingDirection * bulletvelocity,ForceMode.Impulse);

        StartCoroutine(destroyBulletAfterSeconds(bullet, bulletLifespan));



        if (allowRest)
        {
            Invoke("ResetShot", shootingDelay);
            allowRest = false;
        }


        if (currentShootingMode == shootingMode.burst && currentBurst > 1)
        {
            currentBurst--;
            Invoke("Fireweapon",shootingDelay);
        }

    }



    private void Reload()
    {
        IsReloading = true;
        Invoke("ReloadCompleted", Reloadtime);
    }



    private void ReloadCompleted()
    {
        bulletsLeft = magazineSize;
        IsReloading=false;
    }
    private void ResetShot()
    {
        readyToShoot=true;
        allowRest = true;
    }




    private Vector3 CalculateDirectionAndSpread()
    {


        //shooting from middle of the screen
        Ray ray=Camera.main.ViewportPointToRay(new Vector3(0.5f,0.5f,0));
        RaycastHit Hit;

        Vector3 targetPoint;

        if(Physics.Raycast(ray,out Hit))
        {
            targetPoint=Hit.point;

        }
        else
        {
            targetPoint=ray.GetPoint(100);
        }

        Vector3 direction= targetPoint - bulletspawn.position;


        float x=UnityEngine.Random.Range(-spreadIntensity,spreadIntensity);
        float y=UnityEngine.Random.Range(-spreadIntensity, spreadIntensity);

        return direction+new Vector3(x,y,0);



    }

    private IEnumerator destroyBulletAfterSeconds(GameObject bullet, float delay)
    {
        yield return new WaitForSeconds(delay);
        Destroy(bullet);
    }
}
