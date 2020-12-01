using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Breakable : MonoBehaviour
{

    private Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        
    }

    public void Smash()
    {
        anim.SetBool("Smash", true);
        StartCoroutine(BreakCo());
    }

    IEnumerator BreakCo()
    {
        yield return new WaitForSeconds(.5f);
        this.gameObject.SetActive(false);
    }
}
