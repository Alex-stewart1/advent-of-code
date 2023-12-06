using System.ComponentModel;
using Microsoft.VisualBasic;
using Newtonsoft.Json;
// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

var gamesArray = File.ReadAllLines(@"D:\git\advent-of-code\day-2\input.txt");




int powerSum = 0;

foreach (var gameString in gamesArray){

    var idSplit = gameString.Split(':')[0][5..];
    var id = Convert.ToInt32(idSplit);

    var subGames = gameString.Split(':')[1].Split(';');

    var isStillValid = true;

    int minGreen = 0;
    int minRed = 0;
    int minBlue = 0;

    foreach (var subGame in subGames){
        // 20 green, 3 red, 2 blue

        var individualResults = subGame.Split(','); //[ 20 green][ 12 red] etc

        var _game = new Game{};

        foreach (var result in individualResults){
            var resultSplit = result[1..].Split(' ');
            switch(resultSplit[1]){//20 green
                case "green":
                    _game.Green = Convert.ToInt32(resultSplit[0]);
                    break;
                case "red":
                    _game.Red = Convert.ToInt32(resultSplit[0]);
                    break;
                case "blue":
                    _game.Blue = Convert.ToInt32(resultSplit[0]);
                    break;
                    default:
                    throw new InvalidOperationException("Invalid parse");
            }
        }


        if (_game.Green > minGreen){
            minGreen = _game.Green;
        }

        if (_game.Red > minRed){
            minRed = _game.Red;
        }

        if (_game.Blue > minBlue){
            minBlue = _game.Blue;
        }


        //if(!IsPossibleSubGame(_game)){
          //  isStillValid = false;
        //}
    }


    var power = minBlue * minRed * minGreen;

    if (power == 0) throw new InvalidDataException("0");


    powerSum += power;



}

System.Console.WriteLine(powerSum);

static bool IsPossibleSubGame(Game game){
    return game.Red <= 12 && game.Green <= 13 && game.Blue <= 14;
}






struct Game{
    public int Green {get; set;}
    public int Red {get; set;}
    public int Blue {get; set;}
}