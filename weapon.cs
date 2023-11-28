using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class weapon : MonoBehaviour
{
    public float rate;

    public Transform bulletExit;
    public GameObject bullet;

    public Transform bulletCaseExit;
    public GameObject bulletCase;

    public int maxAmmo;
    public int curAmmo;

    public void use()
    {
        if (curAmmo > 0)
        {
            StartCoroutine("shot");
            curAmmo--;
            
        }
      
        
    }

    //총알이랑 총탄 벽에 닿았을때 사라지는 로
   IEnumerator shot()
    {
        GameObject instantBullet = Instantiate(bullet, bulletExit.position, bulletExit.rotation);
        Rigidbody bulletRigid = instantBullet.GetComponent<Rigidbody>();
        bulletRigid.velocity = bulletExit.forward * 50;
        Destroy(instantBullet, 5f);

        yield return null;
        GameObject instantBulletCase = Instantiate(bulletCase, bulletCaseExit.position, bulletCaseExit.rotation);
        Rigidbody bulletCaseRigid = instantBullet.GetComponent<Rigidbody>();
        bulletRigid.AddForce(Vector3.up,ForceMode.Impulse);

    }
}