namespace Snake
{
    public enum Direction
    {
        Up, Down, Left, Right
    }

    public enum BonusItemType
    {
        Brake           = 0, //smanji brzinu
        SpeedUp           = 1, //poveca brzinu
        Cut         = 2, //smanji duzinu zmije
        MultiplierX2    = 3, //svaki novcic nosi duplo vise bodova
        MultiplierX3    = 4, // svaki novcic nosi troduplo vise bodova
        ExtraPoints     = 5, //extra points
        Reverse         = 6, //zmija krene u suprotnom pravuc
        Ghost           = 7, //zija prolazi kroz zapreke
        Count,
    }

    public enum Screens
    {
        Menu,
        Game,
        GameOver,
        LevelEditor,
        GameWin,
        Highscores
    }

    public enum LoadedFonts
    {
        SegoeUIMono,
        KristenITC,
        CourierNew_fs8,
        KristenITC_fs24,
        KristenITC_fs18,
        KristenITC_fs10
    }

    internal enum ObstacleType
    {
        Basic, Line, Square
    }

    public enum SortMethod
    {
        Ascending,
        Descending
    }
}
