//Hi There!
using System;
using System.Collections.Generic;
using System.Collections;
//Dont forget to import the linq namespace first so you can actually use the extension methods!
using System.Linq;


// #1 Zip()

//suppose we have 2 collections

int[] numberArray = new int[] { 1, 2, 3, 4 };

string[] stringArray = new string[] { "first", "second", "third", "fourth" };

// now what if we wanted to put each elements of the 2 arrays into a grouping next to each other?
// something like: (1,"first") , (2,"second), (3,"third) ?
// We can acheive that using the new Linq Zip() extention method!
// the method returns a tuple of the joined values.

IEnumerable<(int number, string name)> zipedArray = numberArray.Zip(stringArray);

var valuesArray = zipedArray.ToArray();

//You can even deconstruct the joined values in a foreach loop!
foreach (var (number, name) in zipedArray)
{
    Console.WriteLine($"({number},{name})");
}

// Bonus! you can even zip 3 collections together if you would like!

double[] percents = new double[] { 0.1D, 0.2D, 0.3D, 0.4D };

var tripleZippedArray = numberArray.Zip(stringArray, percents).ToArray();

Console.WriteLine("Bonus:");
foreach (var (number, name, percent) in tripleZippedArray)
{
    Console.WriteLine($"({number},{name},{percent})");
}

Console.ReadLine();


// #2 Chunk()

// suppose we have a collection

IEnumerable<int> veryBigList = Enumerable.Range(0, 21420);

//As you might notice, this collection is very large and one might say they would prefer
//to split it up into smaller pieces and perform multiple batch operations on it
//for example a bulk database call that only accepts smaller ranges of data
//we can split up this collection with the new Linq Chunk() Mehtod!

IEnumerable<int[]> smallerLists = veryBigList.Chunk(500);

//As you can see, the smaller collections are gathered in new arrays NOT enumerables!

//NOTICE! for simplicity sake i will override our example with a smaller array so it is easier to read the output
// Feel free to delete the next 2 lines in your own testing envoironment ^^
veryBigList = Enumerable.Range(0, 32);
smallerLists = veryBigList.Chunk(8);
//

foreach (var smallerList in smallerLists)
{
    int lenght = smallerList.Length;
    for (int index = 0; index < lenght; index++)
    {
        Console.Write($"{smallerList[index]}-");
    }
    Console.WriteLine("\nJust passed a chunk!\n");
}

Console.ReadLine();

// #3 new *By methods improvments!

// The Distinct() extension Method now has a DistinctBy() version which allows you to specify a subset of a value
// to use during comparison!

var items = new[]
{
    new { Name = "James", Age = 18 },
    new { Name = "John", Age = 18 },
    new { Name = "John", Age = 30 },
};

var distinctByNameItems = items.DistinctBy(d => d.Name).ToArray();
var distinctByAgeItems = items.DistinctBy(d => d.Age).ToArray();

Console.WriteLine("Distinct by Name:");
foreach(var item in distinctByNameItems)
{
    Console.WriteLine($"Name: {item.Name} ,Age: {item.Age} ");
}
Console.WriteLine("\nDistinct by Age:");
foreach (var item in distinctByAgeItems)
{
    Console.WriteLine($"Name: {item.Name} ,Age: {item.Age} ");
}

Console.ReadLine();

// The ExceptBy() Method is also new, you can compare and remove items from a collection based on
// keys provided from another collection.

var androids = new[]
{
    new { Name = "2B", Age = 18 },
    new { Name = "9S", Age = 18 },
    new { Name = "A2", Age = 30 },
    new { Name = "8B", Age = 25 },
};

var traitorNames = new string[]
{
    "A2",
    "8B",
};

//This will remove all items with the same name as the provided names
var exceptTraitors = androids.ExceptBy(traitorNames,s => s.Name); 

Console.WriteLine("The androids whose name were in traitors were removed:");
foreach(var android in exceptTraitors)
{
    Console.WriteLine($"Name: {android.Name} , Age: {android.Age} ");
}
Console.ReadLine();

// We also have the new IntersectBy() method!
// 

var dangerousPeople = new[]
{
    new { Name = "My Neighbour", DangerLevel = 1000 },
    new { Name = "Local Cat", DangerLevel = 1800 },
    new { Name = "Angry Customer", DangerLevel = 900 },
};

var oldDangerousPeopleNames = new string[]
{
    "Grandma",
    "Local Cat",
};

// Only the items that have their name in the string array will remain
var reoccuringDangerousPeople = dangerousPeople.IntersectBy(oldDangerousPeopleNames, s => s.Name);

Console.WriteLine("Only the items whose name value was in the second array will remain:");
foreach(var item in reoccuringDangerousPeople)
{
    Console.WriteLine($"Name: {item.Name}, DangerLevel: {item.DangerLevel}");
}

Console.ReadLine();

// We also have the new UnionBy() Mehtod! it will join the elements of 2 collections and remove the 
// duplicates based on the key you provide!

var leaders = new[]
{
    new { Name = "Donald", Country = "Usa" },
    new { Name = "Vladimir", Country = "Russia" },
    new { Name = "Mamooti", Country = "Iran" },
};

var otherLeaders = new[]
{
    new { Name = "Elizabeth", Country = "United Kingdom" },
    new { Name = "Stalin", Country = "Russia" },
    new { Name = "Angela", Country = "Germany" },
    new { Name = "Ebi", Country = "Iran" },
};

// All items in otherLeaders will be added except the ones with a repeated country name
var allLeaders = leaders.UnionBy(otherLeaders, s => s.Country);

Console.WriteLine("The leaders with repeated Country names were not added:");
foreach(var item in allLeaders)
{
    Console.WriteLine($"Name: {item.Name}, Country: {item.Country}");
}

Console.ReadLine();

// #4 improvments for the *OrDefault() Methods!

//Remember the methods that looked for a specific value and returned that value OR the default() of that type?

var fruits = new[]
{
    new { Name = "Banana", Price = 300 },
    new { Name = "Watermelon", Price = 250 },
    new { Name = "Apple", Price = 50 },
    new { Name = "Avacado", Price = 600 },
};

//this will return the first item that has a price lower than 60 : Apple
var cheepFruit = fruits.FirstOrDefault(c => c.Price > 60); 

//However it will return null if it fails to find the requested item
// what if i wanted it to return a specific item in case it fails to find what i want?
// we can now specify the default value that will be returned when a search fails!

var defaultFruit = new { Name = "UnKnown", Price = 0 };

var cheepOrDefaultFruit = fruits.FirstOrDefault(c => c.Price < 30, defaultFruit);
//as simple as that, the default was overwritten

Console.WriteLine($"the fruit i found was: Name: {cheepOrDefaultFruit.Name}, Price: {cheepOrDefaultFruit.Price}");

Console.ReadLine();



