using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Subconscionus.PianoPuzzle
{
    public class PianoPuzzleController : MonoBehaviour
    {
        /// <summary>
        /// ћассив всех клавиш пианино (заполн€етс€ в ручную)
        /// </summary>
        [SerializeField] private PianoKeyController[] _pianoKeysS;

        public int[] CorrectKeyCombination { get { return _correctKeyCombination; } }
        public int CurrentCorrectKey { get { return _correctKeyCombination[CurrentCorrectKeyNumber]; } set { _currentCorrectKey = value.ToString(); } }
        public int CurrentCorrectKeyNumber { get; set; } = 0;
        [SerializeField] private int[] _correctKeyCombination;

        [SerializeField] private Text _currentCorrectText;
        [SerializeField] private Text _resultText;
        private string _currentCorrectKey { get { return _currentCorrectText.text; } set { _currentCorrectText.text = value; } }
        public string Result { get { return _resultText.text; } set { _resultText.text = value; } }

        public float CurrentBetweenTaps { get; set; } = 0f; // им€ по адекватней надо) —обытие нужно
        /// <summary>
        /// ћаксимальное врем€ между нажати€ми на клавиши
        /// </summary>
        [SerializeField] private float _maxBetweenTaps;

        private void Start()
        {
            CurrentCorrectKey = _correctKeyCombination[0];
            for (int i = 0; i < _pianoKeysS.Length; i++)
            {
                _pianoKeysS[i].PianoPuzzleControllerS = GetComponent<PianoPuzzleController>();
                _pianoKeysS[i].PianoAudioSource = GetComponent<AudioSource>();
                _pianoKeysS[i].MainIndex = i;
            }
        }

        private void Update()
        {
            CurrentBetweenTaps += Time.deltaTime;
            if (CurrentBetweenTaps > _maxBetweenTaps)
            {
                CurrentCorrectKeyNumber = 0;
                CurrentCorrectKey = _correctKeyCombination[0];
                CurrentBetweenTaps = 0f;
            }
        }
    }
}