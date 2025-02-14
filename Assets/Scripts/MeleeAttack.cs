using UnityEngine;

public class MeleeAttack : MonoBehaviour
{
    //[SerializeField] Animator animator;
    //[SerializeField] GameObject attack;
    [SerializeField] PlayerController playerController;

    //private readonly int animHashAttacking = Animator.StringToHash("Attacking");
    //private readonly int animHashAttackDir = Animator.StringToHash("SwingDir");
    //private readonly int animHashSwingDelay = Animator.StringToHash("SwingDelay");

    private RaycastHit2D[] hits;

    private void Update()
    {
        //animator.SetBool(animHashAttacking, playerController.IsAttacking);
        //gameObject.SetActive(playerController.IsAttacking);
        if (playerController.IsAttacking)
        {
            Attack(Vector2.up);
        }
        
    }

    private void Attack(Vector2 dir)
    {
        //transform.rotation = Quaternion.LookRotation(dir);//transform.LookAt(dir);
        Debug.DrawRay(transform.position + transform.up * 2, transform.up * 0.5f, Color.white, 0.1f);
        hits = Physics2D.CircleCastAll(transform.position + transform.up * 2, 0.5f, transform.up, 0f);

        for (int i = 0; i < hits.Length; i++)
        {
            if (hits[i].collider.gameObject.layer == 6)
            {
                EnemyController enemy = hits[i].collider.gameObject.GetComponent<EnemyController>();
                enemy.Damage();
            }
            
        }
    }
}
