using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

namespace Subconscionus.PianoPuzzle
{
    public class PianoKeyController : MonoBehaviour
    {
        public PianoPuzzleController PianoPuzzleControllerS { get; set; }
        public AudioSource PianoAudioSource { get; set; }
        /// <summary>
        /// Звуки ноты, которую должна проиграться при нажатии на клавишу
        /// </summary>
        //[SerializeField] private AudioClip _soundClip;

        public int MainIndex { get; set; }

        public void CheckingKey()
        {
            //PianoAudioSource.PlayOneShot(_soundClip);
            if (MainIndex == PianoPuzzleControllerS.CurrentCorrectKey)
            {
                PianoPuzzleControllerS.CurrentBetweenTaps = 0f;
                PianoPuzzleControllerS.CurrentCorrectKeyNumber++;
                int i = PianoPuzzleControllerS.CurrentCorrectKeyNumber;
                if (i < PianoPuzzleControllerS.CorrectKeyCombination.Length)
                    PianoPuzzleControllerS.CurrentCorrectKey = PianoPuzzleControllerS.CorrectKeyCombination[PianoPuzzleControllerS.CurrentCorrectKeyNumber];
                else 
                {
                    PianoPuzzleControllerS.CurrentCorrectKeyNumber = 0;
                    PianoPuzzleControllerS.CurrentCorrectKey = PianoPuzzleControllerS.CorrectKeyCombination[0];
                    PianoPuzzleControllerS.gameObject.SetActive(false);
                } 
                PianoPuzzleControllerS.Result = "Угадал!";
            }
            else
            {
                PianoPuzzleControllerS.CurrentCorrectKeyNumber = 0;
                PianoPuzzleControllerS.CurrentCorrectKey = PianoPuzzleControllerS.CorrectKeyCombination[0];
                PianoPuzzleControllerS.Result = "Не угадал!";
            }
        }
    }
}
