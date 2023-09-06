using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CGT.MusicGallery
{
    public class MusicPlayArgs : System.EventArgs
    {
        public virtual SongEntry Song { get; set; }
        public virtual MusicPlayer MusicPlayer { get; set; }
    }
}