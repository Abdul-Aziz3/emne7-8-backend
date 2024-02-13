using ExtensionMethodTester;

int x = 10;
int testValue = 20;

// lage en boolean variabel -> x > tesValue
// er x større enn 20?
bool isGreaterThan20 = x > testValue;

bool isGreater = x.IsGreaterThan(testValue);


int myNumber = 12345;
int sumNum = myNumber.sumNum();
Console.WriteLine(sumNum);