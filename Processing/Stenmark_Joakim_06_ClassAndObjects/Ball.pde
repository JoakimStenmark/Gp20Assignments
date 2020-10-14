class Ball extends GameObject
{
	float ballSize;
	float ballRadius = ballSize/2;
	color ballColor;

	Ball(float x, float y)
	{
		super();
		ballColor = color(255,0,0);
		ballSize = random(20, 50);
       
        while (player.roundCollision(new PVector(x,y), ballSize)) 
        {
        	println("spawned on player. relocating");
        	x = random(width);
        	y = random(height);
        }
        position.set(x, y);
        velocity.x = random(210) - 100;
        velocity.y = random(210) - 100;
    }

    //Update our ball
    void update()
    {
    	position.x += velocity.x * deltaTime;
    	position.y += velocity.y * deltaTime;

    }

    void draw()
    {
    	stroke(2);
    	fill(ballColor);
    	ellipse(position.x, position.y, ballSize, ballSize);
    }

    void CheckEdges()
    {
    	if (position.y > height - ballRadius)
    	{
    		velocity.y *= -1;
    		position.y = height - ballRadius;
    	}
    	else if (position.y < 0 + ballRadius)
    	{
    		velocity.y *= -1;
    		position.y = 0 + ballRadius;
    	}

    	if (position.x < 0 + ballRadius)
    	{
			velocity.x *= -1;
			position.x = 0 + ballRadius;
		}
		else if (position.x > width - ballRadius)
		{
			velocity.x *= -1;
			position.x = width - ballRadius;
		}
	}
}
