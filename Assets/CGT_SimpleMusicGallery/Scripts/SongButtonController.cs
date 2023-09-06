using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using ButtonClick = UnityEngine.UI.Button.ButtonClickedEvent;

namespace CGT.MusicGallery
{
    public class SongButtonController : MonoBehaviour
    {
        [SerializeField] protected SongEntry song;
        [SerializeField] protected Button button;

        public virtual SongEntry Song
        {
            get { return song; }
            set 
            { 
                song = value;
                this.gameObject.name = $"{song.name}_Button";
                UpdateViewList();
                UpdateViewStates();
            }
        }

        protected virtual void UpdateViewList()
        {
            views.Clear();
            var whatWeFound = GetComponentsInChildren<IView>();
            views.AddRange(whatWeFound);
        }

        protected List<IView> views = new List<IView>();

        protected virtual void UpdateViewStates()
        {
            foreach (var registered in views)
            {
                registered.Song = this.Song;
                registered.Refresh();
            }
        }

        protected virtual void OnEnable()
        {
            UpdateViewList();
            UpdateViewStates();
            OnClick.AddListener(ResponseToClick);
        }

        public ButtonClick OnClick
        {
            get { return button.onClick; }
        }

        protected virtual void ResponseToClick()
        {
            UpdateViewList();
            UpdateViewStates();
            AnyClicked.Invoke(song);
        }

        public static System.Action<SongEntry> AnyClicked = delegate { };

        protected virtual void OnDisable()
        {
            OnClick.RemoveListener(ResponseToClick);
        }
    }
}