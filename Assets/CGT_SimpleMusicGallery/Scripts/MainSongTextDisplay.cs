using UnityEngine;
using TMPro;
using System.Text.RegularExpressions;

namespace CGT.MusicGallery
{
    public class MainSongTextDisplay : MonoBehaviour
    {
        [SerializeField] protected TextMeshProUGUI nameLabel;
        [SerializeField] protected TextMeshProUGUI commentText;

        protected virtual void Awake()
        {
            nameLabel.text = commentText.text = string.Empty;
        }

        protected virtual void OnEnable()
        {
            SongButtonController.AnyClicked += UpdateTextFields;
            Myceliaudio.OnMusicPlay += UpdateTextFields;
        }

        protected virtual void UpdateTextFields(SongEntry song)
        {
            string nameToGoWith, commentsToGoWith;

            if (song.IsLocked)
            {
                nameToGoWith = song.UnlockName;
                commentsToGoWith = song.UnlockComments;
            }
            else
            {
                nameToGoWith = song.LockName;
                commentsToGoWith = song.LockComments;
            }

            nameToGoWith = newlines.Replace(nameToGoWith, " ");
            nameToGoWith = consecutiveSpaces.Replace(nameToGoWith, "");
            
            nameLabel.text = nameToGoWith;
            commentText.text = commentsToGoWith;
        }

        protected static Regex newlines = new Regex(@"\n+");
        protected static Regex consecutiveSpaces = new Regex(@"\s\s+");

        protected virtual void OnDisable()
        {
            SongButtonController.AnyClicked -= UpdateTextFields;
            Myceliaudio.OnMusicPlay -= UpdateTextFields;
        }
    }
}