using UnityEngine;

namespace Core
{
    public class TestTrigger : MonoBehaviour
    {
        private int _count; 
        
        private void OnEnable()
        {
            Debug.Log("e ");
            _count = 0;
            GetComponent<Rigidbody2D>().simulated = false;
        }

        private void FixedUpdate()
        {
            Debug.Log("F " + _count++);
            if (_count > 1)
                GetComponent<Rigidbody2D>().simulated = true;
        }

        private void OnTriggerEnter2D(Collider2D col)
        {
            Debug.Log("ENTER " + _count++);
            enabled = false;
        }
        
        private void OnTriggerExit2D(Collider2D col)
        {
            enabled = true;
        }
    }
}