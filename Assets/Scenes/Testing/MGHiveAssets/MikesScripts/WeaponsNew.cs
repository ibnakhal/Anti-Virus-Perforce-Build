using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponsNew : MonoBehaviour
{
        //controls
        [SerializeField]
        private string reload = "Reload";
        //gun sound
        [SerializeField]
        private AudioSource shotSound;
        //Weapon stats
        [SerializeField]
        private bool isCooledDown = true;
        [SerializeField]
        private Rigidbody bullet;
        [SerializeField]
        private float shotSpeed = 6.0f;
        [SerializeField]
        private Transform barrel;
        [SerializeField]
        private string fireStyle = "Single";
        [SerializeField]
        private float spread;
        [SerializeField]
        private float shotCool;
        [SerializeField]
        private float burstRate = .01f;
        [SerializeField]
        private float maxSpreadUp;
        [SerializeField]
        private float maxSpreadDown;
        [SerializeField]
        private float reloadSpeed;
        [SerializeField]
        private float manuReloadSpeed;
        [SerializeField]
        private bool isSpec;
        //[MG] these need to be public for it to be called from player********************************
        public bool fireMe = false;
        public bool activeWeapon;
        public int currentAmmo;
        public bool Reloading = false;
        public int MagSize;
    //***************************************************************************************

    //[MG] Painkiller unique vars
    [SerializeField]
    private GameObject[] blades;
    [SerializeField]
    private GameObject hitBox;
    [SerializeField]
    private float hitRate;
    [SerializeField]
    private bool bladesActive;
    [SerializeField]
    private GameObject idle;
    [SerializeField]
    private GameObject active;
    //[MG] PK - Secondary fire vars (If we get to secondary fire)
    [SerializeField]
    private GameObject mountBarrel;
    [SerializeField]
    private GameObject mountTrigger;

    //[MG] Buster Rifle(Wing Zero) vars
    [SerializeField]
    private AudioSource busterCharge;
    [SerializeField]
    private float busterCool;
    [SerializeField]
    private float chargeTime;

    [SerializeField]
    private bool firing;

    [SerializeField]
    private Text ammoCount;

        private bool testing;
        void Start()
        {
            currentAmmo = MagSize;
        }

        void Update()
        {
        if (fireStyle != "Melee" || fireStyle != "Buster")
        {
            if (currentAmmo == 0)
            {
                ammoCount.text = "0";
            }
            else
            {
                ammoCount.text = (currentAmmo.ToString());
            }

            if (!Reloading)
            {
                if (Input.GetKey(KeyCode.R))
                {
                    if (fireStyle != "Buster")
                    {
                        StartCoroutine("ManuReloadGun");
                    }
                }

                if (currentAmmo == 0 && fireStyle != "Buster")
                {
                    StartCoroutine("ReloadGun");
                }
            }
        }

            if(Input.GetMouseButton(0))
            {
                if (this.activeWeapon == true)
                {
                    if (!Reloading)
                    {
                        if (fireStyle == "Single")
                        {
                            StartCoroutine("SingleShoot");
                        }
                        if (fireStyle == "Burst")
                        {
                            StartCoroutine("BurstShoot");
                        }
                        if (fireStyle == "Lob")
                        {
                            StartCoroutine("LobShoot");
                        }
                        if (fireStyle == "Rapid")
                        {
                            StartCoroutine("RapidShoot");
                        }
                        if(fireStyle == "Buster")
                        {
                            StartCoroutine("WingZero");
                        }
                }
                }
            }
            if(Input.GetButtonDown("Fire1"))
            {
            if (fireStyle == "Melee")
            {
                bladesActive = true;

                if (idle.activeSelf == true)
                {
                    idle.SetActive(false);
                    active.SetActive(true);
                }
            }
            }
            if(bladesActive)
            {
            StartCoroutine("MeleeBox");
            }

        if (Input.GetButtonUp("Fire1"))
            {
                if (fireStyle == "Melee")
                {
                    bladesActive = false;
                    hitBox.SetActive(false);
                    idle.SetActive(true);
                    active.SetActive(false);
                    isCooledDown = true;
                    shotSound.Stop();
                }
            }
    }

        private IEnumerator SingleShoot()
        {
            if (isCooledDown)
            {
            print("I am shooting");
                if (currentAmmo > 0)
                {
                    Rigidbody clone = Instantiate(bullet,
                                                   barrel.transform.position,
                                                   barrel.transform.rotation) as Rigidbody;
                    clone.velocity = barrel.transform.forward * shotSpeed;
                    shotSound.Play();
                    --currentAmmo;
                    isCooledDown = false;
                    yield return new WaitForSeconds(shotCool);
                    fireMe = false;
                    isCooledDown = true;
                }
                else
                {
                    print(this.gameObject.name + " is out of ammo");
                }
            }
        }

    private IEnumerator MeleeBox()
    {
        if (isCooledDown)
        {
            isCooledDown = false;

            if (shotSound.isPlaying == false)
            {
                shotSound.Play();
            }
            hitBox.SetActive(true);
            yield return new WaitForSeconds(hitRate);
            hitBox.SetActive(false);
            yield return new WaitForSeconds(shotCool);
            isCooledDown = true;
        }
    }

        private IEnumerator BurstShoot()
        {
                if (isCooledDown)
                {
                    if (currentAmmo > 0)
                    {
                        isCooledDown = false;
                        Rigidbody clone = Instantiate(bullet,
                                                       barrel.transform.position,
                                                       barrel.transform.rotation) as Rigidbody;
                        clone.velocity = barrel.transform.forward * shotSpeed;
                        shotSound.Play();
                        --currentAmmo;
                        yield return new WaitForSeconds(burstRate);
                        Rigidbody clone2 = Instantiate(bullet,
                                                       barrel.transform.position,
                                                       barrel.transform.rotation) as Rigidbody;
                        clone2.velocity = barrel.transform.forward * shotSpeed;
                        shotSound.Play();
                        --currentAmmo;
                        yield return new WaitForSeconds(burstRate);
                        Rigidbody clone3 = Instantiate(bullet,
                                                       barrel.transform.position,
                                                       barrel.transform.rotation) as Rigidbody;
                        clone3.velocity = barrel.transform.forward * shotSpeed;
                        shotSound.Play();
                        --currentAmmo;
                        yield return new WaitForSeconds(burstRate);
                        yield return new WaitForSeconds(shotCool);
                        fireMe = false;
                        isCooledDown = true;
                    }
                }
        }

        private IEnumerator LobShoot()
        {
            if (isCooledDown)
            {
                if (currentAmmo > 0)
                {
                    Rigidbody clone = Instantiate(bullet,
                                                   barrel.transform.position,
                                                    barrel.transform.rotation) as Rigidbody;
                    clone.velocity = barrel.transform.forward * shotSpeed;
                    shotSound.Play();
                    --currentAmmo;
                    isCooledDown = false;
                    yield return new WaitForSeconds(shotCool);
                    fireMe = false;
                    isCooledDown = true;
                }
            }
        }

        private IEnumerator RapidShoot()
        {
            if (isCooledDown)
            {
                if (currentAmmo > 0)
                {
                    Rigidbody clone = Instantiate(bullet,
                                                   barrel.transform.position,
                                                   barrel.transform.rotation) as Rigidbody;
                    clone.velocity = ((barrel.transform.forward * shotSpeed) + (barrel.transform.up * Random.Range(maxSpreadDown, maxSpreadUp)));
                    --currentAmmo;
                    isCooledDown = false;
                    yield return new WaitForSeconds(shotCool);
                    fireMe = false;
                    isCooledDown = true;
                }
            }

        }

        private IEnumerator ReloadGun()
        {
            Reloading = true;
            isCooledDown = false;
            yield return new WaitForSeconds(reloadSpeed);
            currentAmmo = MagSize;
            isCooledDown = true;
            yield return new WaitForSeconds(reloadSpeed);
            Reloading = false;
            print(currentAmmo);
        }

    private IEnumerator WingZero()
    {
        if (!firing)
        {
            if (isCooledDown)
            {
                
                if (currentAmmo > 0)
                {
                    firing = true;
                    busterCharge.Play();
                    yield return new WaitForSeconds(chargeTime);
                    Rigidbody clone = Instantiate(bullet,
                                                   barrel.transform.position,
                                                   barrel.transform.rotation) as Rigidbody;
                    clone.velocity = barrel.transform.forward * shotSpeed;
                    shotSound.Play();
                    --currentAmmo;
                    isCooledDown = false;
                    yield return new WaitForSeconds(shotCool);
                    firing = false;
                    yield return new WaitForSeconds(busterCool);
                    isCooledDown = true;
                }
            }
        }
    }

    private IEnumerator ManuReloadGun()
    {
        Reloading = true;
        isCooledDown = false;
        yield return new WaitForSeconds(manuReloadSpeed);
        currentAmmo = MagSize + 1;
        isCooledDown = true;
        yield return new WaitForSeconds(manuReloadSpeed/2);
        Reloading = false;
    }
}