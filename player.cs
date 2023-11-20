using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{

    Animator anim;
    Rigidbody rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
    }

    void Update()
    {
        Move();
    }

    void Move()
    {
        public float moveSpeed = 5.0f; // 이동 속도
        public Camera playerCamera; // 3인칭 카메라
        float horizontalInput;
        float verticalInput;
        // 카메라가 바라보는 방향을 기준으로 이동 벡터 계산
        Vector3 cameraForward = playerCamera.transform.forward;
        cameraForward.y = 0; // 수직 이동 방지
        Vector3 moveDirection = cameraForward.normalized;

        // 키 입력으로 이동
        Vector3 movement = (moveDirection * verticalInput + playerCamera.transform.right * horizontalInput).normalized;

        // 이동 벡터에 이동 속도를 곱하여 속도 벡터 생성
        Vector3 velocity = movement * moveSpeed;

        // Rigidbody를 사용하여 이동
        rigid.velocity = new Vector3(velocity.x, rigid.velocity.y, velocity.z);


        anim.SetBool("Walk", !(horizontalInput == 0 && verticalInput == 0));
        anim.SetBool("Run", running);

        transform.LookAt(transform.position + new Vector3(velocity.x, 0, velocity.z));
    }
  
}
       