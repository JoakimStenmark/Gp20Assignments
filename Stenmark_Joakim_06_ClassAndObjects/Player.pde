class Player extends GameObject
{
	float ballSize = 40;
	float ballRadius = ballSize/2; 

	PVector direction;
	float speed = 0;
	float acceleration = 200;
	float drag = 0.95;
	PVector gravity;
	color ballColor;

	int health;
	boolean haveCollided = false;

	Player()
	{
		super();

		direction = new PVector(0, 0);
		gravity = new PVector(0, 25);
		ballColor = color(0,255,0);
		health = 1;

	}

	void update()
	{
		ReadDirection();

		MoveObject();

		CheckEdges();

		ApplyDrag();

		DrawObject();
	}

	void DrawObject()
	{	
		stroke(2);
		fill(ballColor);
		ellipse(position.x, position.y, ballSize, ballSize);
		if (health == 0) 
		{
			ballSize = 0;

			textSize(64);
			fill(227, 1, 1);
			text("YOU DIED", 180, 94, 527, 515);
		}
	}


	void MoveObject()
	{
		if (keyPressed) 
		{
			speed += acceleration * deltaTime;
		}

		velocity.add(direction.mult(speed));

		if(gravityOn)
		{
			velocity.add(gravity);
		}
		PVector move = velocity.copy();
		position.add(move.mult(deltaTime));
	}

	void MoreControl()
	{

		velocity.normalize();
		velocity.mult(speed);
	}


	void ApplyDrag()
	{
		speed *= drag;
		velocity.mult(drag);
	}

	void CheckEdges()
	{
		if (position.y > height-ballRadius)
		{
			velocity.y *= -1;
			position.y = height - ballRadius;
		}
		else if (position.y < 0 + ballRadius)
		{
			velocity.y *= -1;
			position.y = 0 + ballRadius;
		}

		if (position.x < 0 - ballRadius)
		{
			position.x = width + ballRadius;
		}
		else if (position.x > width + ballRadius)
		{
			position.x = 0 - ballRadius;
		}
	}


	void ReadDirection()
	{
		direction.set (0, 0);

		if (moveLeft) 
		{
			direction.add(-1, 0);
		}
		if (moveRight)
		{
			direction.add(1, 0);
		}

		if (moveUp) 
		{
			direction.add(0, -1);

		}
		if (moveDown)
		{
			direction.add(0, 1);
		}

		direction.normalize();
	}

	boolean roundCollision(PVector otherPos, float otherSize)
	{
		float maxDistance = ballSize + otherSize;


	  if(abs(position.x - otherPos.x) > maxDistance || abs(position.y - otherPos.y) > maxDistance)
	  {
	  	return false;
	  }

	  else if(dist(position.x, position.y, otherPos.x, otherPos.y) > maxDistance)
	  {
	  	return false;
	  }

	  else
	  {
	  	return true;
	  }
	}
}
