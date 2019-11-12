using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class CTT_WeaponSwap_Quest : MonoBehaviour
{


    public OVRInput.Button controlButton;

    public GameObject gun;
    public GameObject sword;

    public enum Weapon
    {
        Sword,
        Gun,
        None
    }

    public Weapon startWeapon = Weapon.Sword;
    private Weapon curWeapon = Weapon.None;

    // Start is called before the first frame update
    void Start()
    {

        switchWeapon(Weapon.None);

    }

    

    public void buttonPress()
    {
        if (GameObject.Find("Control").GetComponent<CTT_GameTracker_Quest>().isGameActive())
        {
            if (curWeapon == Weapon.None)
            {
                switchWeapon(startWeapon);
            }
            else if (curWeapon == Weapon.Sword)
            {
                switchWeapon(Weapon.Gun);
            }
            else if (curWeapon == Weapon.Gun)
            {
                switchWeapon(Weapon.Sword);
            }
        }
    }

    public void switchWeapon(Weapon w)
    {
        if (w == Weapon.Sword)
        {
            gun.SetActive(false);
            sword.SetActive(true);
            curWeapon = Weapon.Sword;
        }
        else if (w == Weapon.Gun)
        {
            gun.SetActive(true);
            sword.SetActive(false);
            curWeapon = Weapon.Gun;
        }
        else if (w == Weapon.None) { 

            gun.SetActive(false);
            sword.SetActive(false);
            curWeapon = Weapon.None;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(controlButton))
        {
            buttonPress();
        }
    }
}
