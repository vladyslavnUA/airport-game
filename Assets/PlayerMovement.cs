using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    public Animator animator;
    public float movementSpeed;
    public float jumpHeight;
    public float item;
    public Text itemText;
    // public int Grounded;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        itemText.text = "Items Collected: " + item.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxis("Horizontal") * movementSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        if (Input.GetKey(KeyCode.UpArrow) && Grounded == 1)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpHeight);
        }
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody2D myRigidbody;
    public Animator animator;
    public float movementSpeed;
    public float jumpHeight;
    public float health;
    public Text healthText;
    public int Grounded;
    private float time;

    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = GetComponent<Rigidbody2D>();
        healthText.text = "Health: " + health.ToString() + " XP";
    }

    // Update is called once per frame
    void Update() 
    {
        float horizontal = Input.GetAxis("Horizontal") * movementSpeed;
        animator.SetFloat("Speed", Mathf.Abs(horizontal));

        // float vertical = Input.GetAxisRaw("Vertical")
        
        if (Input.GetKey(KeyCode.UpArrow) && Grounded == 1)
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, jumpHeight);
            Debug.Log("JUMP");
            Grounded = 0;
            // animator.Play("jump_ani");
        } 
        // else {
        //     Grounded = 1;
        //     Debug.Log("nuthin' to see here");
        // }

        if (Input.GetKey(KeyCode.RightArrow))
        {
            // myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, vertical * movementSpeed);
            myRigidbody.velocity = new Vector2(movementSpeed, myRigidbody.velocity.y);
            // = Input.GetAxis("Horizontal");
        }

        // GetKeyDown will omit going back
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            myRigidbody.velocity = new Vector2(-movementSpeed, myRigidbody.velocity.y);
        }

        if (Input.GetKey(KeyCode.DownArrow))
        {
            animator.Play("crouch");
            // myRigidbody.velocity = new Vector2(-movementSpeed, myRigidbody.velocity.y);
        } 
        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            animator.Play("idle_anim");
        }
    }

    void OnCollisionEnter2D(Collision2D other){
        if (other.gameObject.tag == "ground") {
        // if (other.gameObject.CompareTag("ground")){
            // 1 means on the ground
            Grounded = 1;
            Debug.Log("Found Ground");
        }
    }

    void OnCollisionExit2D(Collision2D other){
        if (other.gameObject.tag == "ground") {
        // if (other.gameObject.CompareTag("ground")){
            // 0 means in the air
            Grounded = 0;
            Debug.Log("Found Air");
        }
    }

    // public void DoThisNow(int num)
    // {
    //     if(num <= 3){
    //         num *= 8;
    //         Debug.Log(num);
    //     }
    // }

    // static void PrintPls(){
    //     DoThisNow(4);
    //     Debug.Log("Return this");
    // }

    void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "enemy")
        {
            Debug.Log("fooudn it");
            Damaged();
            Debug.Log("done it");
        }
    }

    IEnumerator doit ()
    {
        time = 0.02f * Time.deltaTime;
        yield return new WaitForSeconds(time);
        health -= 5;
        healthText.text = "Health: " + health.ToString() + " XP";
        // yield return new WaitForSeconds(0.02f);
    }


    public void Damaged(){
        StartCoroutine(doit());

        if (health <= 0){
            Debug.Log("reached 0");
            GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerPos>().PlayDead();
            // player.GetComponent<PlayerPos>().PlayDead();
        }
    }
}