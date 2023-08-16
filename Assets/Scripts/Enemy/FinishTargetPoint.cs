using System;
using UnityEngine;

namespace Enemy
{
    public class FinishTargetPoint : MonoBehaviour
    {
        public Action enemyOnFinishPoint;

        [SerializeField] private EnemyContainer m_enemyContainer;
        [SerializeField] private float m_successErrorDistance = 2;
        
        private void Update()
        {
            for (int i = 0; i < m_enemyContainer.enemyCharacterContainer.Count; i++)
            {
                EnemyCharacter enemyCharacter = m_enemyContainer.enemyCharacterContainer[i];
                if (Vector3.Distance(enemyCharacter.transform.position, transform.position) < m_successErrorDistance)
                {
                    enemyCharacter.OnFinish();
                    enemyOnFinishPoint?.Invoke();
                }
            }
        }
    }
}