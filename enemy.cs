using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class enemy : MonoBehaviour
{
    public static int kill = 0;

    public float MaxHealth;
    public float CurHealth;
    public int attack;
    public Transform target;
    bool chasing;



    NavMeshAgent nav;

    Rigidbody rigid;
    BoxCollider collider;

    public Sprite attacksprite;
    public Sprite deadsprite;
    SpriteRenderer sp;
    Sprite issprite;
    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
        collider = GetComponent<BoxCollider>();
        nav = GetComponent<NavMeshAgent>();
        sp = GetComponent<SpriteRenderer>();
        issprite = this.sp.sprite;

        Invoke("chaseStart", 2f);

    }

    void chaseStart()
    {
        chasing = true;
    }

    private void Update()
    {
        if (chasing)
        {
            nav.SetDestination(target.position);
        }

       
    }

    public static float bossHealth;
    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "bullet")
        {
            bullet bullet = other.GetComponent<bullet>();
            CurHealth -= bullet.Damage;
            bossHealth = CurHealth;

            Vector3 reactVec = other.transform.position - transform.position;

            Debug.Log(CurHealth);
            Destroy(other.gameObject);
            StartCoroutine(Ondamage(reactVec));
        }
        if(other.tag == "Player")
        {
            player2 player = other.GetComponent<player2>();
            
            player.health -= attack;

            Vector3 pushDirection = new Vector3(transform.position.x - other.transform.position.x, 0, transform.position.z - other.transform.position.z);

            pushDirection.Normalize();
            
            rigid.AddForce(pushDirection * 10, ForceMode.Impulse);
            rigid.AddTorque(Vector3.right, ForceMode.Impulse);
            Debug.Log("적이 밀려나고 회전합니ㅏ;");
        }
    }

    

    IEnumerator Ondamage(Vector3 reactVec)
    {
        ChangeToNewSprite();

        //reactVec = reactVec.normalized

      

        yield return new WaitForSeconds(0.5f);

        if ( CurHealth >0 )
        {
            sp.sprite = issprite;

        }
        else if ( CurHealth == 0 )
        {
            collider.enabled = false;
            sp.sprite = deadsprite;
            rigid.AddForce(reactVec * 1, ForceMode.Impulse);
            nav.isStopped = true;

            Destroy(gameObject, 3f);
            
            kill++;
        }
        else
        {
            collider.enabled = false;
            sp.sprite = deadsprite;
            rigid.AddForce(reactVec * 1, ForceMode.Impulse);
            nav.isStopped = true;

            Destroy(gameObject, 3f);
        }
    }

    void ChangeToNewSprite()
    {
        if (sp != null && attacksprite != null)
        {
            sp.sprite = attacksprite; 
        }

        
    }
}