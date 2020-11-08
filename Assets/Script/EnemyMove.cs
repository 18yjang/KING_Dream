using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    //public GameObject enemy;

    Rigidbody2D rigid;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D capsuleCollider;

    public int nextMove;

    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsuleCollider = GetComponent<CapsuleCollider2D>();
        Invoke("Think", 0);
    }


    void FixedUpdate()
    {
        //Move
        int ran = Random.Range(0, 2);
        if (ran == 0)
        {
            rigid.velocity = new Vector2(nextMove, rigid.velocity.y);
        }
        else
            rigid.velocity = new Vector2(rigid.velocity.x, nextMove);


        //Platform Check
        //Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.3f, rigid.position.y);
        //Vector2 upVec = new Vector2(rigid.position.x, rigid.position.y + nextMove * 0.3f);
        Debug.DrawRay(Vector2.left, Vector2.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(Vector2.right, Vector2.down, 1, LayerMask.GetMask("Wall"));

        if (rayHit.collider)
        {
            Turn();
        }
    }

    void Think()
    {
        // Set Next Action
        nextMove = Random.Range(-1, 2);   // 최대값 포함 안됨(-1<= n < 2)

        int ran = Random.Range(0, 2);
        // Flip Sprite
        if (nextMove != 0)
        if (ran == 0)
        {
            spriteRenderer.flipX = nextMove == 1;
        }
        else
            spriteRenderer.flipY = nextMove == 1;

        // Recursive
        float nextThinkTime = Random.Range(0f, 2f);
        Invoke("Think", nextThinkTime); // 재귀
    }

    void Turn()
    {
        nextMove *= -1;
        int ran = Random.Range(0, 2);
        if (ran == 0)
        {
            spriteRenderer.flipX = nextMove == 1;
        }
        else
            spriteRenderer.flipY = nextMove == 1;


        CancelInvoke();
        Invoke("Think", 1);
    }

}
