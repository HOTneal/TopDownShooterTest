﻿using Managers;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Controllers.MobileController
{
    public class MobileInputController : MonoBehaviour, IDragHandler, IEndDragHandler, IPointerUpHandler, IPointerDownHandler
    {
        [SerializeField] private float m_MaxJoyMoveAway = 2.0f;

        private LinkManager m_LinkManager;
        private Image m_BgJoystick;
        private Image m_MoveStick;
        private Vector2 m_InputVector;

        private void Start()
        {
            m_LinkManager = LinkManager.instance;
            m_BgJoystick = GetComponent<Image>();
            m_MoveStick = transform.GetChild(0).GetComponent<Image>();
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
            m_LinkManager.player.inputController.isCanShootMouse = false;
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            m_InputVector = Vector2.zero;
            m_MoveStick.rectTransform.anchoredPosition = Vector2.zero;
            m_LinkManager.player.inputController.isCanShootMouse = true;
        }

        public virtual void OnDrag(PointerEventData eventData)
        {
            Vector2 m_Pos;

            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(m_BgJoystick.rectTransform, eventData.position,
                eventData.pressEventCamera, out m_Pos)) return;

            var sizeDelta = m_BgJoystick.rectTransform.sizeDelta;
            m_Pos.x = (m_Pos.x / sizeDelta.x);
            m_Pos.y = (m_Pos.y / sizeDelta.y);

            m_InputVector = new Vector2(m_Pos.x * m_MaxJoyMoveAway , m_Pos.y * m_MaxJoyMoveAway);
            m_InputVector = (m_InputVector.magnitude > 1.0f) ? m_InputVector.normalized : m_InputVector;

            m_MoveStick.rectTransform.anchoredPosition = new Vector2(m_InputVector.x * (sizeDelta.x / m_MaxJoyMoveAway), m_InputVector.y * (sizeDelta.y / m_MaxJoyMoveAway));
        }

        public virtual void OnEndDrag(PointerEventData eventData)
        {

        }

        public float Horizontal()
        {
            return m_InputVector.x;
        }

        public float Vertical()
        {
            return m_InputVector.y;
        }
    }
}
