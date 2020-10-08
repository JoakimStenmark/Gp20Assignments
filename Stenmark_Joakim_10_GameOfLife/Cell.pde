class Cell
{
	PVector position;
	PVector arrayPosition;
	color livingColor  = color(0, 128, 0);
	color deadColor = color(0, 0, 0);
	color currentColor;
	float size;
	boolean alive = false;

	Cell[] neighbors;

	int liveNeighbors = 0;
	Cell(float x, float y, int totalCells, int arrayX, int arrayY)
	{
		arrayPosition = new PVector(arrayX, arrayY);
		neighbors = new Cell[0];
		size = width/totalCells;
		position = new PVector(x,y);
		if ((int)random(0, 5) == 0) 
		{
			alive = true;
		}
		UpdateColor();
	}

	void Draw()
	{
		UpdateColor();
		stroke(255, 63);
		fill(currentColor);
		rect(position.x, position.y, size, size);
	}

	void UpdateColor()
	{
		if (alive) 
		{
			currentColor = livingColor;	
		}
		else 
		{
			currentColor = deadColor;	
		}
	}

	void FindNeighbors()
	{
		ArrayList<Cell> neighborsToAdd = new ArrayList<Cell>();

		for (int x = 0; x < cellsPerRow; x++) 
		{
			for (int y = 0; y < cellsPerRow; y++) 
			{
				float distanceBetweenCells = PVector.dist(arrayPosition, cells[x][y].arrayPosition);
				if (distanceBetweenCells > 0 && distanceBetweenCells <= 1.5) 
				{
					neighborsToAdd.add(cells[x][y]);
				}			
			}
		}

		neighbors = neighborsToAdd.toArray(neighbors); // fråga hur denna funkar
	}

	// void TurnNeighborsRed()
	// {
	// 	for (Cell c : neighbors) 
	// 	{
	// 		c.currentColor = color(128, 0, 0);
	// 	}
	// }


	void CheckStateOfNeighbors()
	{
		liveNeighbors = 0;
		for (int i = 0; i < neighbors.length; ++i) 
		{
			if (neighbors[i].alive) 
			{
				liveNeighbors++;	
			}
		}
	}

	void ChangeState()
	{

		if (alive) 
		{
			if (liveNeighbors < 2 || liveNeighbors > 3) 
			{
				alive = false;
			}
		}
		else 
		{
			if (liveNeighbors == 3) 
			{
				alive = true;	
			}	
		}
	}



}	
