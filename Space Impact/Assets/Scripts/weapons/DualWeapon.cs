using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class DualWeapon : Weapon
{
    public Weapon[] weapons;
    int phase;
    int previousPhase;
    public float[] phaseDurations;
    bool waiting = false;
    float phaseStart;
    public bool useAtSameTime;

    protected override void Update()
    {
        if (!Active)
        {
            weapons.ToList().ForEach(w => w.Active = false);
        }
        base.Update();
    }

    protected override void useWeapon()
    {
        if (enemy)
        {
            foreach (Weapon w in weapons)
            {
                w.enemy = true;
            }
        }
        
        if(Active)
        {
            if (useAtSameTime)
            {
                weapons.ToList().ForEach(w => w.Active = true);
                return;
            }
            switch (waiting)
            {
                case false:
                    weapons[phase].Active = true;
                    previousPhase = phase;
                    if(phase < weapons.Length - 1)
                    {
                        phase++;
                    }
                    else
                    {
                        phase = 0;
                    }
                    phaseStart = Time.time;
                    waiting = true;
                    break;
                case true:
                    if(Time.time > phaseStart + phaseDurations[previousPhase])
                    {
                        weapons[previousPhase].Active = false;
                        waiting = false;
                    }
                    break;
            }
        }
        
    }
}
