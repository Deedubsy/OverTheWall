using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class EnemyController : MonoBehaviour
{

    public enum Enemy_Type
    {
        Sword = 0,
        SwordAndShield = 1,
        Archer = 2
    }

    public RectTransform castleRect;

    private Enemy_Type EnemyType;
    private float EnemyHealth;
    private float MovementSpeed;
    private float AttackSpeed;
    private float AttackRange;
    private float AttackDamage;
    private float AttackCooldown;
    private string Name;
    private Animator animator;

    private RectTransform enemyRect;
    private bool canAttack = true;
    private CastleController castle;

    public void Initialize(Enemy_Type EnemyType, float EnemyHealth, float MovementSpeed, float AttackSpeed, float AttackRange, float AttackDamage, float AttackCooldown)
    {
        this.EnemyType = EnemyType;
        this.EnemyHealth = EnemyHealth;
        this.MovementSpeed = MovementSpeed;
        this.AttackSpeed = AttackSpeed;
        this.AttackRange = AttackRange;
        this.AttackDamage = AttackDamage;
        this.AttackCooldown = AttackCooldown;

        animator = new Animator();

        castle = FindObjectOfType<CastleController>();
        enemyRect = GetComponent<RectTransform>();
    }

    // Use this for initialization
    void Start()
    {
        castle = FindObjectOfType<CastleController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void UpdateRoutine()
    {
        if (WithinAttackRange())
        {
            Attack();
        }
        else
        {
            Move();
        }
    }

    private void Move()
    {
        //Movement
        transform.position = new Vector3(transform.position.x + (MovementSpeed * -1) * Time.deltaTime, transform.position.y);
    }

    private bool WithinAttackRange()
    {
        bool withinAttackRange = false;

        //Vector3 leftBottomEdge = new Vector3(transform.position.x - (enemyRect.sizeDelta.x / 2),
        //                                     transform.position.y - (enemyRect.sizeDelta.y / 2));

        //Vector3 castleRightBottomEdge = new Vector3(castle.transform.position.x + (castleRect.sizeDelta.x / 2),
        //                                     castle.transform.position.y - (castleRect.sizeDelta.y / 2));

        ////float distance = Vector3.Distance(transform.position, castle.transform.position);
        //float distance = Vector2.Distance(leftBottomEdge, castleRightBottomEdge);
        float rightSideCastle = castle.transform.position.x + (castleRect.sizeDelta.x / 2);
        float leftSideEnemy = transform.position.x - (enemyRect.sizeDelta.x / 2);
        float distance = rightSideCastle - leftSideEnemy;


        if (Mathf.Abs(distance) <= AttackRange)
            withinAttackRange = true;
        
        return withinAttackRange;
    }

    public virtual void Attack()
    {
        if (canAttack)
        {
            canAttack = false;

            //Play Attack animation



            castle.OnHit(AttackDamage);

            StartCoroutine(AttackRoutine());

            Debug.Log("ATTACK");
        }
    }

    private IEnumerator AttackRoutine()
    {
        yield return new WaitForSeconds(AttackCooldown);

        canAttack = true;
    }

    public virtual void Die()
    {

    }
}
