using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerScript : MonoBehaviour
{
    private Rigidbody2D rd2d;
    public float speed;
    public float jumpF;
    public int lives;
    public Text score;
    public int scoreValue = 0;
    public Animator playerAnim;
    public SpriteRenderer playerSprite;

    // Start is called before the first frame update
    void Start()
    {
        rd2d = GetComponent<Rigidbody2D>();
        score.text = scoreValue.ToString();
        lives=3;
        score.text = ("Score: ") + scoreValue + (" Lives: ") + lives + ("/3");
    }
    void Update()
    {

        score.text = ("Score: ") + scoreValue + (" Lives: ") + lives + ("/3");

        if (rd2d.velocity.y > .1f || rd2d.velocity.y < -.1f)
        {
            playerAnim.SetBool("onGround", false);
        }
        else if (rd2d.velocity.y == 0f)
        {
            playerAnim.SetBool("onGround", true);
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }
    public void Move()
    {
        float hozMovement = Input.GetAxis("Horizontal");
        float vertMovement = Input.GetAxis("Vertical");
        rd2d.velocity = new Vector2 (hozMovement * speed, rd2d.velocity.y);
        
        if(rd2d.velocity.x > .1f || rd2d.velocity.x < -.1f)
        {
            playerAnim.SetBool("isMoving", true);
        }
        else
        {
            playerAnim.SetBool("isMoving", false);
        }
        
        //rd2d.AddForce(new Vector2(hozMovement * speed, 0f));
    }
    private void OnTriggerEnter2D(Collider2D pickup)
    {
        if (pickup.tag == "Coin")
        {
            scoreValue += 1;
            score.text = ("Score: ") + scoreValue + (" Lives: ") + lives + ("/3");
            Destroy(pickup.gameObject);
        }
        if (pickup.tag == "Hazard")
        {
            lives -= 1;
            score.text = ("Score: ") + scoreValue + (" Lives: ") + lives + ("/3");
            Destroy(pickup.gameObject);
        }

    }
    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ground")
        {
            Debug.Log("Touching Ground");
            if (Input.GetKey(KeyCode.W))
            {
                rd2d.velocity = new Vector2 (rd2d.velocity.x , jumpF);
                //rd2d.AddForce(new Vector2(0,15f), ForceMode2D.Impulse);
            }
        }
    }
}