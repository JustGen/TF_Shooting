using UnityEngine;
using Random = UnityEngine.Random;

namespace TF_Shooting.CodeBaseTF
{
    public class Plate : MonoBehaviour
    {
        [SerializeField] private Rigidbody _rigidbody;
        [SerializeField] private float launchForce;

        public bool IsMove { get; set; } = false;
        private Vector3 _startPosition;
        private float _launchAngle;

        private void Start() => 
            _startPosition = transform.position;

        public void GiveImpulse()
        {
            IsMove = true;
            _rigidbody.isKinematic = false;
            _launchAngle = Random.Range(30, 60);
        
            float radians = _launchAngle * Mathf.Deg2Rad;
            Vector3 launchDirection = new Vector3(-1* Mathf.Cos(radians), Mathf.Sin(radians), 0);
            _rigidbody.AddForce(launchDirection * launchForce, ForceMode.Impulse);
        }

        private void OnCollisionEnter(Collision other)
        {
            if (!other.collider.CompareTag("Ground"))
                return;

            Reset();
        }

        public void Reset()
        {
            IsMove = false;
            _rigidbody.isKinematic = true;
            _rigidbody.velocity = Vector3.zero;
            transform.position = _startPosition;
        }
    }
}