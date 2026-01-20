using System.Collections.Generic;

namespace GoingPostal;

public class GameData
{
    public List<string> Highscores {get; set;} = [];

    public bool SoundEnabled { get; set; } = true;
}