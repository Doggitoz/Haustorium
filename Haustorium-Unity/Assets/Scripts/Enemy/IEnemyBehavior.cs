using System.Collections;
using UnityEngine;



/// <summary>
/// Call Idle, Attack, or Stun once to begin doing that action.
/// EnemyBehavior should continue that action until changed again.
/// Call Die to play death animation and destroy enemy
/// </summary>
public interface IEnemyBehavior
{
    [SerializeField] float stunDuration { get; set; }
    void Idle();
    void Attack(GameObject target);
    void Stun();
    void Die();
}
