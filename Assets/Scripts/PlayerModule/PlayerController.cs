using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField] private GameStats _gameStats;
    [SerializeField] private GameObject _hitImage;
    [SerializeField] private Animator myAnim;
    [SerializeField] private Animator flashlightAnim;
    [SerializeField] private AudioSource myAudio;
    [SerializeField] private AudioClip stepOne;
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private bool isFlashOn;
    [SerializeField] private Flashlight_PRO Flashlight;
    [SerializeField] private Transform fpscam;
    [SerializeField] private Text healthText;
    [SerializeField] private WeaponSetting _weapon;
    [SerializeField] private Text itemInfo;



    private float pickupRange = 10f;

    private bool haveBattery = false;
    private float inputX;
    private float inputZ;

    void Start()
    {
        SetHealth();
    }

    
    void Update()
    {
        if (_gameStats.isGameOver == false)
        {
            Movement();
            FixHitScreen();
            OpenFlashLight(haveBattery);
            CheckGrab();
        }
    }

   private void Movement()
    {
            inputX = Input.GetAxis("Horizontal");
            inputZ = Input.GetAxis("Vertical");

            Vector3 direction = new Vector3(inputX, 0f, inputZ);
            transform.Translate(direction * _playerStats.Speed * Time.deltaTime);

            if (inputX != 0 || inputZ != 0)   //Yürüme animasyonu kamera
        {
            myAnim.SetBool("isWalking", true);
        }
            else
        {
            myAnim.SetBool("isWalking", false);
        }
            
    }

    private void StepOne()
    {
        myAudio.PlayOneShot(stepOne);
    }
  


    private void OpenFlashLight(bool haveBattery)
    {
            if (Input.GetKeyDown(KeyCode.T))
            {
                if (haveBattery)
            {
                Flashlight.Switch();
                flashlightAnim.SetTrigger("switchFlash");
            }
        }
    }

    private void CheckGrab()
    {
        RaycastHit hit;

        if (Physics.Raycast(fpscam.position, fpscam.forward, out hit, pickupRange))
        {
            if (hit.transform.gameObject.CompareTag("cangrab"))
            {
                itemInfo.text = hit.transform.gameObject.name;
                itemInfo.gameObject.SetActive(true);
                if (hit.transform.gameObject.name == ("Bullet"))
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        myAnim.SetTrigger("pickup");
                        _weapon.Ammo += 1;
                        Destroy(hit.transform.gameObject);
                    }
                }
                else if (hit.transform.gameObject.name == ("Battery"))
                {
                    if (Input.GetKeyDown(KeyCode.F))
                    {
                        myAnim.SetTrigger("pickup");
                        haveBattery = true;
                        Destroy(hit.transform.gameObject);
                    }
                }
               
               
            }
            else
            {
                itemInfo.gameObject.SetActive(false);
            }
        }
        
    }

    private void SetHealth()
    {
        _playerStats.Health = 100;
        healthText.text = _playerStats.Health.ToString();
    }

    public void GetDamage(float damage)
    {
        _playerStats.Health -= damage;
        healthText.text = _playerStats.Health.ToString();
        HitScreen();
    }
    private void HitScreen()
    {
        var hitColor = _hitImage.GetComponent<Image>().color;
        hitColor.a = 0.5f;
        _hitImage.GetComponent<Image>().color = hitColor;
    }

    private void FixHitScreen()
    {
        if (_hitImage.GetComponent<Image>().color.a > 0)
        {
            var hitColor = _hitImage.GetComponent<Image>().color;
            hitColor.a -= 0.02f;
            _hitImage.GetComponent<Image>().color = hitColor;
        }
    }

   

}
