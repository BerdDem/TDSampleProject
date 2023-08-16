using System;
using UnityEngine;
using UnityEngine.AI;

namespace Enemy
{
    public class EnemyCharacter : MonoBehaviour
    {
        public Action<EnemyCharacter> deathAction;
        public Action<EnemyCharacter> onFinishPointAction;

        [SerializeField] private NavMeshAgent m_navMeshAgent;

        private GameObject m_movePoint;

        public void SetMovePoint(GameObject movePoint)
        {
            m_movePoint = movePoint;
            m_navMeshAgent.destination = m_movePoint.transform.position;
        }

        public void OnFinish()
        {
            onFinishPointAction?.Invoke(this);
            Destroy(gameObject);
        }

        public void OnDamage()
        {
        }

        private void Death()
        {
            deathAction?.Invoke(this);
        }
    }
}