using Enemy;
using UnityEngine;

namespace GameState
{
    public class GameCondition : MonoBehaviour
    {
        [SerializeField] private FinishTargetPoint m_finishTargetPoint;
        [SerializeField] private EnemyContainer m_enemyContainer;
        [SerializeField] private EnemySpawner m_enemySpawner;

        [SerializeField] private int m_enemyOnFinishToLose = 5;

        private int m_currentCountEnemyOnFinish;

        private void Awake()
        {
            m_finishTargetPoint.enemyOnFinishPoint += EnemyOnFinishPoint;
        }

        private void Update()
        {
            if (m_enemyContainer.enemyCharacterContainer.Count == 0 && m_enemySpawner.remainingEnemiesToSpawn == 0)
            {
                Win();
            }
        }

        private void EnemyOnFinishPoint()
        {
            m_currentCountEnemyOnFinish++;

            if (m_currentCountEnemyOnFinish == m_enemyOnFinishToLose)
            {
                Lose();
            }
        }

        private void Lose()
        {
            Debug.LogError("You lose");
        }

        private void Win()
        {
            Debug.LogError("You win");
        }

        private void OnDestroy()
        {
            if (m_finishTargetPoint)
            {
                m_finishTargetPoint.enemyOnFinishPoint -= EnemyOnFinishPoint;
            }
        }
    }
}