//Amorina Tabera
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using TMPro;

public class Gun : MonoBehaviour
{
    //Gun Stats
    public int damage;                      //Damage done by gun
    public float timeBetweenShooting;       //Time interval between shot        
    public float reloadTime;                //Time to reload (not used in this code)
    public int magazineSize;                //Magazine Size, in this case 1
    public int bulletsPerTap;               //How many bullets are shot, in this case 1
    public int bulletsLeft;                 //Bullets left in magazine
    //public int bulletsShot;                 //Bullets shot so far (for debuging)

    //bullet 
    public GameObject bullet;               //Get Bullet prefab
    //bullet force
    public float shootForce;                //Force gun shoots bullet forward
    public float upwardForce;               //Force gun shoots bullet upward (parabolic)

    //bools
    bool readyToShoot;                      //Ready to shoot again, or not, according to timeBetweenShooting
    bool reloading;                         //Checking if in the middle of reloading, not used in this code

    //Reference
    public Camera fpsCam;                   //First person camera, gun is a child of this
    public Transform attackPoint;           //Attack point is where the bullets will be instantiated 
    public RaycastHit rayHit;               //Raycast for where Player wants to shoot bullet
    public LayerMask whatIsEnemy;           //Get enemy layer
    
    /*//Sounds
    public AudioSource Shotgunfire;
    public AudioClip Shotgun;
*/

    //Called before Start(), slightly better
    private void Awake()
    {
        bulletsLeft = magazineSize;         //Sets magazine
        readyToShoot = true;                //Player starts ready to shoot

    }

    //Method MyInput for checking when Player wants to shoot or reload
    public void MyInput()
    {
        //Reload
        if (bulletsLeft < magazineSize && !reloading)           //Checks if magazine is empty and player clicks the reload button (which is not present for this project)
        {
            Reload();                                           //Calls method Reload(), which is 0 seconds for this project
        }

        //Shoot
        if (readyToShoot && !reloading && bulletsLeft > 0)      //Checks if ready to shoot and have bullets left and not in the middle of reloading
        {
            //bulletsShot += bulletsPerTap;                       //Increment bullets shot (for debuging)
            Shoot();                                            //Calls method Shoot()
        }
    }

    //Method to shoot gun
    private void Shoot()
    {
        readyToShoot = false;                                               //Set to false so time interval for shots starts

        //Find the exact hit position Player wants to shoot using a raycast
        Ray ray = fpsCam.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));    //Ray set to middle of screen
        RaycastHit hit;                                                     //Raycast to hold the hit point

        //check if ray hits something
        Vector3 targetPoint;                                                //Make a vector3 for the target point
        if (Physics.Raycast(ray, out hit))                                  //returns true if something is hit
        {
            targetPoint = hit.point;                                        //targetpoint becomes a vector3 to the raycast hit point
        }
        else                                                                //Player did not hit something (open sky for example)
        {
            targetPoint = ray.GetPoint(100);                                //Just a point far away from the player
        }

        Vector3 directionOfBullet = targetPoint - attackPoint.position;     //Calculate direction from attackPoint to targetPoint

        //Instantiate bullet/projectile
        GameObject currentBullet = Instantiate(bullet, attackPoint.position, Quaternion.identity); //store instantiated bullet in currentBullet
        currentBullet.transform.forward = directionOfBullet.normalized;                            //Rotate bullet to shoot direction

        //Add forces to bullet and make go to direct attacking point
        currentBullet.GetComponent<Rigidbody>().AddForce(directionOfBullet.normalized * shootForce, ForceMode.Impulse);
        currentBullet.GetComponent<Rigidbody>().AddForce(fpsCam.transform.up * upwardForce, ForceMode.Impulse);

        bulletsLeft--;          //Decrement bullets left in magazine
        //bulletsShot--;          //Increment bullets shot, for debuging
        
        //Shots fired!
        //Shotgunfire.Play();

        Invoke("ResetShot", timeBetweenShooting);   //Invoke take a (method, time interval)

        
    }

    //Method to reset the shot
    private void ResetShot()
    {
        readyToShoot = true;    //Player can now shoot again
    }

    //Method to reolad
    private void Reload()
    {
        reloading = true;                       //Player started reloading
        Invoke("ReloadFinished", reloadTime);   //Begin to finish reloading
    }

    //Method to finish reloading
    private void ReloadFinished()
    {
        bulletsLeft = magazineSize;             //Magazine set
        reloading = false;                      //Player finished reloading
    }
}