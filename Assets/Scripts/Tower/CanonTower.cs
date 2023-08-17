using System.Collections.Generic;
using Enemy;
using UnityEngine;

namespace Tower
{
    public class CanonTower : MonoBehaviour
    {
        [SerializeField] private EnemyContainer m_enemyContainer;
        
        [Header("Projectile")]
        [SerializeField] private GameObject m_canonProjectile;
        [SerializeField] private GameObject m_shotPoint;
        
        [Header("Shoot params")]
        [SerializeField] private float m_shotsPerSecond;
        [SerializeField] private float m_projectileSpeed;
        
        private readonly List<CanonProjectile> m_canonProjectiles = new();
        private float m_timeToShot;

        private void Update()
        {
            if (m_timeToShot <= 0)
            {
                Shoot();
                m_timeToShot = m_shotsPerSecond;
                
                return;
            }

            m_timeToShot -= Time.deltaTime;
        }

        private void Shoot()
        {
            if (m_enemyContainer.enemyCharacterContainer.Count == 0)
            {
                return;
            }
            
            int enemyIndex = 0;
            
            if (!CanShoot(ref enemyIndex))
            {
                return;
            }
            
            EnemyCharacter targetEnemy = m_enemyContainer.enemyCharacterContainer[enemyIndex];

            if (!CalculateVelocity(targetEnemy, out Vector3 velocity))
            {
                return;
            }
            
            GameObject projectileObject = Instantiate(m_canonProjectile, m_shotPoint.transform.position, m_shotPoint.transform.rotation);
            CanonProjectile canonProjectile = projectileObject.GetComponent<CanonProjectile>();
            m_canonProjectiles.Add(canonProjectile);
            
            canonProjectile.SetVelocity(velocity);
        }

        private bool CalculateVelocity(EnemyCharacter enemyCharacter, out Vector3 velocity)
        {
            float gravity = Physics.gravity.magnitude;
            Vector3 targetVelocity = enemyCharacter.velocity;
            Vector3 projectilePosition = m_shotPoint.transform.position;
            Vector3 targetPosition = enemyCharacter.transform.position;

            Vector3[] solutions = new Vector3[2];
            int numSolution;

            if (targetVelocity.sqrMagnitude > 0)
            {
                numSolution = fts.solve_ballistic_arc(projectilePosition, m_projectileSpeed, targetPosition, targetVelocity, gravity, out solutions[0], out solutions[1]);
            }
            else
            {
                numSolution = fts.solve_ballistic_arc(projectilePosition, m_projectileSpeed, targetPosition, gravity, out solutions[0], out solutions[1]);
            }

            velocity = solutions[0];
            
            return numSolution != 0;
        }

        private bool CanShoot(ref int enemyIndex)
        {
            float maxRange = fts.ballistic_range(m_projectileSpeed, Physics.gravity.magnitude, m_shotPoint.transform.position.y);
            
            for (int i = 0; i < m_enemyContainer.enemyCharacterContainer.Count; i++)
            {
                EnemyCharacter character = m_enemyContainer.enemyCharacterContainer[i];
                if (Vector3.Distance(character.gameObject.transform.position, transform.position) > maxRange)
                {
                    continue;
                }

                enemyIndex = i;
                return true;
            }
            
            return false;
        }
    }
}