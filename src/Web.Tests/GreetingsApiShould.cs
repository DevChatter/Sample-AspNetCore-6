using System.Collections.Generic;
using System.Net;
using System.Net.Http.Json;
using System.Threading.Tasks;
using Xunit;

namespace DevChatter.Sample.Web.Tests
{
    public class GreetingsApiShould
    {
        private static readonly string[] _greetingsToAdd =  new[] { "What's shakin'?!", "Hello and welcome!", "Hi everybody!" };

        [Fact]
        public async Task GetAnEmptyListOfGreetings()
        {
            await using GreetingApi? application = new();
            var client = application.CreateClient();

            var greetings = await client.GetFromJsonAsync<List<Greeting>>("/greetings");

            Assert.Empty(greetings);
        }

        [Fact]
        public async Task HaveGreetingsAfterPostingNew()
        {
            await using GreetingApi? application = new();
            var client = application.CreateClient();

            for (int i = 0; i < _greetingsToAdd.Length; i++)
            {
                var response = await client.PostAsJsonAsync("/greetings", new Greeting { Text = _greetingsToAdd[i] });

                Assert.Equal(HttpStatusCode.Created, response.StatusCode);

                var storedGreetings = await client.GetFromJsonAsync<List<Greeting>>("/greetings");

                Assert.NotNull(storedGreetings);
                Assert.Equal(i+1, storedGreetings!.Count);
                Assert.Equal(_greetingsToAdd[i], storedGreetings[i].Text);
            }
        }
    }
}