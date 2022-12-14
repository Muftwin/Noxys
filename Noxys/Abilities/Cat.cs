using Godot;
public class Cat : Ability
{
    Player player;
    bool enabled = true; //allowed to use at all
    bool currentTransform = false; //currently using

    public bool climbing = false;
    public Cat(Player player)
    {
        this.player = player;
    }

    public void transform()
    {
        if (!enabled) return;

        player.character = Player.Character.CAT;
        player.setSprite("cat.png");
        currentTransform = true;
    }
    public void passive()
    {
        if (!enabled || !currentTransform) return;

        climbing = (player.IsOnWall() && player.speed.y <= 0);

    }
}