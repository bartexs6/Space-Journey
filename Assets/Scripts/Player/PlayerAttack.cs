using System.Collections;
using System.Collections.Generic;
using UnityEngine;

class PlayerAttack : MonoBehaviour
{
    public Transform[] firePoints;
    public GameObject bullet;

    public GameObject rocketPrefab;

    public float bulletForce, bulletPush;

    private CameraScript cum;

    void Start()
    {
        cum = Camera.main.GetComponent<CameraScript>();
    }

    public void Skill_Rocket()
    {
        GameObject rocket = Instantiate(rocketPrefab, transform.position, transform.rotation);
        rocket.GetComponent<RocketScript>().GetSender(tag, Game.getPlayer().getDmg());
        
    }

    public void PrimaryAttack()
    {
        for (int i = 0; i < firePoints.Length; i++)
        {
            GameObject laser = Instantiate(bullet, firePoints[i].position, firePoints[i].rotation);
            laser.GetComponent<Rigidbody2D>().AddForce(firePoints[i].up * bulletForce, ForceMode2D.Impulse);
            laser.GetComponent<BulletScript>().GetSender(tag, Game.getPlayer().getDmg());
            Game.getPlayer().getPlayerRigidbody().AddForce(firePoints[i].up * -bulletPush, ForceMode2D.Impulse);
        }

        cum.CameraShake();
    }
}
