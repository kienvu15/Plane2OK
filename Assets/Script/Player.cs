using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody2D rb;
    Animator anim;

    public float moveSpeed = 6f;
    private float moveX, moveY;

    public GameObject Laser;
    public GameObject Pivot;
    public GameObject Pivot21;
    public GameObject Pivot22;
    public GameObject Pivot31;
    public GameObject Pivot32;

    public float bulletSpeed = 15f;

    private bool isHoldingKey = false;
    private bool isAnimationEnded = false;

    private float upgradePoint = 0;
    public int scoreValue = 10;

    public float maxHeath = 3;
    public GameObject LoseCanvas;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        moveMent();
        TurnLoR();
        Shot();
        
    }

    public void moveMent()
    {
        moveX = Input.GetAxisRaw("Horizontal");
        moveY = Input.GetAxisRaw("Vertical");
        Vector2 movement = new Vector2(moveX, moveY).normalized;
        rb.velocity = movement * moveSpeed;
    }
    public void TurnLoR()
    {
        if (moveX < 0) 
        {
            isHoldingKey = true;
            isAnimationEnded = false;
            anim.SetBool("isMoving", true);
            anim.SetFloat("moveX", moveX);
            anim.SetTrigger("goLeft");
        }
        else if (moveX > 0)
        {
            isHoldingKey = true;
            isAnimationEnded = false;
            anim.SetBool("isMoving", true);
            anim.SetFloat("moveX", moveX);
            anim.SetTrigger("goRight");
        }
        else
        {
            isHoldingKey = false;
            if (!isAnimationEnded)
            {
                anim.SetBool("isMoving", false);
            }
            anim.speed = 1;
        }
    }
    public void OnAnimationEnd()
    {
        isAnimationEnded = true;
        if (isHoldingKey)
        {
            anim.speed = 0;
        }
        else
        {
            anim.speed = 1;
            anim.SetBool("isMoving", false);
        }
    }

    public void Shot()
    {
        if ((Input.GetKeyDown(KeyCode.Space)) && (upgradePoint==0))
        {
            GameObject shot = Instantiate(Laser, Pivot.transform.position, Quaternion.identity);
            Rigidbody2D laser = shot.GetComponent<Rigidbody2D>();

            laser.velocity = new Vector2(0, bulletSpeed);
            Destroy(shot, 1.5f);
        }
        if ((Input.GetKeyDown(KeyCode.Space)) && (upgradePoint == 1))
        {
            GameObject shot1 = Instantiate(Laser, Pivot21.transform.position, Quaternion.identity);
            GameObject shot2 = Instantiate(Laser, Pivot22.transform.position, Quaternion.identity);
            Rigidbody2D laser1 = shot1.GetComponent<Rigidbody2D>();
            Rigidbody2D laser2 = shot2.GetComponent<Rigidbody2D>();

            laser1.velocity = new Vector2(0, bulletSpeed);
            laser2.velocity = new Vector2(0, bulletSpeed);
        }
        if ((Input.GetKeyDown(KeyCode.Space)) && (upgradePoint == 2))
        {
            GameObject shot1 = Instantiate(Laser, Pivot21.transform.position, Quaternion.identity);
            GameObject shot2 = Instantiate(Laser, Pivot22.transform.position, Quaternion.identity);
            GameObject shot3 = Instantiate(Laser, Pivot.transform.position, Quaternion.identity);
            Rigidbody2D laser1 = shot1.GetComponent<Rigidbody2D>();
            Rigidbody2D laser2 = shot2.GetComponent<Rigidbody2D>();
            Rigidbody2D laser = shot3.GetComponent<Rigidbody2D>();

            laser1.velocity = new Vector2(0, bulletSpeed);
            laser2.velocity = new Vector2(0, bulletSpeed);
            laser.velocity = new Vector2(0, bulletSpeed);
        }
        if ((Input.GetKeyDown(KeyCode.Space)) && (upgradePoint >= 3))
        {
            GameObject shot1 = Instantiate(Laser, Pivot21.transform.position, Quaternion.identity);
            GameObject shot2 = Instantiate(Laser, Pivot22.transform.position, Quaternion.identity);
            GameObject shot3 = Instantiate(Laser, Pivot.transform.position, Quaternion.identity);
            GameObject shot4 = Instantiate(Laser, Pivot31.transform.position, Quaternion.Euler(0,0,-20));
            GameObject shot5 = Instantiate(Laser, Pivot32.transform.position, Quaternion.Euler(0, 0, 20));
            Rigidbody2D laser1 = shot1.GetComponent<Rigidbody2D>();
            Rigidbody2D laser2 = shot2.GetComponent<Rigidbody2D>();
            Rigidbody2D laser = shot3.GetComponent<Rigidbody2D>();
            Rigidbody2D laser3 = shot4.GetComponent<Rigidbody2D>();
            Rigidbody2D laser4 = shot5.GetComponent<Rigidbody2D>();

            float angle3 = -20f * Mathf.Deg2Rad;
            float angle4 = 20f * Mathf.Deg2Rad;  

            laser3.velocity = new Vector2(Mathf.Sin(angle3) * bulletSpeed, Mathf.Cos(angle3) * bulletSpeed);
            laser4.velocity = new Vector2(Mathf.Sin(angle4) * bulletSpeed, Mathf.Cos(angle4) * bulletSpeed);

            laser1.velocity = new Vector2(0, bulletSpeed);
            laser2.velocity = new Vector2(0, bulletSpeed);
            laser.velocity = new Vector2(0, bulletSpeed);
        }

    }
    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Upgrade"))
        {
            upgradePoint++;
            ScoreManager.Instance.AddScore(scoreValue);
            Destroy(collision.gameObject);
        }
        if (collision.CompareTag("Rock"))
        {
            maxHeath--;
            if(maxHeath <= 0)
            {
                LoseCanvas.SetActive(true);
                Time.timeScale = 0;
            }
        }
    }
    public void PlayAgain()
    {
        SceneManager.LoadScene(0);
        Time.timeScale = 1;
    }
}