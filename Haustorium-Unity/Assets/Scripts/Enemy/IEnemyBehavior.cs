using System.Collections;
using UnityEngine;

namespace Assets.Scripts.Enemy
{
    public interface IEnemyBehavior
    {
        void Idle();
        void Attack(GameObject target);
        void Stun();
    }
}