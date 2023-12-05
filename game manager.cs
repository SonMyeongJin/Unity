using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject menuCam;
    public GameObject gameCam;
    public GameObject weapon1;
    public GameObject weapon2;
    public weapon gun1;
    public weapon gun2;

    public player2 player;


    public int stage;


    public GameObject menuPanel;
    public GameObject gamePanel;
    public GameObject gameOver;
    public GameObject gameClear;

    public Text enemyTxt;
    public Text stageTxt;
    public Text healthTxt;
    public Text gun1Txt;
    public Text gun2Txt;
    public GameObject gun1img;
    public GameObject gun2img;

    public GameObject boss;
    enemy bossEnemy;

    public GameObject bossHealthGroup;
    public RectTransform bossHealthBar;

    public void Awake()
    {
       
        Debug.Log("시브랄 여기 들어온건가" );
        gun1 = weapon1.GetComponent<weapon>();
        gun2 = weapon2.GetComponent<weapon>();
        


    }

    public void GameStart()
    {
        menuCam.SetActive(false);
        gameCam.SetActive(true);

        menuPanel.SetActive(false);
        gamePanel.SetActive(true);

    }

    public void Update()
    {
        //왼쪽 위
        enemyTxt.text = enemy.kill + " / " + (respawn.stage + 9) ;
        stageTxt.text = "Stage : " + respawn.stage.ToString();

        //왼쪽 아래
        healthTxt.text = player.health + " / " + player.maxHealth;

        if (player.hasweapon[0] == false)
            gun1Txt.text = "- /" + player.ammo1;
        else
        {
            gun1Txt.text = gun1.curAmmo + " / " + player.ammo1;
            gun1img.SetActive(true);
        }

        if (player.hasweapon[1] == false)
            gun2Txt.text = "- /" + player.ammo2;
        else
        {
            gun2Txt.text = gun2.curAmmo + " / " + player.ammo2;
            gun2img.SetActive(true);
        }


        Debug.Log("respawn stage : " + respawn.stage);
        //보스 체력바
       
        if (respawn.stage == 3)
        {
            Debug.Log("보스 체력바 활성");
            bossHealthGroup.SetActive(true);


            bossEnemy = boss.GetComponent<enemy>();
            Debug.Log(enemy.bossHealth / bossEnemy.MaxHealth);
           
            bossHealthBar.localScale = new Vector3(enemy.bossHealth / 200, 1, 1);
        }

        //gameover panel
        if (player.dead == true && player.health < 1)
        {
            Invoke("gameOv", 4f);
        }

        //gameclear panel
        if (enemy.kill >= 12)
        {
            Invoke("gameCR", 4f);
        }


    }
    void gameOv()
    {
        gameOver.SetActive(true);
        gamePanel.SetActive(false);
    }

    void gameCR()
    {
        gamePanel.SetActive(false);
        gameClear.SetActive(true);
    }
}
