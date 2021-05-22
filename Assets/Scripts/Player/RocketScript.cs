using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RocketScript : MonoBehaviour
{
    //Seek for target
    private int detectRadius = 4;    
    private float disappearTime = 10;   //Time for rocket to find target, otherwise it will explode
    private float seekingTime = 0;

    //Following target
    private float detectExplosion = 0.5f;
    private float giveUpTime = 15;  //If chase lasts too long, rocket needs to find other target
    private float followingTime = 0;
    private float speedIncrease = 0.5f;
    private float currentSpeed;
    //Exploding 
    int dmgModificator = 3;
    int dmg;

    //Circulate
    //private float circulatingTime = 0;
    //private float timeToGo = 1; 
    //private float maxSearchRadius = 20f;

    //Basic
    int baseSpeed = 7;
    private Collider2D targetCollider;
    private Transform targetTransform;

    private enum RocketState
    {
        seeking,
        following,
        circling
    }

    RocketState state = RocketState.seeking;


    void Update()
    {
        switch(state)
        {
            case RocketState.seeking:
                SeekForTarget();
                break;
            case RocketState.following:
                FollowTarget();
                break;
            case RocketState.circling:
                Circulate();
                break;
        }
    }

    public void GetSender(string s, int dmg)
    {
        this.dmg = dmg * dmgModificator;
    }

   
    void SeekForTarget()
    {
        //Ruch rakiety do przodu
        transform.Translate(transform.up * Time.deltaTime * baseSpeed, Space.World);

        //Sprawdzanie czy obok rakiety jest jakic cel
        targetCollider = Physics2D.OverlapCircle(transform.position, detectRadius);

        //Zmiana trybu na Śledzenie
        if(targetCollider != null && targetCollider.gameObject.tag == "Enemy")
        {
            seekingTime = 0;
            currentSpeed = baseSpeed;
            targetTransform = targetCollider.GetComponent<Transform>();
            state = RocketState.following;           
        }

        //Jeśli rakieta za długo szuka celu, wybucha
        seekingTime += Time.deltaTime;
        if(seekingTime >= disappearTime)
        {
            Explode();
        } 
    }

    void FollowTarget()
    {
        //Sprawdzanie czy rakieta ma cel do śledzenia
        if(targetTransform == null)
        {
            state = RocketState.seeking;
            followingTime = 0f;
            return;
        }

        //Podążanie za celem
        Vector2 dir = targetTransform.position - transform.position;
        transform.Translate(dir.normalized * currentSpeed * Time.deltaTime, Space.World);

        //Nabieranie predkosci
        currentSpeed += followingTime * speedIncrease;

        //Celowanie w cel
        Vector2 lookDirection = targetTransform.position - transform.position;
        float angle = Mathf.Atan2(lookDirection.y, lookDirection.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle -90, Vector3.forward);
        
        //Sprawdzanie czy rakieta jest wystarczajoco blisko celu, jesli tak wybucha
        if (Vector2.Distance(transform.position, targetTransform.position) <= detectExplosion)
        {
            Explode();
        }       

        //Jesli czas podazania za celem jest zbyt duzy, zacznij szukac nowego celu
        followingTime += Time.deltaTime;
        if(followingTime >= giveUpTime)
        {
            state = RocketState.seeking;
        }

    }

    void Circulate()
    {
        //Rakieta bedzie krazyc zanim znajdzie nowego przeciwnika

    }


    void Explode()
    {
        // Instantiate some particles
               
        if(targetTransform != null)
        {
            targetTransform.GetComponent<Enemy>().TakeDamage(dmg);
        }

        Destroy(this.gameObject);
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, detectExplosion);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, detectRadius);
    }

}
