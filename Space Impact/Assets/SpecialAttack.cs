using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpecialAttack : MonoBehaviour
{
    protected GameObject player;
    public Image hidingBox;
    public byte weapon;
    public float time;
    public float reloadSpeed;
    public float timer;
    void Start()
    {
        player = GameObject.Find("PlayerShip");
        timer = reloadSpeed * -1;
    }
    void Update()
    {
        if(Time.time < timer + reloadSpeed)
        {
            float reloadPercent = ((Time.time - timer) / reloadSpeed);
            //hidingBox.rectTransform.offsetMax = new Vector2(0, -reloadPercent);
            hidingBox.fillAmount = 1 - reloadPercent;

            //hidingBox.rectTransform.sizeDelta = new Vector2(128, 128 - reloadPercent);
            //hidingBox.rectTransform.anchoredPosition = new Vector2(0, -reloadPercent / 2);
        }
        else
        {
            //hidingBox.rectTransform.offsetMax = new Vector2(0, -128);
            hidingBox.fillAmount = 0;

            //hidingBox.rectTransform.sizeDelta = new Vector2(128, 0);
            //hidingBox.rectTransform.anchoredPosition = new Vector2(0, 0);
        }
    }
    public void UseSpecialAttack()
    {
        if (Time.time >= timer + reloadSpeed && player.GetComponent<PlayerWeaponManager>().WeaponsActive && !player.GetComponent<PlayerWeaponManager>().RearWeapons)
        {
            player.GetComponent<PlayerWeaponManager>().UnlockWeapon(weapon, time);
            timer = Time.time;
            //hidingBox.rectTransform.offsetMax = new Vector2(0, 0);
            hidingBox.fillAmount = 1;
            //hidingBox.GetComponent<RectTransform>().position = new Vector3(0, 0);
            //hidingBox.rectTransform.sizeDelta = new Vector2(128, 128);
            //hidingBox.rectTransform.anchoredPosition = new Vector2(0, -64);
        }
        else {

        }
    }
}
