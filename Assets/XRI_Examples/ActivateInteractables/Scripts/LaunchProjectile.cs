namespace UnityEngine.XR.Content.Interaction
{
    /// <summary>
    /// Apply forward force to instantiated prefab
    /// </summary>
    public class LaunchProjectile : MonoBehaviour
    {
        [SerializeField]
        [Tooltip("The projectile that's created")]
        GameObject m_ProjectilePrefab = null;

        [SerializeField]
        [Tooltip("The point that the project is created")]
        Transform m_StartPoint = null;

        [SerializeField]
        private float m_LaunchSpeed = 1.0f;

        public GameObject buildammoObj;

       
        public void Fire()
        {
            if (buildammoObj != null)
            {
                GameObject newObject = Instantiate(m_ProjectilePrefab, m_StartPoint.position, m_StartPoint.rotation, null);

                if (newObject.TryGetComponent(out Rigidbody rigidBody))
                    ApplyForce(rigidBody);
            }
        }

        void ApplyForce(Rigidbody rigidBody)
        {
            Vector3 force = m_StartPoint.forward * m_LaunchSpeed;
            rigidBody.AddForce(force);
        }

        public void BuildAmmo(GameObject buildammoObj_)
        {
            buildammoObj = buildammoObj_;
            Debug.Log("LaunchProjectile BuildAmmo>>");    
        }

        public void UnBuildAmmo()
        {
            buildammoObj = null;
            Debug.Log("LaunchProjectile UnBuildAmmo>>");
        }
    }
}
