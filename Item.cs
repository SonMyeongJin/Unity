using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public enum Type { aGun, bGun, bag, guncase, ball1,ball2, health }
    public Type Itemtype;
    public int number;

    void Update()
    {
        transform.Rotate(Vector3.up * 30 * Time.deltaTime);
    }
}
