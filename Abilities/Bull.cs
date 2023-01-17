using Godot;
public class Bull : Ability
{
    Player player;
    bool enabled = true; //allowed to use at all
    bool currentTransform = false; //currently using

    public bool dashing = false;
    float dashTime = 0.25f;

    public bool cooldown = false;
    float cooldownTime = 0.5f;

    public Bull(Player player)
    {
        this.player = player;
    }

    public bool transform()
    {
        if (!enabled)
            return false;

        if (!cooldown)
        { //you can still transform on cooldown
            dashing = true;
            this.player.inputBuffer.usedTheShiftToDashAlready = true;
        }

        player.character = Player.Character.BULL;
        player.setSprite("cow.png");
        currentTransform = true;
        return true;
    }
    public void passive(float delta)
    {
        if (cooldown)
            cooldownTime -= delta;
        if (cooldownTime <= 0)
        {
            cooldown = false;
            cooldownTime = 0.5f;
        }

        if (!enabled || !currentTransform)
            return;

        if (dashing)
        {
            dashTime -= delta;
        }

        if (dashTime <= 0)
        {
            dashing = false;
            dashTime = 0.25f;
            cooldown = true;
        }
    }
}