using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace PokeView
{
    public class Request
    {
        public static async Task<PokeModel> PokeCall(string PokeSearch) //Methode die die Anfrage durchführt und das Model befüllt - Prob aufteilen (für andere "Füllmethoden" etc.)
        {                                                     // Task, static async Spaß war eher trial and error bis das richtige gefundne war als actual Verständnis

            PokeModel Pokemon = new PokeModel(); //neues Pokemon wird erstellt

            try
            {
                var PokeClient = new HttpClient(); //Standard Befehl um nen Neuen Client zu erstellen
                PokeClient.BaseAddress = new Uri("https://pokeapi.co/api/v2/pokemon/"); // die HttPClient Method kann Unterscheiden zwischen ner BaseAdress und dem Anhang - hier das spezifische Pokemon nach dem gesucht wird - #praktisch
                var Response = await PokeClient.GetAsync(PokeSearch); //einfacher Get Befehl zur Anfrage reicht - gibt andere, z.B. für die Übermittlung von Daten an die Zieladresse;  "a" string der Funktion dient als angefügtes Ende an die BaseAdresse

                if (Response.IsSuccessStatusCode) //Test obs das Pokemon gibt und Daten zurückgesendet wurden
                {
                    var ResponseContent = Response.Content.ReadAsStringAsync().Result; //Antwort wird als String eingelesen
                    dynamic ResponseDeserial = JsonConvert.DeserializeObject(ResponseContent); //Json wird in Object convertiert - jetzt einfacher Zugriff über Objekt.Was.Ich.Will

                    Pokemon.OnlineSuccess = true;
                    //Console.WriteLine(Pokemon.OnlineSuccess);//für tests

                    Pokemon.Name = ResponseDeserial.name;
                    //Console.WriteLine(Pokemon.Name);

                    for (int i = 0; i < ResponseDeserial.abilities.Count; i++) // gibt mehrere abilities -> count befehl gibt die ANzahl wieder
                    {
                        if (ResponseDeserial.abilities[i] != null) //absichern dass nicht Null
                        {
                            Pokemon.Ability.Add(ResponseDeserial.abilities[i].ability.name.ToString()); //hier noch komplett manuell rausgesucht unter wlechem Pfad die Daten sind, die ich will - Patrick hatte das deutlich einfacher gemahct...hätte doch ein Foto machen sollen, zusätzliches tostring() war nötig sonst Fehler
                            //Console.WriteLine(ResponseDeserial.abilities[i].ability.name);//nur für Tests
                        }
                        else
                        {
                            break;
                        }
                    }

                    for (int i = 0; i < ResponseDeserial.types.Count; i++) //selbee Logik
                    {
                        if (ResponseDeserial.types[i] != null)
                        {
                            Pokemon.Type.Add(ResponseDeserial.types[i].type.name.ToString()); //wieder  tostring() nötig, ka warum genau ... anscheinend ist das iwie noch kein string obwohls als string ausgelesen und nur in ein objekt übersetzt wird aber egal
                            //Console.WriteLine(ResponseDeserial.types[i].type.name); //Tests
                        }
                        else
                        {
                            break;
                        }

                        if (ResponseDeserial.sprites.front_default != null)
                        {

                            Pokemon.PictureUrl = ResponseDeserial.sprites.front_default;//same same umständliche Pfadsuche

                            //Console.WriteLine(Pokemon.PictureUrl);
                        }
                        if (ResponseDeserial.sprites.front_shiny != null)
                        {

                            Pokemon.ShinyUrl = ResponseDeserial.sprites.front_shiny;//smaesamesame

                            //Console.WriteLine(Pokemon.ShinyUrl);
                        }
                    }
                    return Pokemon;

                }
                else
                {
                    //Console.WriteLine("Gibts nicht!"); //Tests
                    return Pokemon;//Pokemon wid hier mit boolean auf False returned
                }
            }
            catch (Exception ex)
            {
                //Console.WriteLine($"Exception: {ex.Message}");//ka ob der catch nötig ist
                return Pokemon;//Pokemon wid hier mit boolean auf False returned
            }
        }
    }
}

