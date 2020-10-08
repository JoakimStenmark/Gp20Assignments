int cellsPerRow = 25;
int totalCells = 0;
Cell[][] cells;
int randomNumber;

float timeBetweenCycles = 250;
float timeCount;





void setup() 
{
	background(0);
	colorMode(HSB);

	size(768,768);
	cells = new Cell[cellsPerRow][cellsPerRow];


	IterateAllCells(Actions.CREATE_CELLS);

	// for (int x = 0; x < cells.length; x++) 
	// {
	// 	for (int y = 0; y < cells[0].length; y++) 
	// 	{
	// 		cells[x][y] = new Cell(x * width/cellsPerRow, y * height/cellsPerRow, cellsPerRow, x, y);
	// 		totalCells++;
	// 	}
	// }

	IterateAllCells(Actions.FIND_NEIGHBORS);


	// for (int x = 0; x < cells.length; x++) 
	// {
	// 	for (int y = 0; y < cells[0].length; y++) 
	// 	{
	// 		cells[x][y].FindNeighbors();
			
	// 	}
	// }
}

void draw() 
{

	BasicDelayEffect();
	//background(0);
	if (millis() > timeCount) 
	{

		IterateAllCells(Actions.CHECK_STATEOFNEIGHBORS);

		// for (int x = 0; x < cells.length; x++) 
		// {
		// 	for (int y = 0; y < cells[0].length; y++) 
		// 	{
		// 		cells[x][y].CheckStateOfNeighbors();
		// 	}
		// }	

		IterateAllCells(Actions.CHANGE_STATE);


		// for (int x = 0; x < cells.length; x++) 
		// {
		// 	for (int y = 0; y < cells[0].length; y++) 
		// 	{
		// 		cells[x][y].ChangeState();
		// 	}
		// }		

		timeCount = millis() + timeBetweenCycles;		
	}

	if (mousePressed) 
	{		
		IterateAllCells(Actions.FORCE_ALIVE);
		// for (int x = 0; x < cells.length; x++) 
		// {
		// 	for (int y = 0; y < cells[0].length; y++) 
		// 	{
		// 		float distance = PVector.dist(new PVector(mouseX, mouseY), cells[x][y].position);
		// 		if (distance > 0 && distance <= 30) 
		// 		{
		// 			cells[x][y].alive = true;
		// 		}	
		// 	}
		// }	

	}


	IterateAllCells(Actions.UPDATE_ALL);
	// for (int x = 0; x < cells.length; x++) 
	// {
	// 	for (int y = 0; y < cells[0].length; y++) 
	// 	{
	// 		cells[x][y].Update();
	// 		cells[x][y].Draw();
	// 	}
	// }

	
	
}
enum Actions
{
	CREATE_CELLS,
	FIND_NEIGHBORS,
	CHECK_STATEOFNEIGHBORS,
	CHANGE_STATE,
	FORCE_ALIVE,
	UPDATE_ALL,
}

void IterateAllCells(Actions action)
{
	for (int x = 0; x < cells.length; x++) 
	{
		for (int y = 0; y < cells[0].length; y++) 
		{
			switch (action) 
			{
				case CREATE_CELLS :

					cells[x][y] = new Cell(x * width/cellsPerRow, y * height/cellsPerRow, cellsPerRow, x, y);
					totalCells++;
					
					break;	

				case FIND_NEIGHBORS :

					cells[x][y].FindNeighbors();
				
					break;	

				case CHECK_STATEOFNEIGHBORS :

					cells[x][y].CheckStateOfNeighbors();
					
					break;	

				case CHANGE_STATE :

					cells[x][y].ChangeState();
					
					break;	

				case FORCE_ALIVE :

					float distance = PVector.dist(new PVector(mouseX, mouseY), cells[x][y].position);
					if (distance > 0 && distance <= 30) 
					{
						cells[x][y].alive = true;
					}	
					
					break;	

				case UPDATE_ALL :

					cells[x][y].Update();
					cells[x][y].Draw();
					
					break;	
			}
		}
	}
}

void BasicDelayEffect()
{
	fill(0, 0, 0, 32);
	rect(0, 0, width, height);
	fill(0, 13, 86);
}

void MakeAlive()
{

}
