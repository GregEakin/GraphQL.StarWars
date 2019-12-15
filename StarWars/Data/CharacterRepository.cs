// Copyright 2019 Greg Eakin
// 
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at:
//     http://www.apache.org/licenses/LICENSE-2.0
// 
// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.

using System;
using System.Collections.Generic;
using System.Linq;
using StarWars.Models;

namespace StarWars.Data
{
    public class CharacterRepository
    {
        private readonly Dictionary<string, ICharacter> _characters = CreateCharacters().ToDictionary(t => t.Id);
        private readonly Dictionary<string, Starship> _starships = CreateStarships().ToDictionary(t => t.Id);

        public ICharacter GetHero(Episode episode) => episode == Episode.Empire
            ? _characters["1000"]
            : _characters["2001"];

        public ICharacter GetCharacter(string id) =>
            _characters.TryGetValue(id, out var c)
                ? c
                : null;

        public Human GetHuman(string id) =>
            _characters.TryGetValue(id, out var c) && c is Human h
                ? h
                : null;

        public Droid GetDroid(string id) =>
            _characters.TryGetValue(id, out var c) && c is Droid d
                ? d
                : null;

        public IEnumerable<object> Search(string text)
        {
            var characters = _characters.Values.Where(t => t.Name.Contains(text, StringComparison.OrdinalIgnoreCase));
            foreach (var character in characters)
                yield return character;

            var starships = _starships.Values.Where(t => t.Name.Contains(text, StringComparison.OrdinalIgnoreCase));
            foreach (var starship in starships)
                yield return starship;
        }

        private static IEnumerable<ICharacter> CreateCharacters()
        {
            yield return new Human
            {
                Id = "1000",
                Name = "Luke Skywalker",
                Friends = new[] {"1002", "1003", "2000", "2001"},
                AppearsIn = new[] {Episode.NewHope, Episode.Empire, Episode.Jedi},
                HomePlanet = "Tatooine"
            };

            yield return new Human
            {
                Id = "1001",
                Name = "Darth Vader",
                Friends = new[] {"1004"},
                AppearsIn = new[] {Episode.NewHope, Episode.Empire, Episode.Jedi},
                HomePlanet = "Tatooine"
            };

            yield return new Human
            {
                Id = "1002",
                Name = "Han Solo",
                Friends = new[] {"1000", "1003", "2001"},
                AppearsIn = new[] {Episode.NewHope, Episode.Empire, Episode.Jedi}
            };

            yield return new Human
            {
                Id = "1003",
                Name = "Leia Organa",
                Friends = new[] {"1000", "1002", "2000", "2001"},
                AppearsIn = new[] {Episode.NewHope, Episode.Empire, Episode.Jedi},
                HomePlanet = "Alderaan"
            };

            yield return new Human
            {
                Id = "1004",
                Name = "Wilhuff Tarkin",
                Friends = new[] {"1001"},
                AppearsIn = new[] {Episode.NewHope}
            };

            yield return new Droid
            {
                Id = "2000",
                Name = "C-3PO",
                Friends = new[] {"1000", "1002", "1003", "2001"},
                AppearsIn = new[] {Episode.NewHope, Episode.Empire, Episode.Jedi},
                PrimaryFunction = "Protocol"
            };

            yield return new Droid
            {
                Id = "2001",
                Name = "R2-D2",
                Friends = new[] {"1000", "1002", "1003"},
                AppearsIn = new[] {Episode.NewHope, Episode.Empire, Episode.Jedi},
                PrimaryFunction = "Astromech"
            };
        }

        private static IEnumerable<Starship> CreateStarships()
        {
            yield return new Starship
            {
                Id = "3000",
                Name = "TIE Advanced x1",
                Length = 9.2
            };

            yield return new Starship
            {
                Id = "3001",
                Name = "Millennium Falcon",
                Length = 34.75
            };
        }
    }
}