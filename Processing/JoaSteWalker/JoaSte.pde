class JoaSte implements WalkerInterface {

	//Add your own variables here.
	//Do not use processing variables like width or height

	PVector currentPosition;
	int windowWidth = 0;
	int windowHeight = 0;

	PVector[] directions;
	int[] movementPlan = {5, 1, 7, 1};
	int currentDirection = 0;
	int steps = 0;
	public JoaSte()
	{
		currentPosition = new PVector();
		resetDirections();
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
				return new PVector(1, 0);
			case 2:
				return new PVector(0, 1);
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
		//float x = (int) 0;
		float y = (int) random(0, playAreaHeight);

		windowWidth = playAreaWidth;
		windowHeight = playAreaHeight;

		currentPosition = new PVector(x, y);
		//a PVector holds floats but make sure its whole numbers that are returned!
		return new PVector(x, y);
	}

//All valid outputs:
// PVector(-1, 0);
// PVector(1, 0);
// PVector(0, 1);
// PVector(0, -1);

//Any other outputs will kill the walker!
	
	PVector update()
	{

		//add your own walk behavior for your walker here.
		//Make sure to only use the outputs listed below.
	
		if (checkOutOfBounds()) 
		{
			return new PVector(0, 0);
		}

		resetDirections();
		preventOutBoundsMovement();
		
		
		




		currentPosition.add(directions[currentDirection]);		
		return directions[currentDirection];
		//return getDirection(number);
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
			println("Left");
		}

		if (currentPosition.x + 1 > windowWidth) 
		{
			directions[1].mult(-1);
			println("right");
			
		}

		if (currentPosition.y + 1 > windowHeight) 
		{
			directions[2].mult(-1);
			println("Down");

		}

		if (currentPosition.y - 1 < 0) 
		{
			directions[3].mult(-1);
			println("Up");

		}
	}

}




