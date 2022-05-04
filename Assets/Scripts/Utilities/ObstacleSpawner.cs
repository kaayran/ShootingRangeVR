using UnityEngine;

namespace Utilities
{
    public class ObstacleSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject _obstacle;
        [SerializeField] private int _count;

        private void Start()
        {
            for (var i = 0; i < _count; i++)
            {
                var obstacle = Instantiate(_obstacle, transform.position + Vector3.one * Random.Range(0f, 1f),
                    transform.rotation);
                obstacle.GetComponent<MeshRenderer>().material.color = Random.ColorHSV();
            }
        }
    }
}