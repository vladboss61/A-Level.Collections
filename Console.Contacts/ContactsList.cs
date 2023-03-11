using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;

namespace Console.Contacts;

public class ContactsList
{
    private const string EnLetter = "en-US";
    private const string UkLetter = "uk-UA";

    private readonly CultureInfo _enCulture = new(EnLetter);
    private readonly CultureInfo _ukCulture = new(UkLetter);

    private readonly SortedDictionary<char, List<Contact>> _englishContactsByLetter = new();
    private readonly SortedDictionary<char, List<Contact>> _ukrainianContactsByLetter = new();

    public void AddContact(Contact contact)
    {
        var firstLetter = char.ToUpper(contact.Name[0]);

        switch (DefineLanguage(firstLetter))
        {
            case Language.English:
                if (!_englishContactsByLetter.ContainsKey(firstLetter))
                {
                    _englishContactsByLetter[firstLetter] = new List<Contact>();
                }

                _englishContactsByLetter[firstLetter].Add(contact);
                break;
            case Language.Ukrainian:
                if (!_ukrainianContactsByLetter.ContainsKey(firstLetter))
                {
                    _ukrainianContactsByLetter[firstLetter] = new List<Contact>();
                }

                _ukrainianContactsByLetter[firstLetter].Add(contact);
                break;
            default:
                throw new ArgumentException("Invalid letter.");
        }
    }

    private Language DefineLanguage(char letter)
    {
        if (!char.IsLetter(letter))
        {
            throw new InvalidOperationException();
        }

        var unicodeChar = char.GetUnicodeCategory(letter);
        if (unicodeChar != UnicodeCategory.UppercaseLetter && unicodeChar != UnicodeCategory.LowercaseLetter)
        {
            throw new InvalidOperationException();
        }

        if (_enCulture.CompareInfo.IndexOf("abcdefghijklmnopqrstuvwxyz", letter.ToString(), CompareOptions.IgnoreCase) >= 0)
        {
            return Language.English;
        }

        if (_ukCulture.CompareInfo.IndexOf("абвгґдеєжзиіїйклмнопрстуфхцчшщьюя", letter.ToString(), CompareOptions.IgnoreCase) >= 0)
        {
            return Language.Ukrainian;
        }

        throw new InvalidOperationException($"Does not supported language for letter: {letter}");
    }

    public IEnumerable<Contact> GetContactsByLetter(char letter, CultureInfo cultureInfo)
    {
        var textInfo = cultureInfo.TextInfo;
        var uppercaseLetter = textInfo.ToUpper(letter.ToString())[0];

        switch (textInfo.CultureName)
        {
            case EnLetter:
                if (_englishContactsByLetter.ContainsKey(uppercaseLetter))
                {
                    return _englishContactsByLetter[uppercaseLetter];
                }
                break;

            case UkLetter:
                if (_ukrainianContactsByLetter.ContainsKey(uppercaseLetter))
                {
                    return _ukrainianContactsByLetter[uppercaseLetter];
                }
                break;

            default:
                return Enumerable.Empty<Contact>();
        }

        return Enumerable.Empty<Contact>();
    }
}