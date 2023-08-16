using UnityEngine;
using Random = UnityEngine.Random;

namespace Enemy
{
    public class EnemySpawner : MonoBehaviour
    {
        public int remainingEnemiesToSpawn { get; private set; }
        
        [SerializeField] private FinishTargetPoint m_finishTargetPoint;
        [SerializeField] private EnemyContainer m_enemyContainer;
 
        [Header("Character")] 
        [SerializeField] private GameObject m_mutantEnemy;

        [Header("Spawn Params")]
        [SerializeField] private int m_spawnEnemyCount = 20;
        [SerializeField] private Vector2 m_randomSpawnDuration = new(1, 2);

        private float m_timeToSpawn;

        private void Awake()
        {
            remainingEnemiesToSpawn = m_spawnEnemyCount;
        }

        private void Update()
        {
            if (remainingEnemiesToSpawn == 0)
            {
                return;
            }
            
            if (m_timeToSpawn < 0)
            {
                Spawn();
                remainingEnemiesToSpawn--;

                m_timeToSpawn = Random.Range(m_randomSpawnDuration.x, m_randomSpawnDuration.y);
                return;
            }

            m_timeToSpawn -= Time.deltaTime;
        }
        
        private void Spawn()
        {
            Quaternion spawnRotation = Quaternion.LookRotation(m_finishTargetPoint.transform.position - transform.position, Vector3.up);
            GameObject EnemyObject = Instantiate(m_mutantEnemy, transform.position, spawnRotation, m_enemyContainer.transform);

            EnemyCharacter enemyCharacter = EnemyObject.GetComponent<EnemyCharacter>();
            enemyCharacter.SetMovePoint(m_finishTargetPoint.gameObject);
            
            m_enemyContainer.AddNewEnemy(enemyCharacter);
        }
    }
}
