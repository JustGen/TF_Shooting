using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace TF_Shooting.CodeBaseTF
{
    public class GameController : MonoBehaviour
    {
        [SerializeField] private GameObject _spawnPoint;
        [SerializeField] private Plate _platePrefab;
        [SerializeField] private Cross _cross;

        private List<Plate> _plates = new();
        private bool _isActive = false;
        private Plate _currentTarget;
        private int _sizePool = 5;

        private void Start()
        {
            _isActive = true;
            BuildPool();
            
            SetTarget(_plates[0]);
            StartCoroutine(PlateGeneratorCo());
        }

        private void OnDestroy() => 
            _isActive = false;

        private void Update()
        {
            if (_currentTarget.IsMove && !(_currentTarget.gameObject.transform.position.x < -1.2f)) 
                return;
            
            SetTarget(_plates[1]);
                
            Plate oldPlate = _plates[0];
            _plates.RemoveAt(0);
            _plates.Add(oldPlate);
        }

        private void BuildPool()
        {
            for (int i = 0; i < _sizePool; i++) 
                CreatePlate();
        }

        private void SetTarget(Plate plate)
        {
            _currentTarget = plate;
            _cross.Target = _currentTarget;
        }

        private IEnumerator PlateGeneratorCo()
        {
            while (_isActive)
            {
                yield return new WaitForSeconds(1f);
                GiveImpulseNextPlate();
            }
        }

        private void GiveImpulseNextPlate()
        {
            foreach (Plate item in _plates.Where(item => !item.IsMove))
            {
                item.GiveImpulse();
                break;
            }
        }

        private void CreatePlate()
        {
            GameObject plate = Instantiate(_platePrefab.gameObject, _spawnPoint.transform.position, Quaternion.identity);
            _plates.Add(plate.GetComponent<Plate>());
        }
    }
}