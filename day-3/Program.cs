using System.IO;
using System.IO.Compression;
using System.Xml.Serialization;

char[,] engineArray = new char[140, 140];

using var fs = new FileStream(@"D:\git\advent-of-code\day-3\input.txt", FileMode.Open);
using var sr = new StreamReader(fs);

var symbolDictionary = new HashSet<char>(){
    '-', '@', '*', '=', '%', '/', '$', '#', '+', '&'
};

int partNumberSum = 0;

for(int i = 0; i < engineArray.GetLength(1); i++){
    var schematicRow = sr.ReadLine();
    if (schematicRow is null){
        throw new NullReferenceException("schematicRow");
    }
    for(int j = 0; j < engineArray.GetLength(0); j++){
        engineArray[j, i] = schematicRow[j];
    }
}

for(int y = 0; y < engineArray.GetLength(1); y++){
    for(int x = 0; x < engineArray.GetLength(0); x++){
        if (symbolDictionary.Contains(engineArray[x, y])){
            //[x-1,y-1],[x,y-1],[x+1,y-1]
            //[x-1,  y]         [x+1,  y]
            //[x-1,y+1],[x,y+1],[x+1,y+1]

            //TOP
            if(IsCoordInBounds(x-1,y-1)){
                AddIfNumberAndSetItToX(x-1, y-1);
            }
            if(IsCoordInBounds(x,y-1)){
                AddIfNumberAndSetItToX(x, y-1);
            }
            if(IsCoordInBounds(x+1,y-1)){
                AddIfNumberAndSetItToX(x+1, y-1);
            }
            //MID
            if(IsCoordInBounds(x-1,y)){
                AddIfNumberAndSetItToX(x-1, y);
            }
            if(IsCoordInBounds(x+1,y)){
                AddIfNumberAndSetItToX(x+1, y);
            }
            //BOTTOM
            if(IsCoordInBounds(x-1,y+1)){
                AddIfNumberAndSetItToX(x-1, y+1);
            }
            if(IsCoordInBounds(x,y+1)){
                AddIfNumberAndSetItToX(x, y+1);
            }
            if(IsCoordInBounds(x+1,y+1)){
                AddIfNumberAndSetItToX(x+1, y+1);
            }
        }
    }
}



for(int y = 0; y < engineArray.GetLength(1); y++){
    for(int x = 0; x < engineArray.GetLength(0); x++){
        Console.Write(engineArray[x,y]);
    }
    Console.Write("\n");
}

System.Console.WriteLine($"Part sum: {partNumberSum}");

bool IsCoordInBounds(int x, int y){
    int xBound = engineArray.GetLength(0);
    int yBound = engineArray.GetLength(1);
    return x < xBound && x >= 0 && y < yBound && y >= 0;
}
void AddIfNumberAndSetItToX(int x, int y){
    if(engineArray[x, y] == 'X' || engineArray[x, y] == '.' || symbolDictionary.Contains(engineArray[x, y])){
        return;
    }
    int leftMost = x;
    int rightMost = x;
    GetLeftMostDigitIndex(x, y, ref leftMost);
    GetRightMostDigitIndex(x, y, ref rightMost);

    if (leftMost == rightMost){
        partNumberSum += Convert.ToInt32($"{engineArray[x, y]}");
        engineArray[x, y] = 'X';
        return;
    }

    var tempNum = "";
    for (int i = leftMost; i <= rightMost; i++){
        tempNum += engineArray[i, y];
        engineArray[i, y] = 'X';
    }
    partNumberSum += Convert.ToInt32(tempNum);


}

void GetLeftMostDigitIndex(int x, int y, ref int leftMost){
    if (char.IsDigit(engineArray[x, y])){
        leftMost = x;
        if(IsCoordInBounds(x-1,y)){
            GetLeftMostDigitIndex(x-1, y, ref leftMost);
        }
    }
}
void GetRightMostDigitIndex(int x, int y, ref int rightMost){
    if (char.IsDigit(engineArray[x, y])){
        rightMost = x;
        if(IsCoordInBounds(x+1,y)){
            GetRightMostDigitIndex(x+1, y, ref rightMost);
        }
    }
}






