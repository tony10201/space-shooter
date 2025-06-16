using Godot;
using System;
using System.Runtime.CompilerServices;

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

    //screen limits
    Vector2 HScreen_Limit = new Vector2(-30, 30);
    Vector2 VScreen_Limit = new Vector2(-25, 25);


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
        input(delta);
    }
    // input handler
    private async void input(double delta)
    {
        float Horizontal = Input.GetAxis("Left", "Right");
        float Vertical = Input.GetAxis("Up", "Down");

        if (player != null)
        {
            //blocks out of bounds
            //left Border
            if (player.Position.X <= HScreen_Limit.X)
            {
                if (Horizontal == -1)
                {
                    Horizontal = 0;
                }
            }
            //right Border
            else if (player.Position.X >= HScreen_Limit.Y)
            {
                if (Horizontal == 1)
                {
                    Horizontal = 0;
                }
            }
            // Bottom Border
            if (player.Position.Z >= VScreen_Limit.Y)
            {
                if (Vertical == 1)
                {
                    GD.Print("Bottom Hit");
                    Vertical = 0;
                }
            }
            // Top Border
            else if (player.Position.Z <= VScreen_Limit.X)
            {
                if (Vertical == -1)
                {
                    GD.Print("Top Hit");
                    Vertical = 0;
                }
            }
            // Moving controls
            Vector3 offset = new Vector3(Horizontal * speed * (float)delta, 0, Vertical * speed * (float)delta);
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