﻿using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace Recursos.EXPRESATE.PLANTILLAS.Scripts.Seleccion_Multiple
{
    public class MultipleChooseAnswer : MonoBehaviour
    {
        [FormerlySerializedAs("isRight")] public bool IsRight;
        [FormerlySerializedAs("isChoosed")] public bool IsChoosed;
        private Button _buttonComponent;
        private MultipleChooseQuestion _parent;


        private void Awake() {
            //Inicializa el componente button y el padre
            _buttonComponent = GetComponent<Button>();
            _parent = gameObject.transform.parent.GetComponent<MultipleChooseQuestion>();
        }

        private void OnEnable() {
            //Deselecciona las respuesta
            IsChoosed = false;
        }


        /// <summary>
        /// Busca el padre y el arreglo de los hijos de este, reasigna su tamaño, y los habilita para poder ser seleccionados
        /// </summary>
        public void ChooseAnswer() {
            if (_parent != null) {
                foreach (var elem in _parent.Answers) {
                    elem.gameObject.GetComponent<Button>().interactable = true;
                    elem.transform.localScale = Vector3.one;
                    elem.gameObject.GetComponent<MultipleChooseAnswer>().IsChoosed = false;
                }
            }

            IsChoosed = true;
            _buttonComponent.interactable = false;
//            transform.localScale = Vector3.one * 1.2f;  //Aumenta la escala del elemento
        }
    }
}