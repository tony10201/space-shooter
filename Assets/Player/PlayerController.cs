using Godot;
using System;

public partial class PlayerController : Node
{
    [Export]
    public float speed = 20.0f;

    public float shootingCD = 0.5f;

    Vector3 PlayerLimit = new Vector3(0, 0, 0);

    Node3D player;


    public override void _Ready()
    {
        player = this.GetParent<Node3D>();
    }

    public override void _Process(double delta)
    {
        input(delta);
    }
    // input handler
    private void input(double delta)
    {
        float Horizontal = Input.GetAxis("Left", "Right");
        float Vertical = Input.GetAxis("Up", "Down");

        if (player != null)
        {
            // Moving controls
            player.Position = player.Position += new Vector3(Horizontal * speed, 0, Vertical * speed).Normalized();

            // holding down shoot button
            if (Input.IsActionPressed("Shoot"))
            {
                GD.Print("shooting");
            }
        }
    }
}