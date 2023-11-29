using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player : MonoBehaviour
{
    public float moveSpeed = 5.0f; 
    public Camera playerCamera; 
    float horizontalInput;
    float verticalInput;

    Animator anim;
    Rigidbody rigid;

    void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    bool running;
    bool jumping;
    bool eatWeapon;
    bool swap1;
    bool swap2;
    bool shooting;
    bool reroad;

    void GetInput()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");

        jumping = Input.GetButtonDown("Jump");
        running = Input.GetButton("Run");
        eatWeapon = Input.GetButtonDown("Eat");
        swap1 = Input.GetButtonDown("Swap1");
        swap2 = Input.GetButtonDown("Swap2");
        shooting = Input.GetButtonDown("Fire1");
        reroad = Input.GetButtonDown("Reroad");
    }

    void Update()
    {
        if (!dead)
        {
            GetInput();
            Move();
            jump();
            eat();
            swap();
            shoot();
            reroading();
        }
        die();
    }

    void Move()
    {
        if( !isreroad )
        {
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
  
    public float JumpPower;
    bool Isjump;

    void jump()
    {
        if (jumping && !Isjump)
        {
            rigid.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
            Isjump = true;
        }
    }
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "City")
        {
            Isjump = false; //점프 중에 또 점프 못하게 하려고
        }
    }

    GameObject nearObject;

    public GameObject[] weapon;
    public bool[] hasweapon;

    void eat()
    {
        if(eatWeapon && nearObject != null )
        {
            Item item = nearObject.GetComponent<Item>();
            int number = item.number;
            hasweapon[number] = true;

            Destroy(nearObject);
        }
    }

    weapon equipWeapon;

    void swap()
    {
        int Index = -1;
        if (swap1) Index = 0;
        if (swap2) Index = 1;
        if (swap1 && !hasweapon[0])
            return;
        if (swap2 && !hasweapon[1])
            return;

        if (swap1||swap2 && !jumping)
        {
            if (equipWeapon != null)
            {
                equipWeapon.gameObject.SetActive(false);
            }

            equipWeapon = weapon[Index].GetComponent<weapon>();
            weapon[Index].SetActive(true);
        }
    }
    
    public float fireDelay;
    bool fireReady;

    void shoot()
    {
        fireDelay += Time.deltaTime;
        fireReady = equipWeapon.rate < fireDelay;

        if(shooting && fireReady && !running && !swap1 && !swap2)
        {
            equipWeapon.use();
            anim.SetTrigger("DoShoot");
            fireDelay = 0; 
        }
    }

    public int ammo1;
    public int ammo2;
    public int health = 100;

    public int maxAmmo1;
    public int maxAmmo2;
    public int maxHealth = 100;

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Item")
        {
            Item item = other.GetComponent<Item>();
            switch(item.Itemtype)
            {
                case Item.Type.ball1:
                    ammo1 += item.number;
                    if (ammo1 > maxAmmo1)
                        ammo1 = maxAmmo1;
                    break;
                case Item.Type.ball2:
                    ammo2 += item.number;
                    if (ammo2 > maxAmmo2)
                        ammo2 = maxAmmo2;
                    break;
                case Item.Type.health:
                    health += item.number;
                    if (health > maxHealth)
                        health = maxHealth;
                    break;
            }
            Destroy(other.gameObject);
        }
    }

    bool isreroad;
    void reroading()
    {
        if (equipWeapon == null)
            return;

        if (running || swap1 || swap2)
            return;
        if (ammo1 == 0 && ammo2 == 0)
            return;
        if(reroad)
        {
            anim.SetTrigger("DoRoad");
            isreroad = true;

            Invoke("reroadout", 2f);
        }


    }

    void reroadout()
    {
        int reAmmo1 = ammo1 < equipWeapon.maxAmmo ? ammo1 : equipWeapon.maxAmmo;
        equipWeapon.curAmmo = reAmmo1;
        ammo1 -= reAmmo1;
        int reAmmo2 = ammo2 < equipWeapon.maxAmmo ? ammo2 : equipWeapon.maxAmmo;
        equipWeapon.curAmmo = reAmmo2;
        ammo2 -= reAmmo2;

        isreroad = false;
    }

    void alive()
    {
        dead = false;
    }

    bool dead = false;
    void die()
    {
        Debug.Log("다이함수안에 들어왔어 ");
        if(health <= 0 && !dead)
        {
            Debug.Log("다이함수 안에 이프문 안에 들어왔어 ");
            anim.SetTrigger("DoDie");

            rigid.velocity = Vector3.zero;
            rigid.isKinematic = true;

            dead = true;
        }
    }
}       