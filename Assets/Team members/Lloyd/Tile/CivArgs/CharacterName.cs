using System;
using System.Collections.Generic;

public class RandomNameGenerator
{
    //CHAT GPT WUZ HERE
    private readonly List<string> names;
    private readonly List<string> celebNames;
    private readonly List<string> zombeeNames;
    
    public RandomNameGenerator()
    {
        names = new List<string> { "Cam", "John", "Lloyd", "Marcus", "Oscar", "Virginia" };
        celebNames = new List<string> { "Arnold Schwarzenegger", "Christopher Walken", "Joan of Arc", "Mike Tyson", "Muhammed Ali", "Solid Snake", "Borat", "Buzz Aldrin" };
        zombeeNames = new List<string> { "Buzz Lightyear", "Beeyoncé", "Bee-thoven", "Honey Boo-boo", "Bee-tlejuice", "Barry Gibb", "Robin Gibb" };
    }

    public string GetRandomName(CivEventArgs.Personality type)
    {
        Random random = new Random();

        if (type == CivEventArgs.Personality.Celebrity)
            return celebNames[random.Next(celebNames.Count)];
        
        return names[random.Next(names.Count)];
    }
}