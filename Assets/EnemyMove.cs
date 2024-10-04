using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    Rigidbody2D rigid;
    public int nextMove;
    Animator anim;
    SpriteRenderer spriteRenderer;
    CapsuleCollider2D capsulecolloder;


    // Start is called before the first frame update
    void Awake()
    {
        rigid = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        capsulecolloder = GetComponent<CapsuleCollider2D>();


        Invoke("Think", 5);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        rigid.velocity = new Vector2(nextMove, rigid.velocity.y);

        //platform chenk
        Vector2 frontVec = new Vector2(rigid.position.x + nextMove * 0.2f, rigid.position.y);
        Debug.DrawRay(frontVec, Vector3.down, new Color(0, 1, 0));
        RaycastHit2D rayHit = Physics2D.Raycast(frontVec, Vector3.down, 1, LayerMask.GetMask("Ground"));
        if (rayHit.collider == null)
        {
            nextMove *= -1;
            CancelInvoke();
            Invoke("Think", 2);
        }

    }

    void Think()
    {
        //set next active
        nextMove = Random.Range(-1, 2);
        //
        float nextThinkTime = Random.Range(2f, 5f);
        Invoke("Think", nextThinkTime);
        //
        anim.SetInteger("WalkSpeed", nextMove);
        //Filp
        if (nextMove != 0)
            spriteRenderer.flipX = nextMove == 1;
    }
    void Turn()
    {
        nextMove *= -1;
        spriteRenderer.flipY = nextMove == 1;

        CancelInvoke();
        Invoke("Think", 2);
    }
    public void OnDamaged()
    {
        //Sprite Alpha
        spriteRenderer.color = new Color(1, 1, 1, 0.4f);
        //Sprite Flip Y
        spriteRenderer.flipY = true;
        //Collider Disable
        capsulecolloder.enabled = false;
        //Die Effect Jump
        rigid.AddForce(Vector2.up * 5, ForceMode2D.Impulse);
        //Destroy
        Invoke("DeActive", 5);
    }
}
