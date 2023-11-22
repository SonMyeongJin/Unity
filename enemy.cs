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

    Rigidbody rigid;

    private void Awake()
    {
        rigid = GetComponent<Rigidbody>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "bullet")
        {
            bullet bullet = other.GetComponent<bullet>();
            CurHealth -= bullet.Damage;

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

            // 방향을 정규화합니다.
            pushDirection.Normalize();
            
            rigid.AddForce(pushDirection * 10, ForceMode.Impulse);
            rigid.AddTorque(Vector3.right, ForceMode.Impulse);
            Debug.Log("적이 밀려나고 회전합ㄴㅣㄷㅏ;");
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
        else
        {
            sp.sprite = deadsprite;
            rigid.AddForce(reactVec * 1, ForceMode.Impulse);
            nav.isStopped = true;

            Destroy(gameObject, 10f);
            kill++;
        }
    }
    }
}