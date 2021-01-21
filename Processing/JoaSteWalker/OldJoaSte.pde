class JoaSteOld implements WalkerInterface {

	//Add your own variables here.
	//Do not use processing variables like width or height

	PVector currentPosition;
	int windowWidth = 0;
	int windowHeight = 0;

	PVector[] directions;
	boolean hitWall = false;

	public JoaSteOld()
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
		//float x = (int) random(0, playAreaWidth);
		//float y = (int) random(0, playAreaHeight);
		float x = (int) playAreaWidth/2;
		float y = (int) playAreaHeight/2;


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
	
	int[] movementPlan = {8, 1, 3, 30, 30, 1, 7, 0};
	int currentDirection = 0;
	int currentPlan = 0;
	int step = 0;

	PVector update()
	{

		//add your own walk behavior for your walker here.
		//Make sure to only use the outputs listed below.
		if (hitWall) 
		{
			movementPlan = shiftArray(movementPlan);
			// for (int i = 0; i < movementPlan.length; i++) 
			// {
			// 	println("new plan: "+ movementPlan[i]);
			// }

			hitWall = false;
		}

		if (checkOutOfBounds()) 
		{
			return new PVector(0, 0);
		}

		resetDirections();
		preventOutBoundsMovement();
		

		while (step >= movementPlan[currentPlan]) 
		{
			currentPlan++;
			currentPlan = currentPlan % movementPlan.length;
			currentDirection++;
			currentDirection = currentDirection % directions.length;
			step = 0;
		}
		println("movementPlan[currentPlan]: "+movementPlan[currentPlan]);
		step++;

		currentPosition.add(directions[currentDirection]);
		currentPosition = new PVector((int)currentPosition.x, (int)currentPosition.y);		
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
			hitWall = true;
			println("Left");
		}

		if (currentPosition.y + 1 > windowHeight) 
		{
			directions[1].mult(-1);
			hitWall = true;
			println("Down");
		}

		if (currentPosition.x + 1 > windowWidth) 
		{
			directions[2].mult(-1);
			hitWall = true;
			println("right");			
		}

		if (currentPosition.y - 1 < 0) 
		{
			directions[3].mult(-1);
			hitWall = true;
			println("Up");
		}

	}

	int[] shiftArray(int[] arrayToShift)
	{
		int i = 0;
		int temp = arrayToShift[0];
		for (i = 0; i < movementPlan.length - 1; i++) 
		{
			arrayToShift[i] = arrayToShift[i+1];
		}
		arrayToShift[i] = temp;

		return arrayToShift;
	}

}




