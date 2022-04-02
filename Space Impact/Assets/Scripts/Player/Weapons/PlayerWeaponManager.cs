using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering;
using System.Linq;

public class PlayerWeaponManager : MonoBehaviour
{
    [HideInInspector()]
    public bool[] activeWeapon = new bool[3];
    [HideInInspector()]
    public Weapon[] weapons = new Weapon[3];
    private float[] activeTimes = new float[3];
    private float[] activeStartTimes = new float[3];

    //activates weapons;
    private bool weaponsActive;
    public bool WeaponsActive {
        get { return weaponsActive; }
        set { if (value == false) {
                activeWeapon = activeWeapon.Select((aw) => { aw = false; return aw; }).ToArray();
                weapons = weapons.Select((w) => { w.Active = false; return w; }).ToArray();
                if(rearWeapon != null) rearWeapon.Active = false;
            }
            else
            {
                activeWeapon[0] = true;
            }
            weaponsActive = value;
        }
    }
    public Weapon rearWeapon;
    private bool rearWeapons = false;
    [HideInInspector()]
    public bool RearWeapons { 
        get { return rearWeapons; } 
        set { if(value == true)
            {
                weapons = weapons.Select((w) => { w.Active = false; return w; }).ToArray();
                rearWeapon.Active = true;
            }
            else
            {
                rearWeapon.Active = false;
            }
            rearWeapons = value; 
        } 
    }
    void Start()
    {
        Weapon[] w = gameObject.GetComponents<Weapon>();
        for(int i = 0; i < 3; i++)
        {
            weapons[i] = w[i];
        }
        activeTimes[0] = -1;
    }

    // Update is called once per frame
    void Update()
    {
        if (!rearWeapons)
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                if (activeTimes[i] != -1)
                {
                    if (activeWeapon[i] && Time.time > activeStartTimes[i] + activeTimes[i])
                    {
                        activeWeapon[i] = false;
                    }
                }
            }

            for (int i = weapons.Length - 1; i >= 0; i--)
            {
                if (activeWeapon[i])
                {
                    if (weapons[i].exclusive)
                    {
                        foreach (Weapon w in weapons)
                        {
                            w.Active = false;
                        }
                    }
                    weapons[i].Active = true;
                    i = -1;
                }
            } 
        }
        else
        {

        }
    }
    public void UnlockWeapon(byte weapon, float time)
    {
        if (RearWeapons) return;
        activeWeapon[weapon] = true;
        activeTimes[weapon] = time;
        activeStartTimes[weapon] = Time.time;
    }
}
