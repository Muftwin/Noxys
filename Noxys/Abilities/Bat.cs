using Godot;
public class Bat : Ability
{
    Player player;
    bool enabled = true; //allowed to use at all
    bool currentTransform = false; //currently using

    public bool slowFall = false;

    public Bat(Player player)
    {
        this.player = player;
    }

    public bool transform()
    {
        if (!enabled)
            return false;

        player.character = Player.Character.BAT;
        player.setSprite("bat.png");
        currentTransform = true;
        return true;
    }
    public void passive()
    {
        slowFall = (Input.IsKeyPressed((int)KeyList.Up) || Input.IsKeyPressed((int)KeyList.W));
        player.sprite.Rotation = 0; //this is dumb?

        if (!enabled || !currentTransform)
            return;

        if (player.speed.x != 0)
            player.sprite.Rotation = -100 * player.facing;


    }
}