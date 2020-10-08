int cellsPerRow = 25;
int totalCells = 0;
Cell[][] cells;
int randomNumber;

float mouseTouchSize = 1.0f;

float timeBetweenCycles;
float timeCount;

enum Actions
{
	CREATE_CELLS,
	FIND_NEIGHBORS,
	CHECK_STATEOFNEIGHBORS,
	CHANGE_STATE,
	FORCE_ALIVE,
	UPDATE_ALL,
}

void setup() 
{
	background(0);
	colorMode(HSB);

	size(768,768);
	cells = new Cell[cellsPerRow][cellsPerRow];


	IterateAllCells(Actions.CREATE_CELLS);

	IterateAllCells(Actions.FIND_NEIGHBORS);
}

void draw() 
{

	if (pause)
	{
		return;
	}
  
  	timeBetweenCycles = 184;

	BasicDelayEffect();
	
	if (millis() > timeCount) 
	{

		IterateAllCells(Actions.CHECK_STATEOFNEIGHBORS);

		IterateAllCells(Actions.CHANGE_STATE);

		timeCount = millis() + timeBetweenCycles;		
	}

	if (mousePressed) 
	{		
		IterateAllCells(Actions.FORCE_ALIVE);

	}
	
	IterateAllCells(Actions.UPDATE_ALL);
	
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
					PVector mousePosition = new PVector(mouseX - cells[x][y].size * 0.5, mouseY - cells[x][y].size * 0.5);
					float distance = mousePosition.dist(cells[x][y].position);
					if (distance <= mouseTouchSize * cells[x][y].size * 0.5) 
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
	fill(0, 0, 59, 25);
	rect(0, 0, width, height);
	fill(134, 232, 255);
}
