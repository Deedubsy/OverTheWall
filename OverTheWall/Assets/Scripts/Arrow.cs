using OverTheWall.EnemyController;
using OverTheWall.Projectile;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : Projectile
{
    private float damage = 5;
    private bool canCauseDamage = true;

    public void InitializeArrow(Vector2 sp, Vector2 tp, float speed)
    {
        Initialize(ProjectileType.Straight, sp, tp, 30, damage);
    }

	// Use this for initialization
	void Start ()
    {
	    
	}

    private void Update()
    {
        this.UpdatePosition();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject != null)
        {
            if (!string.IsNullOrEmpty(collision.gameObject.tag))
            {
                if (collision.gameObject.tag.ToLower() == "enemy")
                {
                    collision.gameObject.GetComponent<EnemyController>();

                    Destroy(this);
                }
                else if (collision.gameObject.tag.ToLower() == "ground")
                {
                    Destroy(this);
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject != null)
        {
            if (!string.IsNullOrEmpty(collision.gameObject.tag))
            {
                if (collision.gameObject.tag.ToLower() == "enemy" && canCauseDamage)
                {
                    EnemyController enemy = collision.gameObject.GetComponent<EnemyController>();

                    enemy.OnHit(damage);

                    canCauseDamage = false;

                    Destroy(this);
                }
                else if (collision.gameObject.tag.ToLower() == "ground")
                {
                    Destroy(this);
                }
            }
        }
    }

    //// Update is called once per frame
    //new

    //// Update is called once per frame
    //void Update()
    //{

    //}
}
