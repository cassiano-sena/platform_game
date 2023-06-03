using Godot;
using System;

public partial class Jogador : CharacterBody2D
{
	private Vector2 velocity;
	private Vector2 direction;
	public const float Speed = 200.0f;
	public const float JumpVelocity = -400.0f;

	// Get the gravity from the project settings to be synced with RigidBody nodes.
	public float gravity = ProjectSettings.GetSetting("physics/2d/default_gravity").AsSingle();

	private AnimatedSprite2D animate;

	public override void _Ready() {
		animate = GetNode<AnimatedSprite2D>("AnimatedSprite2D");
	} //fim do ready

	public override void _PhysicsProcess(double delta)
	{
		//inercia inicial
		velocity = Velocity;

		// Add the gravity.
		if (!IsOnFloor())
			velocity.Y += gravity * (float)delta;

		// Handle Jump.
		if (Input.IsActionJustPressed("Space") && IsOnFloor())
			for(int i=0;i<2;i++){
				velocity.Y = JumpVelocity;
			}

		// Get the input direction and handle the movement/deceleration.
		// As good practice, you should replace UI actions with custom gameplay actions.
		// direction = Input.GetVector("ui_left", "ui_right", "ui_up", "ui_down");
		direction = Input.GetVector("Left", "Right", "Up", "Down");
		if (direction != Vector2.Zero)
		{
			if(!IsOnFloor()){
				velocity.X = direction.X * (Speed/3);
			}else velocity.X = direction.X * Speed;
		}
		else
		{
			//velocity.X = 0;
			velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed*(float)(4*delta));
		}

		Velocity = velocity;
		MoveAndSlide();

		Animation(velocity);

	} //fim do process

	private void Animation(Vector2 velocity) {
		if (!IsOnFloor()){
			// if(Input.IsActionJustPressed("Down")){
			// 	animate.Play("run_look_down");
			// }if(Input.IsActionJustPressed("Up")){
			// 	animate.Play("run_look_up");
			// }
			// else 
			animate.Play("airborne");
		}

		if(Input.IsActionJustPressed("Left")){
			animate.Play("run");
		}
		if(Input.IsActionJustPressed("Right")){
			animate.Play("run");
		}
		if(Input.IsActionJustPressed("Up")){
			animate.Play("idle_look_up");
		}
		if(Input.IsActionJustPressed("Down")){
			animate.Play("idle_look_down");
		}

		if (velocity.X != 0) {
			if(Input.IsActionJustPressed("Down")){
				animate.Play("run_look_down");
			}if(Input.IsActionJustPressed("Up")){
				animate.Play("run_look_up");
			}
			animate.Play("run");
		} else {
			animate.Play("idle");
		}

		// Scale=new Vector2(1,-1);

		//if(velocity.X=0){}
		if(velocity.X>0 && velocity.X!=0){
			animate.FlipH=true;
		}else if(velocity.X<0 && velocity.X!=0){
			animate.FlipH=false;
		}
	}

	public void SavePoint(){
		// GlobalPosition=;
	}

} //fim da classe
