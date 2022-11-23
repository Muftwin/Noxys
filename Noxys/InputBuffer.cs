using Godot;
public class InputBuffer
{
    bool spaceWasDown = false;

    public float spaceLastUp;
    public float spaceLastDown;

    public bool usedTheSpaceToJumpAlready = false;

    public float lastOnTheFloor;
    public float yCoordinateLastOnTheFloor;

    public float leftLastUp;
    public float rightLastUp;
    public bool leftMoreRecentThanRight; //Assuming you are pressing left

    public void update(float delta)
    {
        spaceLastUp += delta;
        spaceLastDown += delta;

        lastOnTheFloor += delta;

        if (Input.IsKeyPressed((int)KeyList.Space))
            spaceLastDown = 0;
        else
        {
            spaceLastUp = 0;
            usedTheSpaceToJumpAlready = false;
        }

        leftLastUp += delta;
        rightLastUp += delta;
        if (!Input.IsKeyPressed((int)KeyList.Left))
            leftLastUp = 0;
        if (!Input.IsKeyPressed((int)KeyList.Right))
            rightLastUp = 0;

        leftMoreRecentThanRight = leftLastUp < rightLastUp;

        spaceWasDown = Input.IsKeyPressed((int)KeyList.Space); //do this last so we can check if it changed next time //do i actually need this?
    }
}