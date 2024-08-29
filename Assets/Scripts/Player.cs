using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float speed;
    public float movementX;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    public GameObject bulletPrefab;
    // public Bullet bigBulletPrefab;
    public int maxBulletCount = 10; // Giới hạn số lượng đạn
    public int currentBulletCount = 0;
    // private List<Bullet> bullets = new List<Bullet>();
    public float isHoldingX = 1;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {


        //di chuyen trai phai


        // movementX = Input.GetAxis("Horizontal");
        // transform.position += new Vector3(movementX, 0f, 0f) * speed * Time.deltaTime; 


        // if(Input.GetAxis("Horizontal") != 0){
        //     // Vector2 vector;
        //     // vector.x  = Input.GetAxis("Horizontal");
        // }   

        //Bullet
        if (Input.GetKeyDown(KeyCode.Q) && currentBulletCount < maxBulletCount)
        {
            if (spriteRenderer.flipX )
            {
                Vector3 playerPosition = transform.position;
                playerPosition.x -= 5f;
                GameObject bullet = Instantiate(bulletPrefab, playerPosition, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = Vector2.left * 0.5f;
                maxBulletCount--;
            }
            else{
                Vector3 playerPosition = transform.position;
                playerPosition.x += 5f;
                GameObject bullet = Instantiate(bulletPrefab, playerPosition, Quaternion.identity);
                bullet.GetComponent<Rigidbody2D>().velocity = Vector2.right * 0.5f;
                maxBulletCount--;
            }
        }

        if(Input.GetKey(KeyCode.W))isHoldingX += 1f;
            if (Input.GetKeyUp(KeyCode.W))
            {
                if (spriteRenderer)
                {
                    Vector3 playerPosition = transform.position;
                    playerPosition.x -= 1f;
                    playerPosition.y += 1f;
                    GameObject bottle = Instantiate(bulletPrefab, playerPosition, Quaternion.identity);
                    if (isHoldingX > 4f)
                    {
                        bottle.transform.localScale = new Vector3(4, 4, 0);
                    }
                    else
                    {
                        bottle.transform.localScale = new Vector3(isHoldingX, isHoldingX, 0f);
                    }
                    bottle.GetComponent<Rigidbody2D>().velocity = Vector2.left * 10;

                }
                isHoldingX= 1f;
            }


        if (Input.GetKeyDown(KeyCode.E) && currentBulletCount < maxBulletCount)
        {
            Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, 1f);
            foreach (Collider2D col in colliders)
            {
                if (col.CompareTag("Bullet") && currentBulletCount < maxBulletCount)
                {
                    Destroy(col.gameObject);
                    currentBulletCount++;
                }
            }
        }


        //di chuyen trai-phai
        Vector2 move = rb.velocity;
        move.x = Input.GetAxis("Horizontal") * speed;
        rb.velocity = move;


        //nhay len  
        if(Input.GetButtonDown("Jump")){
            rb.AddForce(new Vector2(0, 2), ForceMode2D.Force);
        }


    }
}
