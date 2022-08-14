using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using UnityEngine;

public class PlayerScore
{
    public string Nome { get; set; }
    public int Ponto { get; set; }
}

public class PlayerScoreClient
{
    // Members.
    private string _getHighscoreUrl;
    private string _getPlayerHighscoreUrl;
    private string _postHighscoreUrl;

    // Ctors.
    public PlayerScoreClient()
    {
        _getHighscoreUrl = "http://localhost:3000/";
        _getPlayerHighscoreUrl = "http://localhost:3000/";
        _postHighscoreUrl = "http://localhost:3000/";

#if UNITY_EDITOR
        _getHighscoreUrl = "http://localhost:3000/highscore";
        _getPlayerHighscoreUrl = "http://localhost:3000/highscore/{nome}";
        _postHighscoreUrl = "http://localhost:3000/novoHighscore";
#endif
    }

    // Public Methods.
    public List<PlayerScore> GetHighscores()
    {
        var message = new HttpRequestMessage(HttpMethod.Get, _getHighscoreUrl);
        var result = SendRequest<List<PlayerScore>>(message);
        return result;
    }
    public List<PlayerScore> GetPlayerScore(string nome)
    {
        var message = new HttpRequestMessage(HttpMethod.Get, _getPlayerHighscoreUrl.Replace("{nome}", nome));
        var result = SendRequest<List<PlayerScore>>(message);
        return result;
    }
    public List<PlayerScore> PostPlayerScore(string nome, int pontos)
    {
        var score = new PlayerScore
        {
            Nome = nome,
            Ponto = pontos
        };

        var jsonSettings = new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() };
        var json = JsonConvert.SerializeObject(score, jsonSettings);
        var httpContent = new StringContent(json, System.Text.Encoding.UTF8, "application/json");

        var message = new HttpRequestMessage(HttpMethod.Post, _postHighscoreUrl);
        message.Content = httpContent;

        var result = SendRequest<List<PlayerScore>>(message);
        return result;
    }

    // Private Methods.
    private T SendRequest<T>(HttpRequestMessage message)
    {
        using (var httpClient = new HttpClient())
        {
            var result = httpClient.SendAsync(message).Result;
            var content = result.Content.ReadAsStringAsync().Result;
            var obj = JsonConvert.DeserializeObject<T>(content);

            return obj;
        }
    }
}
