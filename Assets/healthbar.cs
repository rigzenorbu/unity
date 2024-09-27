using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthbar : MonoBehaviour
{
    // Start is called before the first frame update
    public int maxHealth = 100;
    public int curretHealth;
    void Start()
    {
        curretHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
