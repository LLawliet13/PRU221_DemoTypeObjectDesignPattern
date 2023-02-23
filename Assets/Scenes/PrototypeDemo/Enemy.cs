using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public int attackDamage;
    public float moveSpeed;
    public GameObject deathEffect;
    public new Enemy gameObject;

    public void Attack()
    {
        // Attack logic here
    }
    public Enemy() : base()
    {
    }

    public void TakeDamage(int damageAmount)
    {
        health -= damageAmount;
        if (health <= 0)
        {
            Die();
        }
    }

    private void Die()
    {
        gameObject.Clone();
        Instantiate(deathEffect, transform.position, transform.rotation);
        Destroy(gameObject);
    }

    public Enemy Clone()
    {
        return Instantiate(this);
    }
}

