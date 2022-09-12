using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Subconscionus.PuzzleMaze
{
    public class TimerMaze : MonoBehaviour
    {
        private MazePlayerController _player;

        /// <summary>
        /// Время на прохождение лабиринта в секундах
        /// </summary>
        [SerializeField] private float _times;
        [SerializeField] private float _timeLeft = 0f;
        public static float Minutes;
        public static float Seconds;

        private bool _timerOn = false;

        private void Awake()
        {
            _player = FindObjectOfType<MazePlayerController>();
            _timerOn = true;
            _timeLeft = _times;
            ChangeMazeController.DefaultChangesLeftMaze = Mathf.FloorToInt(_times / 60f);
            ChangeMazeController.DefaultChangesLeftMaze--; // общее количевство изменений должно быть на один меньше, чем количевство минут
        }

        private void Update()
        {
            if (_timerOn)
            {
                if (_timeLeft > 0)
                {
                    _timeLeft -= Time.deltaTime * 10f;
                    UpdateTimer();
                }
                else
                {
                    _timeLeft = _times;
                    for (int i = 0; i < ChangeMazeController.ChangeMazeControllers.Count; i++)
                    {
                        ChangeMazeController controller = ChangeMazeController.ChangeMazeControllers[i];
                        ChangeMazeController.ChangeMazeControllers[i].gameObject.transform.localPosition = controller.StartPosition;
                        ChangeMazeController.ChangeMazeControllers[i].GetComponent<MoveWallMaze>().Index = 0;
                        controller.ChangesPostionWall.RemoveRange(1, controller.ChangesPostionWall.Count - 1);                      
                    }
                    _player.IsMoving = false;
                    _timerOn = false;
                }
            }
        }

        private void UpdateTimer()
        {
            if (_timeLeft < 0f) _timeLeft = 0f;

            Minutes = Mathf.FloorToInt(_timeLeft / 60);
            Seconds = Mathf.FloorToInt(_timeLeft % 60);
            ChangeMazeController.NoChangesLeftMaze = Mathf.FloorToInt(Minutes);
        }
    }
}