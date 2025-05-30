using Godot;
using System;

public partial class PlayerController : Node
{
    [Export] private float speed = 20.0f;

    [Export] private float shootingCD = 0.5f;

    [Export] private PackedScene Bullet;

    Vector3 RotationLimit = new Vector3 (0, 0, 0);

    Vector3 PlayerLimit = new Vector3(0, 0, 0);

    Node3D player;

    Node3D bulletOffset;


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
    private void input(double delta)
    {
        float Horizontal = Input.GetAxis("Left", "Right");
        float Vertical = Input.GetAxis("Up", "Down");

        if (player != null)
        {
            // Moving controls
            player.Position = player.Position += new Vector3(Horizontal * speed, 0, Vertical * speed).Normalized();

            // Player rotation while moving
            player.RotateZ(Horizontal);
            RotationLimit.Z = -Mathf.Clamp(Horizontal, -0.5f, 0.5f);

            player.Rotation = RotationLimit;
            

            
            // holding down shoot button
            if (Input.IsActionPressed("Shoot"))
            {
                Node3D instance = Bullet.Instantiate<Node3D>();
                
                player.GetParent<Node3D>().AddChild(instance);
                instance.GlobalTransform = bulletOffset.GlobalTransform;


                GD.Print("shooting");

            }
        }
    }
}