using UnityEngine;

public class MeleeAttackAnimationController : MonoBehaviour
{
    [SerializeField] Animator animator;
    [SerializeField] GameObject attack;
    [SerializeField] PlayerController playerController;

    private readonly int animHashAttacking = Animator.StringToHash("Attacking");
    private readonly int animHashAttackDir = Animator.StringToHash("SwingDir");
    private readonly int animHashSwingDelay = Animator.StringToHash("SwingDelay");

    private void Update()
    {
        animator.SetBool(animHashAttacking, playerController.IsAttacking);
        gameObject.SetActive(playerController.IsAttacking);

        //if (Physics2D.CircleCast(transform.position, 2f, transform.forward, out hit, 2f))
        //{
        //    Debug.Log("Hit");
        //}
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("stay");
    }
}
