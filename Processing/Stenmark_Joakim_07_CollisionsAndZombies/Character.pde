class Character
{
	float size;
	float radius;
	color _color;
	PVector position;
	PVector velocity;
	PVector direction;
	PVector arm1VectorOffset;
	PVector arm2VectorOffset;

	Character(float x, float y)
	{
		//position.set(pos);
		position = new PVector(x, y);
		_color = color(random(100,256), 25, random(50,150));
		size = random(30,40);
		radius = size/2;
		velocity = new PVector(random(210) - 100, random(210) - 100);
		//velocity.div(2);
		direction = new PVector(0,0);
		arm1VectorOffset = new PVector(0,0);
		arm2VectorOffset = new PVector(0,0);

	}

	void update()
	{
		direction.set(velocity);
		direction.normalize();

		arm1VectorOffset.set(direction);
		arm1VectorOffset.rotate(QUARTER_PI);
		arm1VectorOffset.mult(radius);

		arm2VectorOffset.set(direction);
		arm2VectorOffset.rotate(-QUARTER_PI);
		arm2VectorOffset.mult(radius);

		position.x += velocity.x * deltaTime;
		position.y += velocity.y * deltaTime;
	}

	void draw()
	{
		stroke(255);
		strokeWeight(1);

		fill(_color);
		ellipse(position.x, position.y, size, size);


	}

	void CheckEdges()
	{
		if (position.y > height + radius)
		{
    		//velocity.y *= -1;
    		position.y = 0 - radius;
    	}
    	else if (position.y < 0 - radius)
    	{
    		//velocity.y *= -1;
    		position.y = height + radius;
    	}

    	if (position.x < 0 - radius)
    	{
			//velocity.x *= -1;
			position.x = width + radius;
		}
		else if (position.x > width + radius)
		{
			//velocity.x *= -1;
			position.x = 0 - radius;
		}
	}
}
