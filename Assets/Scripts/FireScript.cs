using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FireScript : MonoBehaviour
{
    [SerializeField] private GameObject casingPrefab;
    [SerializeField] private GameObject muzzleFlashPrefab;


    [SerializeField] private Animator gunAnimator;
    [SerializeField] private Transform barrelLocation;
    [SerializeField] private Transform casingExitLocation;
    [SerializeField] private Transform fpscam;


    [SerializeField] private float destroyTimer = 2f;
    [SerializeField] private float ejectPower = 150f;

    [SerializeField] private GameStats _gameStats;
    [SerializeField] private WeaponSetting _weapon;

    [SerializeField] private Text ammoText;


    void Start()
    {
        _weapon.Ammo = 0;
    }

    void Update()
    {
        ammoText.text = "AMMO : " + _weapon.Ammo;
        if (!_gameStats.isGameOver  && _weapon.Ammo > 0 && Input.GetButtonDown("Fire1"))
        {
            _weapon.Ammo -= 1;
            gunAnimator.SetTrigger("Fire");
                RaycastHit hit;
                if (Physics.Raycast(fpscam.transform.position, fpscam.transform.forward, out hit, _weapon.Range))
                {
                Debug.Log("Fire");
                    if (hit.transform.gameObject.CompareTag("zombie"))
                    {
                    Debug.Log("Fire2");
                    ZombieController zombie = hit.transform.GetComponent<ZombieController>();
                        zombie.getDamage(_weapon.Damage);
                   
                    }
                }
        }
    }


   private void Shoot()
    {
        if (muzzleFlashPrefab)
        {
            GameObject tempFlash;
            tempFlash = Instantiate(muzzleFlashPrefab, barrelLocation.position, barrelLocation.rotation);
            Destroy(tempFlash, destroyTimer);
        }

        
    }

    private void CasingRelease()
    {
        GameObject tempCasing;
        tempCasing = Instantiate(casingPrefab, casingExitLocation.position, casingExitLocation.rotation) as GameObject;
        tempCasing.GetComponent<Rigidbody>().AddExplosionForce(Random.Range(ejectPower * 0.7f, ejectPower), (casingExitLocation.position - casingExitLocation.right * 0.3f - casingExitLocation.up * 0.6f), 1f);
        tempCasing.GetComponent<Rigidbody>().AddTorque(new Vector3(0, Random.Range(100f, 500f), Random.Range(100f, 1000f)), ForceMode.Impulse);
        Destroy(tempCasing, destroyTimer);
    }

    private void ControlAmmo()
    {
        ammoText.text = "AMMO : " + _weapon.Ammo;
    }
}
