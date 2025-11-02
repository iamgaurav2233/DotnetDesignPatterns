namespace TicTacToe
{
    public class Player
    {
        public string name;
        public PlayingPiece playingPiece;

        public Player(string name, PlayingPiece playingPiece)
        {
            this.name = name;
            this.playingPiece = playingPiece;
        }

        public string GetName()
        {
            return name;
        }
        public void setName(string name)
        {
            this.name = name;
        }

        public PlayingPiece GetPlayingPiece()
        {
            return playingPiece;
        }

        public void SetPlayingPiece(PlayingPiece playingPiece)
        {
            this.playingPiece = playingPiece;
        }
    }
}