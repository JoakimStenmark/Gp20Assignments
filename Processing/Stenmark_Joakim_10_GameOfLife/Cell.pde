class Cell
{
	PVector position;
	PVector arrayPosition;
	color livingColor  = color(174, 177, 220);
	color deadColor = color(0, 30, 75, 16);
	color currentColor;
	float size;

	boolean alive = false;
	float timeAlive = 0;
	float timeCurrent = 0;

	Cell[] neighbors;

	float pulse;
	int liveNeighbors = 0;

	boolean havePlayedSound = false;
	SoundFile blip;

	Cell(float x, float y, int totalCells, int arrayX, int arrayY, PApplet pApplet)
	{
		arrayPosition = new PVector(arrayX, arrayY);
		neighbors = new Cell[0];
		size = width/totalCells;
		position = new PVector(x,y);
		// if ((int)random(0, 5) == 0) 
		// {
		// 	alive = true;		
		// }

		blip = new SoundFile(pApplet, "Blip.wav");
		blip.amp(arrayX * 0.05 + 0.3);
		blip.rate(arrayY * 0.1 + 0.5);
		
		UpdateColor();

	}

	void Draw()
	{

		UpdateColor();
		stroke(40, 63);
		strokeWeight(1);
		fill(currentColor, 50);
		rect(position.x, position.y, size, size, 10);
		if (alive) 
		{
			DrawPulse();				
		}
	}

	void Update()
	{
		if (alive) 
		{
			timeAlive += millis() - timeCurrent;
			if (!havePlayedSound) 
			{
				if (blip.isPlaying()) 
				{
					blip.stop();	
				}
				blip.play();	
				havePlayedSound = true;
			}
		}
		else 
		{
			timeAlive = 0;	
		}
		timeCurrent = millis();
	}

	void DrawPulse()
	{
		noFill();
		stroke(livingColor + color(0, 0, 25), 255 - timeAlive * 0.3);
		if (pulse < size * 0.85) 
		{
			pulse += size * 0.07;
		}
		else
			pulse = 0;

		ellipse(position.x + size * 0.5, position.y + size * 0.5, pulse, pulse);	
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

		for (int x = 0; x < cells.length; x++) 
		{
			for (int y = 0; y < cells[0].length; y++) 
			{
				float distanceBetweenCells = PVector.dist(arrayPosition, cells[x][y].arrayPosition);
				if (distanceBetweenCells > 0 && distanceBetweenCells <= 1.5) 
				{
					neighborsToAdd.add(cells[x][y]);
				}			
			}
		}

		neighbors = neighborsToAdd.toArray(neighbors);
	}

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
				havePlayedSound = false;
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
