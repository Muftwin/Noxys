using Godot;
public partial class InputBuffer
{
    //bool spaceWasDown = false;

    public float spaceLastUp;
    public float spaceLastDown = 999999f;

    public bool usedTheSpaceToJumpAlready = false;

    public float shiftLastUp;
    public float shiftLastDown = 999999f;

    public bool usedTheShiftToDashAlready = false;

    public float lastOnTheFloor = 999999f;
    public float yCoordinateLastOnTheFloor;

    public float leftLastUp;
    public float rightLastUp;
    public bool leftMoreRecentThanRight; //Assuming you are pressing left

    public void update(float delta)
    {
        spaceLastUp += delta;
        spaceLastDown += delta;
        if (Input.IsKeyPressed(Key.Space))
            spaceLastDown = 0;
        else
        {
            spaceLastUp = 0;
            usedTheSpaceToJumpAlready = false;
        }

        shiftLastUp += delta;
        shiftLastDown += delta;
        if (Input.IsKeyPressed(Key.Shift))
            shiftLastDown = 0;
        else
        {
            shiftLastUp = 0;
            usedTheShiftToDashAlready = false;
        }

        lastOnTheFloor += delta;
        leftLastUp += delta;
        rightLastUp += delta;
        if (!Input.IsKeyPressed(Key.Left))
            leftLastUp = 0;
        if (!Input.IsKeyPressed(Key.Right))
            rightLastUp = 0;

        leftMoreRecentThanRight = leftLastUp < rightLastUp;

        //spaceWasDown = Input.IsKeyPressed(Key.Space); //do this last so we can check if it changed next time //do i actually need this?
    }
}
