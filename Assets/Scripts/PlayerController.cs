using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {
    private Rigidbody2D rbody;
    public LayerMask blockLayer;
    public GameObject gameManager;
    public GameObject player;
    public GameObject titleManager;
    private Animator animator;

    public float MOVE_SPEED;
    private float moveSpeed;
    private float moveSpeedY;
    private float jumpPower = 300;
    private float downPower = 150;
    private bool goJump = false;
    private bool canJump = false;
    private bool due = false;
    private bool idel = false;
    private bool go = false;
    private bool hashiru = false;
    private bool lying = false;
    private bool lyingwork = false;
    private bool lyingdash = false;

    public enum MOVE_DIR
    {
        STOP,
        UP,
        DOWN,
        LEFT,
        RIGHT,
        JUMP,
        ATTACK,
        DAMEGE,
        LEFTDASH,
        RIGHTDASH,
    };

    private MOVE_DIR moveDirection = MOVE_DIR.STOP;

	// Use this for initialization
	void Start () {
        rbody = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
	}

    void Update()
    {
        canJump =
            Physics2D.Linecast(transform.position - (transform.right * 0.3f),
                transform.position - (transform.up * 0.1f), blockLayer) ||
            Physics2D.Linecast(transform.position + (transform.right * 0.3f),
                transform.position - (transform.up * 0.1f), blockLayer);

        float x = Input.GetAxisRaw("Horizontal");
        float xx = Input.GetAxisRaw("Fire3");
        float y = Input.GetAxisRaw("Vertical");

        animator.SetBool("tachi", idel);
        animator.SetBool("work", go);
        animator.SetBool("dash", hashiru);
        animator.SetBool("jump", canJump);
        animator.SetBool("sita", due);
        animator.SetBool("shagamu", lying);
        animator.SetBool("husework", lyingwork);
        animator.SetBool("husedash", lyingdash);
        if ( x == 0)
        {
            lyingdash = false;
            lyingwork = false;
            lying = false;
            idel = true;
            go = false;
            hashiru = false;
            moveDirection = MOVE_DIR.STOP;
        }
        else
        {
            if(x < 0)
            {
                lyingdash = false;
                lyingwork = false;
                lying = false;
                idel = false;
                hashiru = false;
                go = true;
                moveDirection = MOVE_DIR.LEFT;
                if (xx > 0)
                {
                    lyingdash = false;
                    lyingwork = false;
                    lying = false;
                    idel = false;
                    go = false;
                    hashiru = true;
                    moveDirection = MOVE_DIR.LEFTDASH;
                }
            }
            else
            {
                lyingdash = false;
                lyingwork = false;
                lying = false;
                idel = false;
                hashiru = false;
                go = true;
                moveDirection = MOVE_DIR.RIGHT;
                if(xx > 0)
                {
                    lyingdash = false;
                    lyingwork = false;
                    lying = false;
                    idel = false;
                    go = false;
                    hashiru = true;
                    moveDirection = MOVE_DIR.RIGHTDASH;
                }
            }
        }


        if (Input.GetKeyDown("space"))
        {
            if (canJump)
            {
                lyingdash = false;
                lyingwork = false;
                idel = false;
                go = false;
                hashiru = false;
                lying = false;
                goJump = true;
            }
        }
        if(canJump == false)
        {
            if(y < 0)
            {
                 due = true;
            }
            else
            {
                due = false;
            }
        }
        if (canJump)
        {
            if(y < 0)
            {
                lyingdash = false;
                idel = false;
                lying = true;
                if(x < 0)
                {
                    lyingdash = false;
                    lyingwork = true;
                    moveDirection = MOVE_DIR.LEFT;
                    if(xx > 0)
                    {
                        lyingdash = true;
                        moveDirection = MOVE_DIR.LEFTDASH;
                    }
                }
                else if(x > 0)
                {
                    lyingdash = false;
                    lyingwork = true;
                    moveDirection = MOVE_DIR.RIGHT;
                    if(xx > 0)
                    {
                        lyingdash = true;
                        moveDirection = MOVE_DIR.RIGHTDASH;
                    }
                }
            }
        }
    }

    // Update is called once per frame
    void FixedUpdate () {
        switch (moveDirection)
        {
            case MOVE_DIR.STOP:
                moveSpeed = 0;
                break;
            case MOVE_DIR.LEFT:
                moveSpeed = MOVE_SPEED * -1;
                transform.localScale = new Vector2(-0.2f, 0.2f);
                break;
            case MOVE_DIR.RIGHT:
                moveSpeed = MOVE_SPEED * 1;
                transform.localScale = new Vector2(0.2f, 0.2f);
                break;
            case MOVE_DIR.LEFTDASH:
                moveSpeed = MOVE_SPEED * -1.5f;
                transform.localScale = new Vector2(-0.2f, 0.2f);
                break;
            case MOVE_DIR.RIGHTDASH:
                moveSpeed = MOVE_SPEED * 1.5f;
                transform.localScale = new Vector2(0.2f, 0.2f);
                break;
            case MOVE_DIR.UP:
                moveSpeedY = MOVE_SPEED * 1;
                break;
            case MOVE_DIR.DOWN:
                moveSpeedY = MOVE_SPEED * -1;
                break;
        }

        rbody.velocity = new Vector2(moveSpeed, rbody.velocity.y);

        if (goJump)
        {
            rbody.AddForce(Vector2.up * jumpPower);
            goJump = false;
        }

        if (due)
        {
            rbody.AddForce(Vector2.left * -100);
            rbody.AddForce(Vector2.down * downPower);
            due = false;
        }

    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if(col.gameObject.tag == "New")
        {
            titleManager.GetComponent<TitleManager>().NewGame();
            DestroyPlayer();
        }
        if(col.gameObject.tag == "Select")
        {
            titleManager.GetComponent<TitleManager>().StageSelect();
            DestroyPlayer();
        }
        if(col.gameObject.tag == "Trap")
        {
            gameManager.GetComponent<GameManager>().GameOver();
            DestroyPlayer();
        }

        if (col.gameObject.tag == "Next")
        {
                gameManager.GetComponent<GameManager>().GameNext2();
        }

        if (col.gameObject.tag == "Next2")
        {
            gameManager.GetComponent<GameManager>().GameNext3();
        }

        if(col.gameObject.tag == "Goal")
        {
            gameManager.GetComponent<GameManager>().GameClear();
        }

        if (col.gameObject.tag == "Enemy")
        {
            if(transform.position.y > col.gameObject.transform.position.y + 0.3f)
            {
                rbody.velocity = new Vector2(rbody.velocity.x, 0);
                rbody.AddForce(Vector2.up * jumpPower);
                col.gameObject.GetComponent<EnemyManager>().DestroyEnemy();
            }
            else
            {
                gameManager.GetComponent<GameManager>().GameOver();
                DestroyPlayer();
            }
        }
    }

    void DestroyPlayer()
    {
        Destroy(this.gameObject);
    }
}
