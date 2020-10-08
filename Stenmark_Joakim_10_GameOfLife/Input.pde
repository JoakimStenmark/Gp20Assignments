boolean moveLeft = false;
boolean moveRight = false;
boolean moveUp = false;
boolean moveDown = false;
boolean pause = true;

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
	if (key == 'p')
	{
		pause = !pause;
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
