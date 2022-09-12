using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Subconscionus.PuzzleMaze
{
    public class MazePlayerController : MonoBehaviour
    {
        private Rigidbody2D _mainRigidbody;
        [SerializeField] private Transform _startPosition, _finishPosition;
        public Transform StartPosition { get { return _startPosition; } }

        [SerializeField] private float _moveSpeed;

        public bool IsMoving { get; set; } = true;

        private void Start()
        {
            IsMoving = true;
            transform.position = _startPosition.position;
            _mainRigidbody = GetComponent<Rigidbody2D>();
        }

        private void Update()
        {
            float forward = Input.GetAxis("Vertical");
            float right = Input.GetAxis("Horizontal");
            if (IsMoving) Move(forward, right);
        }

        private void Move(float forward, float right)
        {
            _mainRigidbody.velocity = new Vector2(_moveSpeed * right, _moveSpeed * forward);
        }
    }
}