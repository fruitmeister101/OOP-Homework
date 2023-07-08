class SquareListener
{
    Unit parent;
    Square.Updated listener;
    SquareListener(int x, int y)
    {
        listener = new Square.Updated(SquareUpdated);
        ListenToSquare(x, y);
    }
    void SquareUpdated(Square square)
    {
        parent.SquareInRangeUpdated(square);
    }
    void ListenToSquare(int x, int y)
    {
        GM.board.GetSquare(x, y).updated += listener;
    }
    public void StopListeningToSquare(int x, int y)
    {
        GM.board.GetSquare(x, y).updated -= listener;
    }
}