boolean moveLeft = false;
boolean moveRight = false;
boolean moveUp = false;
boolean moveDown = false;
boolean gravityOn = false;

void keyPressed()
{
	if (keyCode == LEFT || key == 'a')
	{
		moveLeft = true;
	}
	else if (keyCode == RIGHT || key == 'd')
	{
		moveRight = true;
	}

	if (keyCode == UP || key == 'w')
	{
		moveUp = true;
	}
	else if (keyCode == DOWN || key == 's')
	{
		moveDown = true;
	}
	if (key == 'g')
	{
		if (gravityOn)
		{
			gravityOn = false;
		}
		else
		{
			gravityOn = true;
		}
	}

}

void keyReleased()
{
	if (keyCode == LEFT || key == 'a')
		moveLeft = false;
	else if (keyCode == RIGHT || key == 'd')
		moveRight = false;

	if (keyCode == UP || key == 'w')
		moveUp = false;
	else if (keyCode == DOWN || key == 's')
		moveDown = false;
}
