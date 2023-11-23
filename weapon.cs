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
}