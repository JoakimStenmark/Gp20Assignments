class Zombie extends Character
{
	
	Zombie(float x, float y)
	{
		super(x,y);
		velocity.div(2);
		_color = color(50, random(100,200), 50);
		
	}
	Zombie(float x, float y, float s)
	{
		super(x,y);
		size = s;
		radius = size/2;
		velocity.div(2);
		_color = color(50, random(100,200), 50);
	}


	void draw()
	{
		stroke(255);
		strokeWeight(1);

		fill(_color);
		ellipse(position.x, position.y, size, size);
		
		//DRAW ARMS
		strokeWeight(4);
		stroke(_color);
		direction.mult(20);
		line(position.x + arm1VectorOffset.x, position.y + arm1VectorOffset.y, position.x + direction.x +arm1VectorOffset.x, position.y + direction.y + arm1VectorOffset.y);
		line(position.x + arm2VectorOffset.x, position.y + arm2VectorOffset.y, position.x + direction.x +arm2VectorOffset.x, position.y + direction.y + arm2VectorOffset.y);
		
	}


}
