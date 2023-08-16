using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyContainer : MonoBehaviour
    {
        public List<EnemyCharacter> enemyCharacterContainer { get; private set; }

        private void Awake()
        {
            enemyCharacterContainer = new List<EnemyCharacter>();
        }

        public void AddNewEnemy(EnemyCharacter enemyCharacter)
        {
            enemyCharacterContainer.Add(enemyCharacter);
            enemyCharacter.onFinishPointAction += RemoveEnemy;
            enemyCharacter.deathAction += RemoveEnemy;
        }

        private void RemoveEnemy(EnemyCharacter enemyCharacter)
        {
            enemyCharacterContainer.Remove(enemyCharacter);
        }
    }
}