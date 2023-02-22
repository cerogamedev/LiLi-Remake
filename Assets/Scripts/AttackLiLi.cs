using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackLiLi : MonoBehaviour
{
    private Animator anim;
    public bool isAttacking;
    [SerializeField] GameObject attackHitBox;
    void Start()
    {
        anim = GetComponent<Animator>();
        attackHitBox.SetActive(false);
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0) && isAttacking == false)
        {
            isAttacking = true;
            int index = UnityEngine.Random.Range(1, 4);
            anim.Play("lili-attack" + index);
            StartCoroutine(DoAttack());
            
        }

    }
    IEnumerator DoAttack()
    {
        attackHitBox.SetActive(true);
        yield return new WaitForSeconds(.35f);
        isAttacking = false;
        attackHitBox.SetActive(false);
    }
}
