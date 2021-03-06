﻿using AI_MELI_MOD1_ANIMALES_EN_LA_MIRA.Audio;
using Recursos.MELI.AI_MELI_MOD1_ANIMALES_EN_LA_MIRA.Scripts.AI_MELI_MOD1_ANIMALES_EN_LA_MIRA.Misc;
using Recursos.MELI.AI_MELI_MOD1_ANIMALES_EN_LA_MIRA.Scripts.AI_MELI_MOD1_ANIMALES_EN_LA_MIRA.Navegation;
using Resource.LIBRO_C.AI_MELI_MOD1_ANIMALES_EN_LA_MIRA.Scripts.AI_MELI_MOD1_ANIMALES_EN_LA_MIRA.Score;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.UI;

namespace Recursos.EXPRESATE.Lenguaje.PLANTILLAS.Prefabs.Seleccion_Objecto_con_Validar.Scripts
{
    /// <summary>
    /// Componente respuesta para las actividades de Seleccion
    /// </summary>
    public class AnwserChooseManager : MonoBehaviour
    {
        public bool IsRight;
        [SerializeField] private FXAudio _fxAudio;
        [SerializeField] private ScoreManager _scoreManager;
        [SerializeField] public Sprite Choosed, Right, Wrong;
        [SerializeField] public Status status;
        [SerializeField] public QuestionChooseManager _QuestionChooseManager;

        private Image _imageComponent;
        private Sprite _originalSprite;


        public enum Status
        {
            None,
            Choosed,
        }


        private void Awake() {
            FillComponents();
        }

        private void Start() {
            status = Status.None;
            _originalSprite = _imageComponent.sprite;
        }


        /// <summary>
        /// Rellena los componentes automaticamente si no hay nigun elemento en el inspector
        /// </summary>
        private void FillComponents() {
            _fxAudio = _fxAudio == null
                ? GameObject.FindGameObjectWithTag(TAGS.FXAUDIO).GetComponent<FXAudio>()
                : _fxAudio;
            _scoreManager = _scoreManager == null
                ? GameObject.FindGameObjectWithTag(TAGS.SCORE_MANAGER).GetComponent<ScoreManager>()
                : _scoreManager;
            _QuestionChooseManager = _QuestionChooseManager == null
                ? GetComponentInParent<QuestionChooseManager>()
                : _QuestionChooseManager;
            _imageComponent = GetComponent<Image>();
        }

        /// <summary>
        /// Remplaza el sprite asignado al componente
        /// </summary>
        /// <param name="image">Sprite a remplazar</param>
        public void AssignSprite(Sprite image) {
            _imageComponent.sprite = image;
        }


        /// <summary>
        /// Asigna una imagen y modifica el estad
        /// </summary>
        public void ChooseAnswer() {
            if (_QuestionChooseManager.AnswerOptions > 0) {
                switch (status) {
                    case Status.None:
                        status = Status.Choosed;
                        _QuestionChooseManager.AnswerOptions--;
                        _fxAudio.PlayAudio(0);
                        AssignSprite(Choosed);
                        break;
                    case Status.Choosed:
                        ResetAnswer();
                        _QuestionChooseManager.AnswerOptions++;
                        _fxAudio.PlayAudio(0);
                        break;
                }
            }

            else if (_QuestionChooseManager.AnswerOptions == 0) {
                if (status == Status.Choosed) {
                    ResetAnswer();
                    _QuestionChooseManager.AnswerOptions++;
                    _fxAudio.PlayAudio(0);
                }
            }
        }

        public void ResetAnswer() {
            status = Status.None;
            AssignSprite(_originalSprite);
        }
    }
}