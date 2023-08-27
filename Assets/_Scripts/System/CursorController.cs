using UnityEngine;

namespace JustGame.Scripts.Common
{
    public class CursorController : MonoBehaviour
    {
        [SerializeField] private Texture2D m_crosshair;
        private void Start()
        {
            SetCursor();
        }

        public void SetCursor()
        {
            Cursor.SetCursor(m_crosshair,new Vector2(m_crosshair.width/2f, m_crosshair.height/2f),CursorMode.Auto);
        }
    }
}

