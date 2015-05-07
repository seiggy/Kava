using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using RestSharp;

namespace Kava.Models
{
    public class Character
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string World { get; set; }
        public string Title { get; set; }
        public string Avatar { get; set; }
        public string AvatarHash { get; set; }
        public string Portrait { get; set; }
        public string Bio { get; set; }
        public string Race { get; set; }
        public string Clan { get; set; }
        public string Gender { get; set; }
        public string Nameday { get; set; }
        public string Guardian { get; set; }
        public string GuardianIcon { get; set; }
        public string City { get; set; }
        public string CityIcon { get; set; }
        public GrandCompany GrandCompany { get; set; }
        public string GrandCompanyRank { get; set; }
        public string GrandCompanyIcon { get; set; }
        public string FreeCompany { get; set; }
        public string FreeCompanyId { get; set; }
        public List<string> FreeCompanyIcon { get; set; }

        public List<ClassJob> ClassJobs { get; set; }
        // Gear
        // GearBonus
        // GearStats
        // Attributes

        public string ActiveClass { get; set; }
        public string ActiveJob { get; set; }
        public string ActiveLevel { get; set; }

        public List<Minion> Minions { get; set; }
        public List<Mount> Mounts { get; set; } 

        public string Hash { get; set; }
        
        // events are the Achievements. Needs more data for this first.
        //public string Events { get; set; }
        public bool All50 { get; set; }

        public Character LoadCharacter(string CharacterNameOrId, string Server)
        {
            var restClient = new RestClient("http://xivsync.com");
            var idRequest = new RestRequest(Method.GET) {Resource = "/search/character"};
            idRequest.AddQueryParameter("name", CharacterNameOrId);
            idRequest.AddQueryParameter("server", Server);

            var response = restClient.Execute(idRequest);
            dynamic jsonData = JsonConvert.DeserializeObject(response.Content);

            if (jsonData.data.Length != 1)
            {
                throw new ArgumentException("Invalid Character Name or Id", CharacterNameOrId);
            }
            string lodestoneId = jsonData.data[0].id;

            var characterRequest = new RestRequest(method:Method.GET) { Resource = "/character/get"};
            characterRequest.AddQueryParameter("lodestone", lodestoneId);

            var characterResponse = restClient.Execute(characterRequest);
            dynamic characterData = JsonConvert.DeserializeObject(characterResponse.Content);

            return (Character) characterData.data;
        }
    }
}
