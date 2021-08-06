using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float startSpeed = 10f;

    [HideInInspector]
    public float speed ;

    public float health = 100;

    public int worth = 50;
    public GameObject deathEffect;


    void Start()
    {
        speed = startSpeed;
    }
    public void takeDamge(float amount)
    {
        health -= amount;
        if (health <= 0)
        {
            Die();
        }
    }

    public void Slow(float pct)
    {
        speed = startSpeed * (1f - pct);
    }

    void Die()
    {
        GameObject  effect=(GameObject)Instantiate(deathEffect,transform.position,Quaternion.identity);
        Destroy(effect, 5f);
        PlayerStats.Money += worth;
        Destroy(gameObject);
    }

    
}
