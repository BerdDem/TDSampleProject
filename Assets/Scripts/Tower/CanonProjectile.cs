using UnityEngine;

namespace Tower
{
    public class CanonProjectile : MonoBehaviour
    {
        [SerializeField] private Rigidbody m_rigidbody;
        
        public void SetVelocity(Vector3 velocity)
        {
            m_rigidbody.velocity = velocity;
        }
    }
}