class BallManager
{

	Ball ball;
	int numberOfBalls;
	Ball[] balls;

	int maxAmountOfBalls = 100;
	int timeToSpawn = 3000;
	int time;
	

	BallManager(int ballAmount)
	{
		numberOfBalls = ballAmount;
		balls = new Ball[numberOfBalls];

		for(int i = 0; i < balls.length; i++)
		{
			balls[i] = new Ball(100, 100);
		}
		time = millis() + timeToSpawn; 

	}

	void update()
	{

		for(int i = 0; i < balls.length; i++)
		{
			balls[i].update();
			balls[i].CheckEdges();
			balls[i].draw();

			player.haveCollided = player.roundCollision(balls[i].position, balls[i].ballRadius);
			if(player.haveCollided)
			{
				player.health -= 1;	
			}

		}
		if (millis() > time && balls.length < 101)
		{
			ball = new Ball(random(width),random(height));
			balls = (Ball[])append(balls, ball);
			time = millis() + timeToSpawn;
		}
	}
}
