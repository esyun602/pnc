using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    // Movement
    [SerializeField] private float movePower = 3f;
    [SerializeField] private float jumpPower = 3f;

    private Vector3 movement;
    private Rigidbody2D rig;

    private Vector2 jumpVelocity;
    private bool canJump = false;
    private int jumpCount = 2;

    // Life
    private int life = 3;
    private SpriteRenderer pancakeImg;
    [SerializeField] private Sprite[] pancakes;

    // Start is called before the first frame update
    void Start()
    {
        rig = gameObject.GetComponent<Rigidbody2D>();
        pancakeImg = gameObject.GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.W))
            && jumpCount > 0)
        {
            canJump = true;
		}
    }
    void FixedUpdate()
	{
		Move();
		Jump();
	}

    private Vector3 moveVelocity;
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

		transform.position += moveVelocity * movePower * Time.fixedDeltaTime;
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

    // Life 횟수
    public void AddLife(int count)
    {
        life += count;
        pancakeImg.sprite = pancakes[life - 1];
    }
}
