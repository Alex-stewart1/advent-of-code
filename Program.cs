// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");

(string, char)[] numArray = [("one", '1'), ("two", '2'), ("three", '3'), ("four", '4'), ("five", '5'), ("six", '6'), ("seven", '7'), ("eight", '8'), ("nine", '9')];


var inputArray = File.ReadAllLines(@"D:\git\advent-of-code\input.txt");

int sum = 0;

foreach (var input in inputArray)
{
    //Pe-proccess
    var preProccessed = ConvertTextToDigits(input);

    Console.WriteLine($"Input: {input}");
    Console.WriteLine($"PreProccessed: {preProccessed}");

    char leftDigit = 'x';
    char rightDigit = 'x';

    for (int i = 0; i < preProccessed.Length; i++)
    {
        var val = preProccessed[i];

        if (char.IsDigit(val)){
            if(leftDigit == 'x'){
                leftDigit = val;
            }
            rightDigit = val;
        }
    }

    Console.WriteLine($"{leftDigit}{rightDigit}");
    sum += Convert.ToInt32($"{leftDigit}{rightDigit}");


}

Console.WriteLine(sum);

string ConvertTextToDigits(string input){

    char[] mapChars = new char[input.Length];
    for (int i = 0; i < input.Length; i++){
        mapChars[i] = '-';
    }

    

    foreach (var digitMap in numArray){
        var digitName = digitMap.Item1;
        var digitValue = digitMap.Item2;

        int startPos = 0;
        int currentPos = 0;

        for (int i = 0; i < input.Length; i++){
            bool reset = false;

            if (char.IsDigit(input[i])){
                reset = true;
                mapChars[i] = input[i];
            }
            if (!reset){
                if (digitName[currentPos] == input[i]){
                    currentPos++;
                }else{
                    if(digitName[0] == input[i]){
                        startPos = i;
                        currentPos = 1;
                    }else{
                        reset = true;
                    }
                }
            }
            
            if (currentPos == digitName.Length){
                mapChars[startPos] = digitValue;
                reset = true;
            }

            if(reset){
                currentPos = 0;
                startPos = i + 1;
            }
        }
    }

    return new string(mapChars);
}


