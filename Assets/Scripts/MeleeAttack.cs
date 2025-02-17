using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    [SerializeField] Animator animator;
    //[SerializeField] GameObject attack;
    [SerializeField] PlayerController playerController;
    [SerializeField] Transform attackDir;
    [SerializeField] float attackRange = 0.5f;
    [SerializeField] SpriteRenderer swordRenderer;
    bool canAttack = true;
    

    private readonly int animHashAttacking = Animator.StringToHash("AttackingRight");
    private readonly int animHashAttackDir = Animator.StringToHash("SwingDir");
    //private readonly int animHashSwingDelay = Animator.StringToHash("SwingDelay");

    private RaycastHit2D[] hits;

    private void Update()
    {
        //animator.SetBool(animHashAttacking, playerController.IsAttacking);
        //gameObject.SetActive(playerController.IsAttacking);
        //if (playerController.IsAttacking)
        //{
        //    Attack(playerController.NearestEnemy());
        //}
    }

    public void Attack(Vector2 dir)
    {
        Debug.Log("Attack! " + canAttack);
        if (canAttack)
        {
            
            PlayAnimation();
            if (dir != Vector2.zero)
            {
                Vector3 target = dir;

                float angle = Mathf.Atan2(target.y - transform.position.y, target.x - transform.position.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(new Vector3(0, 0, angle + 270));
                attackDir.rotation = targetRotation;
            }

            Debug.DrawRay(attackDir.position + attackDir.up * 2, attackDir.up * 0.5f, Color.white, 0.1f);
            hits = Physics2D.CircleCastAll(attackDir.position + attackDir.up * 0.5f, attackRange, attackDir.up, 0f);

            for (int i = 0; i < hits.Length; i++)
            {
                if (hits[i].collider.gameObject.layer == 6)
                {
                    // TODO: separate colliders
                    Debug.Log("hit: " +hits[i].collider.name);
                    EnemyController enemy = hits[i].collider.gameObject.GetComponent<EnemyController>();
                    enemy.Damage();
                    playerController.combo.IncreaseCombo();
                }
            }
        }
    }

    private void PlayAnimation()
    {
        swordRenderer.gameObject.SetActive(true);
        //AnimatorStateInfo animStateInfo = animator.GetCurrentAnimatorStateInfo(0);

        Debug.Log(animator.GetBool(animHashAttacking));

        if (!animator.GetBool(animHashAttacking))
        {
            swordRenderer.flipX = false;
            animator.SetBool(animHashAttacking, true);
        }
        else 
        {
            swordRenderer.flipX = true;
            animator.SetBool(animHashAttacking, false);
        }
    }

    private void Event_CanAttackAgain()
    {
        swordRenderer.gameObject.SetActive(false);
        canAttack = true;
    }

    private void Event_AttackStart()
    {
        canAttack = false;
    }
}
