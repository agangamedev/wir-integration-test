using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Project.Components
{
    public class MoveableComponent : MonoBehaviour
    {
        private Vector3 _destination;

        private void Start()
        {
            //SetDestination(new Vector3(3.5f, transform.position.y, transform.position.z));
        }

        public void SetDestination(Vector3 destination)
        {
            //add implementation to move this object to destination
            //and destroy it when it reach the destination
            //desination must be some vector3 where y and z coordinate not change from current coordinate
            //only x coordinate change and to positive direction (to the right)

            _destination = new Vector3(destination.x, transform.position.y, transform.position.z);

            StartCoroutine(MoveToDestination(1f));
        }

        private IEnumerator MoveToDestination(float _speed)
        {
            while (transform.position.x < _destination.x)
            {
                Vector3 position = Vector3.MoveTowards(transform.position, _destination, _speed * Time.deltaTime);
                transform.position = position;
                yield return null;
            }

            gameObject.SetActive(false);
        }

    }
}