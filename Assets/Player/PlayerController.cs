using Godot;
using System;
using System.Diagnostics;

public partial class PlayerController : Node
{
    [Export] private float speed = 20.0f;

    [Export] private double shootingCD = 0.25f;

    [Export] private PackedScene Bullet;

    Vector3 RotationLimit = new Vector3 (0, 0, 0);

    Vector3 PlayerLimit = new Vector3(0, 0, 0);

    Node3D player;

    Node3D bulletOffset;

    bool isReady = true;


    public override void _Ready()
    {
        base._Ready();

        player = this.GetParent<Node3D>();
        bulletOffset = player.GetNode<Node3D>("BulletOffset");
        if (bulletOffset == null)
        {
            bulletOffset = player;
        }
    }

    public override void _Process(double delta)
    {
        base._Process(delta);
        input(delta);
    }
    // input handler
    private async void input(double delta)
    {
        float Horizontal = Input.GetAxis("Left", "Right");
        float Vertical = Input.GetAxis("Up", "Down");

        if (player != null)
        {
            Vector3 offset = new Vector3(Horizontal * speed * (float)delta, 0, Vertical * speed * (float)delta).Normalized();
            GD.Print(offset);
            // Moving controls
            player.Position = player.Position += offset;

            

            // Player rotation while moving
            player.RotateZ(Horizontal);
            RotationLimit.Z = -Mathf.Clamp(Horizontal, -0.5f, 0.5f);

            player.Rotation = RotationLimit;
            

            
            // holding down shoot button
            if (Input.IsActionPressed("Shoot") && isReady == true)
            {
                isReady = false;
                Node3D instance = Bullet.Instantiate<Node3D>();
                
                player.GetParent<Node3D>().AddChild(instance);
                instance.GlobalTransform = bulletOffset.GlobalTransform;


                GD.Print("shooting");
                await(ToSignal(GetTree().CreateTimer(shootingCD), SceneTreeTimer.SignalName.Timeout));
                isReady = true;

            }
        }
    }
}