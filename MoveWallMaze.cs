using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Subconscionus.PuzzleMaze
{
    public class MoveWallMaze : MonoBehaviour
    {
        private TimerMaze _timerMazeS;
        private ChangeMazeController _mazeControllerS;
        /// <summary>
        /// индекс новой позиции в списке новых позиций
        /// </summary>
        private int _index = 0;
        public int Index { get { return _index; } set { _index = value; } }

        private bool _isMoving = false;
        private bool a = true;  // просто служит для запрета повторения действия
        private bool b = true;  // просто служит для запрета повторения действия
        private bool c = false; // просто служит для запрета повторения действия

        private void Start()
        {
            _mazeControllerS = GetComponent<ChangeMazeController>();
            _timerMazeS = _mazeControllerS.TimerMazeS;
        }

        private void Update()
        {
            ConfirmedMazeChanges();
        }

        private void ConfirmedMazeChanges()
        {
            if (ChangeMazeController.NoChangesLeftMaze > 0 && ChangeMazeController.NoChangesLeftMaze == TimerMaze.Minutes && TimerMaze.Seconds == 0f)
            {
                _isMoving = true;
            }
            if (_isMoving)
            {
                Move(_mazeControllerS.ChangesPostionWall);
            }
            else if (!_isMoving && c)
            {
                ChangeIndex();
            }
        }

        private void Move(List<Vector3> newPositions)
        {
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, newPositions[_index], _mazeControllerS.SpeedOffsetWall * Time.deltaTime);
            if (transform.localPosition == newPositions[_index])
            {
                if (_index <= _mazeControllerS.OffsetWalls.Length - 1)
                {
                    if (ChangeMazeController.NoChangesLeftMaze != 0) // после последнего изменения в консоль выскочит Только одна ошибка, она ни на что не влияет!!! ПРОВЕРЕНО
                    {
                        Debug.ClearDeveloperConsole();
                        if (_mazeControllerS.OffsetWalls[_index + 1] == ChangeMazeController.OffsetWall.X)
                        {
                            if (a)
                            {
                                _mazeControllerS.ChangesPostionWall.Add(new Vector2(transform.localPosition.x + _mazeControllerS.ValueOffsetPosition[_index + 1], transform.localPosition.y));
                                a = false;
                            }
                        }

                        if (_mazeControllerS.OffsetWalls[_index + 1] == ChangeMazeController.OffsetWall.Y)
                        {
                            if (a)
                            {
                                _mazeControllerS.ChangesPostionWall.Add(new Vector2(transform.localPosition.x, transform.localPosition.y + _mazeControllerS.ValueOffsetPosition[_index + 1]));
                                a = false;
                            }
                        }
                    }
                    _isMoving = false;
                }
            }
            c = true;
            return;
        }

        private void ChangeIndex()
        {
            if (b)
            {
                _index++;
                b = false;
            }
            c = false;
            a = true;
            b = true;
            return;
        }
    }
}