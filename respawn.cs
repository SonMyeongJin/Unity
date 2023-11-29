using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEditor.FilePathAttribute;
using UnityEngine.UIElements;
using UnityEngine.UI;
using Unity.VisualScripting;

public class respawn : MonoBehaviour
{
    public GameObject n1;
    public GameObject n2;
    public GameObject n3;
    public GameObject n4;
    public GameObject player;

    public Text text1;
    public Text text2;
    public Text text3;

    public float minX = -70f; // X 좌표 최솟값
    public float maxX = 120f;  // X 좌표 최댓값
    public float minY = 5f;   // Y 좌표 최솟값
    public float maxY = 10f;   // Y 좌표 최댓값
    public float minZ = -17f; // Z 좌표 최솟값
    public float maxZ = 100f;  // Z 좌표 최댓값

    

    void Start()
    {
        InvokeRepeating("Make1", 1f,1f);
        InvokeRepeating("Make2", 10f, 2f);       
    }
    bool boss = true;
    bool subbos = true;
    private void Update()
    {
        n1.GetComponent<enemy>().target = player.GetComponent<Transform>();
        n2.GetComponent<enemy>().target = player.GetComponent<Transform>();
        n3.GetComponent<enemy>().target = player.GetComponent<Transform>();
        n4.GetComponent<enemy>().target = player.GetComponent<Transform>();
        Resources.UnloadUnusedAssets();

        if (enemy.kill >= 3  && boss)
        {
            
            text1.gameObject.SetActive(true);
            destrrr();
            Invoke("Make3",6f);
            Invoke("HideWarning1", 5f);
            boss = false;
        }

        if (enemy.kill >=4 && !boss && subbos)
        {
            text2.gameObject.SetActive(true);
            Invoke("HideWarning2", 5f);
            Invoke("upWarning", 5f);
            player.GetComponent<player2>().moveSpeed = 3;
            Invoke("HideWarning3", 10f);
            Invoke("Make4", 12f);
            subbos = false;
        }
        Debug.Log("죽인 적의 수" + enemy.kill);
    }
    void destrrr()
    {
        CancelInvoke("Make1");
        CancelInvoke("Make2");

        GameObject[] objectsToDelete = GameObject.FindGameObjectsWithTag("enemy");
        foreach (GameObject obj in objectsToDelete)
        {
            Destroy(obj);
        }
    }

    void HideWarning1()
    {
        text1.gameObject.SetActive(false);
    }
    void HideWarning2()
    {
        text2.gameObject.SetActive(false);
    }
    void HideWarning3()
    {
        text3.gameObject.SetActive(false);
    }
    void upWarning()
    {
        text3.gameObject.SetActive(true);
    }

    void Make1()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        float randomZ = Random.Range(minZ, maxZ);

        Vector3 randomPosition = new Vector3(randomX, randomY, randomZ);

        Instantiate(n1, randomPosition, Quaternion.identity);
        Debug.Log("make1 생성 ");
    }

    void Make2()
    {
        float randomX = Random.Range(minX, maxX);
        float randomY = Random.Range(minY, maxY);
        float randomZ = Random.Range(minZ, maxZ);

        Vector3 randomPosition = new Vector3(randomX, randomY, randomZ);

        Instantiate(n2, randomPosition, Quaternion.identity);
        Debug.Log("make2 생성 ");
    }

    void Make3()
    {
    
        Instantiate(n3, Vector3.zero, Quaternion.identity);
        Debug.Log("make3 생성 ");
    }

    void Make4()
    {
       
        Debug.Log("make123 루프 중지");

       

        Instantiate(n4, Vector3.zero, Quaternion.identity);
        Debug.Log("make4 보  생성 ");
    }

 
 
}
