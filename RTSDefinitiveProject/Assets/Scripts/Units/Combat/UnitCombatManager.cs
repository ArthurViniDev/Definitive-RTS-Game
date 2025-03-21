using UnityEngine;

public class UnitCombatManager : MonoBehaviour
{
    public int life;

    protected Animator animator;
    [SerializeField] protected int damage;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    public virtual void TakeDamage(int damage)
    {
        if (life <= 0)
        {
            Die();
        }
        animator.SetTrigger("takeDamage");
        life -= damage;
    }

    public virtual void Die()
    {
        Destroy(gameObject);
    }
}
