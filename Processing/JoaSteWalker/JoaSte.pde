class JoaSte implements WalkerInterface {

	//Add your own variables here.
	//Do not use processing variables like width or height

	PVector currentPosition;
	int windowWidth = 0;
	int windowHeight = 0;

	PVector[] directions;
	int currentDirection = 0;

	int[] weights = { 22, 24, 26, 28 };
	int[][] weightedDie ={ {10, 25, 60, 100},{0,1,2,3} };
	
	boolean hitWall = false;

	public JoaSte()
	{
		currentPosition = new PVector();
		resetDirections();
		int cumulative = 0;
		for (int i = 0; i < weights.length; i++) 
		{
			cumulative += weights[i];
			constrain(cumulative, 0, 100);
			weightedDie[0][i] = cumulative; 
		}
		
	}

	void resetDirections()
	{
		directions = new PVector[4];

		for (int i = 0; i < directions.length; i++) 
		{
			directions[i] = getDirection(i);	
		}

	}

	PVector getDirection(int choise)
	{
		switch(choise) 
		{
			case 0:
				return new PVector(-1, 0);
			case 1:
				return new PVector(0, 1);				
			case 2:
				return new PVector(1, 0);
			default:
				return new PVector(0, -1);
		}
	}

	String getName()
	{
		return "JoaSte"; //When asked, tell them our walkers name
	}

	PVector getStartPosition(int playAreaWidth, int playAreaHeight)
	{
		//Select a starting position or use a random one.
		float x = (int) random(0, playAreaWidth);
		float y = (int) random(0, playAreaHeight);



		windowWidth = playAreaWidth;
		windowHeight = playAreaHeight;

		currentPosition = new PVector(x, y);
		//a PVector holds floats but make sure its whole numbers that are returned!
		return new PVector(x, y);
	}

//All valid outputs:
// PVector(-1, 0);
// PVector(0, 1);
// PVector(1, 0);
// PVector(0, -1);

//Any other outputs will kill the walker!
	
	PVector update()
	{

		//add your own walk behavior for your walker here.
		//Make sure to only use the outputs listed below.
		if (hitWall) 
		{
			weightedDie[1] = shiftDirectionalProbability(weightedDie[1]);
			hitWall = false;
		}

		if (checkOutOfBounds()) 
		{
			return new PVector(0, 0);
		}

		resetDirections();
		preventOutBoundsMovement();

		currentDirection = getNewDirection();

		currentPosition.add(directions[currentDirection]);
		currentPosition = new PVector((int) currentPosition.x, (int) currentPosition.y);		
		return directions[currentDirection];

	}

	boolean checkOutOfBounds()
	{

		currentPosition = new PVector((int) currentPosition.x, (int) currentPosition.y);

		if (currentPosition.x < 0 || currentPosition.x > windowWidth) 
		{
			println(currentPosition +  " is OOB");
			return true;
		}
		else if (currentPosition.y < 0 || currentPosition.y > windowHeight)
		{
			println(currentPosition +  " is OOB");
			return true;
		}

		return false;
	}

	void preventOutBoundsMovement()
	{

		if (currentPosition.x - 1 < 0) 
		{
			directions[0].mult(-1);
			hitWall = true;

		}

		if (currentPosition.y + 1 > windowHeight) 
		{
			directions[1].mult(-1);
			hitWall = true;

		}

		if (currentPosition.x + 1 > windowWidth) 
		{
			directions[2].mult(-1);
			hitWall = true;
		
		}

		if (currentPosition.y - 1 < 0) 
		{
			directions[3].mult(-1);
			hitWall = true;

		}

	}

	int getNewDirection()
	{
		int diceRoll = (int)random(0, 100);
		int cumulative = 0;
		for (int i = 0; i < weightedDie[0].length; i++) 
		{
			cumulative += weightedDie[0][i];

			if (cumulative > diceRoll) 
			{
				return weightedDie[1][i];
			}
		}
		return 0;
	}

	int[] shiftDirectionalProbability(int[] arrayToShift)
	{
		int i = 0;
		int temp = arrayToShift[0];
		for (i = 0; i < arrayToShift.length - 1; i++) 
		{
			arrayToShift[i] = arrayToShift[i+1];
		}
		arrayToShift[i] = temp;

		return arrayToShift;
	}

}




