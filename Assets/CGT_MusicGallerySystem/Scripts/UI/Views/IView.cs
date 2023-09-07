namespace CGT.MusicGallery
{
    public interface IView 
    {
        void Refresh();
        SongEntry Song { get; set; }
    }
}