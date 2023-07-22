using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    public float movePower = 1f;
	public float jumpPower = 1f;

	private Rigidbody2D rig;
	private Vector3 movement;
	private bool isJumping = false;
    private int jumpCount = 2;

    // Start is called before the first frame update
    void Start()
    {
        rig = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButtonDown("Jump") || Input.GetKey(KeyCode.W))
        {
			isJumping = true;
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

    private Vector2 jumpVelocity;
	private void Jump()
	{
		if (!isJumping || jumpCount <= 0)
			return;

        // 점프 횟수 제한
        jumpCount--;

        // 점프 증폭 방지
		rig.velocity = Vector2.zero;

		jumpVelocity = new Vector2(0, jumpPower);
		rig.AddForce(jumpVelocity, ForceMode2D.Impulse);

		isJumping = false;
	}

    
    private void OnCollisionEnter2D(Collision2D other)
    {
        // 땅에 있을 때는 점프 횟수 초기화
        if(other.gameObject.tag == "Ground")
        {
            jumpCount = 2;
        }
    }
}
