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
        names = new List<string> { "Cam", "John", "Lloyd", "Marcus", "Oscar", "Virginia", "Arnold Schwarzenegger", "Christopher Walken", "Buzz Lightyear", "Beeyoncé" };
        celebNames = new List<string> { "Arnold Schwarzenegger", "Christopher Walken", "Joan of Arc", "Mike Tyson", "Muhammed Ali", "Solid Snake" };
        zombeeNames = new List<string> { "Buzz Lightyear", "Beeyoncé" };
    }

    public string GetRandomName(CivEventArgs.Personality type)
    {
        Random random = new Random();

        if (type == CivEventArgs.Personality.Celebrity)
            return celebNames[random.Next(names.Count)];
        
        return names[random.Next(names.Count)];
    }
}