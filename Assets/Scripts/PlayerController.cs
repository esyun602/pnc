using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement
    [SerializeField] private float movePower = 3f;
    [SerializeField] private float jumpPower = 3f;

    private Vector3 moveVelocity;
    private Rigidbody2D rig;
    private float customBottom = 0.1f;
    private float customTop = 1f;
    private float customLeft = 0f;
    private float customRight = 1f;

    private Vector2 jumpVelocity;
    private bool canJump = false;
    private int jumpCount = 2;

    private Vector2 tempDir;
    private float dashTime = 0f;
    private bool canDash = false;
    public bool IsMapled { get; set; } = false;

    // Life
    private bool godMode = false;
    private int life = 3;
    private float lastDamagedTime = -10f;
    private const float damageScreenTime = 0.25f;
    private float invincibleTime = 1f;
    private float damageBlinkTerm = 0.25f;

    private BoxCollider2D collider;

    [SerializeField]
    private SpriteRenderer damageScreenSprite;

    private Animator animator;
    private string animationPrefix = "panCake";
    private string CurrentIdleAnimation => animationPrefix + life + "Idle";
    private string CurrentWalkAnimation => animationPrefix + life + "Walk";

    private SpriteRenderer spriteRenderer;
    private float blinkTimePassed = 0f;
    private float nextBlinkAlpha;

    // Start is called before the first frame update
    void Start()
    {
        collider = GetComponent<BoxCollider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rig = gameObject.GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W))
            && jumpCount > 0)
        {
            canJump = true;
		}
        if (IsMapled && Input.GetKeyDown(KeyCode.F))
        {
            IsMapled = false;
            canDash = true;
        }
        ProcessDamageScreen();
        ProcessPanCakeDamageSprite();
    }

    private void ProcessDamageScreen()
    {
        if (Time.time - lastDamagedTime > damageScreenTime)
        {
            var color = damageScreenSprite.color;
            color.a = 0;
            damageScreenSprite.color = color;
            return;
        }

        var ratio = (Time.time - lastDamagedTime) / damageScreenTime;
        ratio = 1 - ratio;

        var tmp = damageScreenSprite.color;
        tmp.a = ratio * 0.2f;
        damageScreenSprite.color = tmp;
    }

    private void ProcessPanCakeDamageSprite()
    { 
        if (Time.time - lastDamagedTime > invincibleTime)
        {
            var color = spriteRenderer.color;
            color.a = 1;
            spriteRenderer.color = color;
            return;
        }

        blinkTimePassed += Time.deltaTime;
        if(blinkTimePassed >= damageBlinkTerm)
        {
            var tmp = spriteRenderer.color;
            tmp.a = nextBlinkAlpha;
            spriteRenderer.color = tmp;

            blinkTimePassed = 0f;
            if(nextBlinkAlpha == 0.5f)
            {
                nextBlinkAlpha = 1f;
            }
            else
            {
                nextBlinkAlpha = 0.5f;
            }
        }
    }

    void FixedUpdate()
	{
        if (!GameManager.Instance.IsInGame)
        {
            return;
        }
        Move();
		Jump();
        PlayerDash();
	}

	private void Move()
	{
        // 움직임 증폭 방지
		moveVelocity = Vector3.zero;

		if (Input.GetKey(KeyCode.A))
        {
			moveVelocity = new Vector3(-1.0f, 0.0f, 0.0f);
		}
			
		else if(Input.GetKey(KeyCode.D))
        {
			moveVelocity = new Vector3(1.0f, 0.0f, 0.0f);
		}

        if(moveVelocity == Vector3.zero)
        {
            animator.Play(CurrentIdleAnimation);
        }
        else
        {
            if(moveVelocity.x < 0)
            {
                spriteRenderer.flipX = true;
            }
            else if(moveVelocity.x > 0)
            {
                spriteRenderer.flipX = false;
            }
            animator.Play(CurrentWalkAnimation);
        }

		var targetPos = transform.position + moveVelocity * movePower * Time.fixedDeltaTime;

        // 이동 제한
        transform.position = ModifyPosToCameraGrid(targetPos);
	}

	private void Jump()
	{
		if (!canJump || jumpCount <= 0)
			return;

        // 점프 횟수 제한
        jumpCount--;

        // 점프 증폭 방지
		rig.velocity = Vector2.zero;

		jumpVelocity = new Vector2(0, jumpPower);
		rig.AddForce(jumpVelocity, ForceMode2D.Impulse);
        canJump = false;
	}

    private void PlayerDash()
    {
        if (!canDash)
            return;

        dashTime += Time.fixedDeltaTime;
        canDash = true;

        // 미끄럼 방지
        rig.velocity = Vector2.zero;
        
        if (tempDir == Vector2.zero)
        {
            tempDir = Vector2.right;
        }
        
        rig.velocity += tempDir.normalized * (rig.velocity * movePower * 80f) * Time.deltaTime;
        rig.velocity = tempDir.normalized * (moveVelocity * movePower * 80f) * Time.deltaTime;

        if (dashTime >= 0.15f)
        {
            dashTime = 0;
            canDash = false;
            // 미끄럼 방지
            rig.velocity = Vector2.zero;
        }
    }

    //TODO: seperation
    private Vector2 ModifyPosToCameraGrid(Vector2 checkPosition)
    {
        var viewportPosition = Camera.main.WorldToViewportPoint(checkPosition);
        viewportPosition.x = Mathf.Clamp(viewportPosition.x, customLeft, customRight);
        viewportPosition.y = Mathf.Clamp(viewportPosition.y, customBottom, customTop);

        return Camera.main.ViewportToWorldPoint(viewportPosition);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        // 땅에 있을 때는 점프 횟수 초기화
        if(other.gameObject.tag == "Ground")
        {
            jumpCount = 2;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // 시럽 장판에 닿았을 때 이동속도 저하
        if(other.gameObject.tag == "Syrup")
        {
            StartCoroutine(SlowDown());
        }  
    }

    private IEnumerator SlowDown()
    {
        movePower = jumpPower = 0.5f;
        yield return new WaitForSeconds(1f);
        movePower = jumpPower = 3f;
    }

    public void Damage()
    {
        if(Time.time - lastDamagedTime < invincibleTime)
        {
            return;
        }

        lastDamagedTime = Time.time;
        blinkTimePassed = damageBlinkTerm;
        nextBlinkAlpha = 0.5f;

        switch (--life)
        {
            case 2:
                collider.size = new Vector2(collider.size.x, 3.5f);
                collider.offset = new Vector2(collider.offset.x, -0.75f);
                break;
            case 1:
                collider.size = new Vector2(collider.size.x, 2.5f);
                collider.offset = new Vector2(collider.offset.x, -1.25f);
                break;
            default:
                //gameover
                break;
        }
    }

    // Life 횟수
    public void AddLife(int count)
    {/*
        if(godMode)
            return;
        //pancake[3-life].setAc
        life += count;
        pancakeImg.sprite = pancakes[life - 1];*/
    }
}
