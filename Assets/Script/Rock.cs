using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rock : MonoBehaviour
{
    Rigidbody2D rb;
    PolygonCollider2D PolygonCollider2D;
    Animator anim;
    public float maxHealth = 3f;

    public GameObject upGrade;
    [Range(0f, 1f)] public float dropChance = 0.8f;
    public int scoreValue = 10;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        PolygonCollider2D = GetComponent<PolygonCollider2D>();
    }

    void Update()
    {
    }

    private void Die()
    {
        anim.SetBool("Die", true);
        PolygonCollider2D.enabled = false;
         rb.gravityScale = 0f;
        ScoreManager.Instance.AddScore(scoreValue);
    }

    public void On2Destroy()
    {
        Destroy(gameObject);
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Laser"))
        {
            maxHealth--;
        }
        if (maxHealth <= 0)
        {
            Die();
            if (Random.Range(0f, 1f) < dropChance)
            {
                Instantiate(upGrade, transform.position, Quaternion.identity);
                
            }
        }
        
        if (collision.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("Limit"))
        {
            Destroy(gameObject);
        }
    }

}
