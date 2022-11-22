using Godot;
public class InputBuffer
{
    bool spaceWasDown = false;

    public float spaceLastUp;
    public float spaceLastDown;

    public bool usedTheSpaceToJumpAlready = false;

    public void update(float delta)
    {
        spaceLastUp += delta;
        spaceLastDown += delta;

        if (Input.IsKeyPressed((int)KeyList.Space))
            spaceLastDown = 0;
        else
        {
            spaceLastUp = 0;
            usedTheSpaceToJumpAlready = false;
        }

        spaceWasDown = Input.IsKeyPressed((int)KeyList.Space); //do this last so we can check if it changed next time //do i actually need this?
    }
}