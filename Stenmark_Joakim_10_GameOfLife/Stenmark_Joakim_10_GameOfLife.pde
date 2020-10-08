int cellsPerRow = 100;
int totalCells = 0;
Cell[][] cells;
int randomNumber;

float timeBetweenCycles = 100;
float timeCount;
void setup() 
{

	size(768,768);
	cells = new Cell[cellsPerRow][cellsPerRow];

	for (int x = 0; x < cellsPerRow; x++) 
	{
		for (int y = 0; y < cellsPerRow; y++) 
		{
			cells[x][y] = new Cell(x * width/cellsPerRow, y * height/cellsPerRow, cellsPerRow, x, y);
			totalCells++;
		}
	}

	for (int x = 0; x < cellsPerRow; x++) 
	{
		for (int y = 0; y < cellsPerRow; y++) 
		{
			cells[x][y].FindNeighbors();
			
		}
	}
}

void draw() 
{

	background(0);

	
	if (millis() > timeCount) 
	{


		for (int x = 0; x < cellsPerRow; x++) 
		{
			for (int y = 0; y < cellsPerRow; y++) 
			{
				cells[x][y].CheckStateOfNeighbors();
			}
		}	

		for (int x = 0; x < cellsPerRow; x++) 
		{
			for (int y = 0; y < cellsPerRow; y++) 
			{
				cells[x][y].ChangeState();
			}
		}		

		timeCount = millis() + timeBetweenCycles;		
	}

	for (int x = 0; x < cellsPerRow; x++) 
	{
		for (int y = 0; y < cellsPerRow; y++) 
		{
			cells[x][y].Draw();

		}
	}

}
