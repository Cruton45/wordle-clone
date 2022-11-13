using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RestSharp;

public class apihelper
{
    public static string BaseUrl = "https://api.dictionaryapi.dev/api/v2/entries/en/";
    public static RestClient client;

    static apihelper()
    {
        client = new RestClient(BaseUrl);

        var request = new RestRequest();

        var responce = client.Get(request).Content;

        Console.WriteLine(responce);
    }

    public static bool IsValidWord(string word)
    {
        var request = new RestRequest("https://api.dictionaryapi.dev/api/v2/entries/en/" + word);

        var responce = client.Get(request).Content[3];

        if(responce == 'w')
            return true;
        else
            return false;
    }
}