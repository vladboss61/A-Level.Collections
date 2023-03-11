using System.Linq;
using System.Threading;

namespace Console.Contacts;

using System.Collections.Generic;
using System.Text;
using System.Globalization;
using System;

public enum Language { English, Ukrainian }

internal class Program
{
    public static void Main(string[] args)
    {
        var allCultures = CultureInfo.GetCultures(CultureTypes.AllCultures);
        var exampleCulture = new CultureInfo("en-US");

        Thread.CurrentThread.CurrentCulture = exampleCulture;
        Thread.CurrentThread.CurrentUICulture = exampleCulture;

        var exampleList = new [] { 1, 2, 3, 4, 5 }.MySelect(x => x * 2).MyWhere(y => y > 4).ToList();
        var example2List = new [] { 1, 2, 3, 4, 5 }.Select(x => x * 2).Where(y => y > 4).ToList();

        ExampleOfCount("hello text for example and display....");

        //ExampleStackReverseString("Text 1234");

        //ExampleOfQueue();

        var contacts = new ContactsList();

        contacts.AddContact(new Contact { Name = "John", Phone = "555-1234" });
        contacts.AddContact(new Contact { Name = "Jane", Phone = "555-5678" });
        contacts.AddContact(new Contact { Name = "Bob", Phone = "555-9012" });
        contacts.AddContact(new Contact { Name = "Андрій", Phone = "555-7890" });
        contacts.AddContact(new Contact { Name = "Андрій", Phone = "555-7890" });
        contacts.AddContact(new Contact { Name = "Антон", Phone = "555-7890" });
        contacts.AddContact(new Contact { Name = "Марія", Phone = "555-2345" });

        var en = new CultureInfo("en-US");
        var uk = new CultureInfo("uk-UA");

        var foundEnContacts = contacts.GetContactsByLetter('J', en);

        foreach (var item in foundEnContacts)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine("==========");
        var notFoundUkContacts = contacts.GetContactsByLetter('А', en);

        foreach (var item in notFoundUkContacts)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine("==========");

        var foundAUkContacts = contacts.GetContactsByLetter('А', uk);

        foreach (var item in foundAUkContacts)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine("==========");
        var foundMUkContacts = contacts.GetContactsByLetter('М', uk);

        foreach (var item in foundMUkContacts)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine("==========");

        var notFoundMUkContacts = contacts.GetContactsByLetter('М', en);
        foreach (var item in notFoundMUkContacts)
        {
            Console.WriteLine(item);
        }
        Console.WriteLine("==========");

        Console.WriteLine("Hello, World!");
    }

    public static void ExampleOfCount(string text)
    {
        var map = new Dictionary<char, int>();

        foreach (var symbol in text)
        {
            if (map.ContainsKey(symbol))
            {
                map[symbol] += 1;
                continue;
            }
            map.Add(symbol, 1);
        }

        foreach (var keyValue in map)
        {
            Console.WriteLine($"Symbol: {keyValue.Key} Count: {keyValue.Value}");
        }
    }

    /// <summary>
    ///     LIFO. (Last In First Out).
    /// </summary>
    /// <param name="text">Text</param>
    public static void ExampleStackReverseString(string text)
    {
        var charStack = new Stack<char>();

        foreach (char c in text)
        {
            charStack.Push(c);
        }

        var builder = new StringBuilder();

        while (charStack.Count > 0)
        {
            builder.Append(charStack.Pop());
        }

        Console.WriteLine(builder.ToString());
    }


    /// <summary>
    ///     FIFO. (First In First Out).
    /// </summary>
    public static void ExampleOfQueue()
    {
        var messageQueue = new Queue<string>();

        messageQueue.Enqueue("Message 1");
        messageQueue.Enqueue("Message 2");
        messageQueue.Enqueue("Message 3");

        while (messageQueue.Count > 0)
        {
            string message = messageQueue.Dequeue();
            Console.WriteLine($"Processing message: {message}");
        }

        Console.WriteLine("All messages processed.");
    }
}
