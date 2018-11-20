﻿using System;
using Navegation;
using Resource.MELI.AI_MELI_MOD1_ANIMALES_EN_LA_MIRA.Scripts.AI_MELI_MOD1_ANIMALES_EN_LA_MIRA.Navegation;
using Resource.MELI.AI_MELI_MOD1_ANIMALES_EN_LA_MIRA.Scripts.AI_MELI_MOD1_ANIMALES_EN_LA_MIRA.Score;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.UI;

namespace Resource.LIBRO_C.AI_MELI_MOD1_ANIMALES_EN_LA_MIRA.Scripts.AI_MELI_MOD1_ANIMALES_EN_LA_MIRA.Score {
    /// <inheritdoc />
    /// <summary>
    /// Determina los puntajes del juego
    /// </summary>
    public class ScoreManager : MonoBehaviour {
        [Tooltip("Asigna el resultado de los intentos de forma positiva y negativa")] [SerializeField]
        private bool AssignResultText;

        [SerializeField] private double _numQuestion, _rightAnswer, _failedAnswers;
        [SerializeField] private Text _textResult, _numResult;

        public int TestTries, ActivitiesTries;
        private int _testTries, _activitiesTries;


        [Header("Componente de navegacion")] [SerializeField]
        private NavegationManager _navegationManager;

        [Tooltip("Al activar esta opcion resetea las actividades añadidas al componente QuestWork")]
        public bool ResetQuestWork;

        [SerializeField] [EnableIf("ResetQuestWork", true)]
        private GameObject[] _QuestWork;


        [EnableIf("AssignResultText", true)] [SerializeField]
        private Text _rightText, _wrongText;

        private void Start() {
            _testTries = TestTries;
            _activitiesTries = ActivitiesTries;
        }

        /// <summary>
        /// Aumenta el numero de respuestas correctas
        /// </summary>
        public void IncreaseScore() {
            _rightAnswer += 1;
            if (_rightText != null && AssignResultText) {
                _rightText.text = _rightAnswer + "";
            }
        }

        public void ReduceScore() {
            _failedAnswers += 1;
            if (_wrongText != null && AssignResultText) {
                _wrongText.text = _failedAnswers + "";
            }
            
        }


        /// <summary>
        /// Redduce el numero de intentos disponibles
        /// </summary>
        public void DecreaseTries() {
            if (_navegationManager.GetLayoutActual().GetComponent<LayoutManager>().Escena ==
                LayoutManager.TipoEscena.Actividad) {
                //Debug.Log(_navegationManager.GetLayoutActual().name);
                if (ActivitiesTries > 0) {
                    ActivitiesTries -= 1;
                }
            }

            if (_navegationManager.GetLayoutActual().GetComponent<LayoutManager>().Escena ==
                LayoutManager.TipoEscena.Conozco) {
                Debug.Log(_navegationManager.GetLayoutActual().name);
                if (TestTries > 0) {
                    TestTries -= 1;
//                    Debug.Log("Current test tries " + TestTries);
                }
            }

//            Debug.Log("Current tries " + ActivitiesTries);
        }


        public bool CheckDelay() {
            return TestTries < 0 || ActivitiesTries < 0;
        }


        /// <summary>    
        /// Restore the number of tries 
        /// </summary>
        private void RestoreTries() {
            TestTries = _testTries;
            ActivitiesTries = _activitiesTries;
        }

        /// <summary>
        /// Asigna el puntaje obtenido
        /// </summary>
        public void AsignScore() {
            _textResult.text = "" + Math.Round(_rightAnswer / _numQuestion * 100) + "%";
            _numResult.text = _rightAnswer + "/" + _numQuestion;
        }


        /// <summary>
        /// Resetea el puntaje final y el numero de respuestas correctas.
        /// </summary>
        public void ResetScore() {
            _rightAnswer = _failedAnswers = 0;
            if (_rightText != null || _wrongText != null) {
               
                _rightText.text = _wrongText.text = "" + 0;
            }

            RestoreTries();
            if (ResetQuestWork) {
                foreach (var t in _QuestWork) {
                    var questWork = t.GetComponentsInChildren<QuestWork>();
                    var images = t.GetComponentsInChildren<Image>();
                    var buttons = t.GetComponentsInChildren<Button>();
                    foreach (var quest in questWork)
                        quest.AnswerValue =
                            quest.IsValuable ? QuestWork.AnswerStatus.None : QuestWork.AnswerStatus.Locked;
                    foreach (var button in buttons) button.interactable = true;
                    foreach (var elem in images) elem.raycastTarget = true;
                }
            }


//            Debug.Log("_rightAnswer " + _rightAnswer + "_numQuestion " + _numQuestion);
        }

        /// <summary>
        /// Desactiva la iteracion con elementos de la actividad tras obtner el numero de intentos maximo
        /// </summary>
        public void DisableActivities() {
            foreach (var t in _QuestWork) {
                var buttons = t.GetComponentsInChildren<Image>();
                foreach (var elem in buttons) elem.raycastTarget = false;
            }
        }
    }
}