using Godot;
public class Bat : Ability
{
    Player player;
    bool enabled = true; //allowed to use at all
    bool currentTransform = false; //currently using
    public Bat(Player player)
    {
        this.player = player;
    }

    public bool transform()
    {
        if (!enabled)
            return false;

        player.character = Player.Character.BAT;
        currentTransform = true;
        return true;
    }
    public void passive()
    {
        if (!enabled || !currentTransform)
            return;

        if (Input.IsKeyPressed((int)KeyList.Up) || Input.IsKeyPressed((int)KeyList.W))
            player.gravity = 1f;
        else
            player.gravity = 5f;
    }
}