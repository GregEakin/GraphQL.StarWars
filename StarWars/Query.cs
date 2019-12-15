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
using HotChocolate.Resolvers;
using StarWars.Data;
using StarWars.Models;

namespace StarWars
{
    public class Query
    {
        private readonly CharacterRepository _repository;

        public Query(CharacterRepository repository) =>
            _repository = repository
                          ?? throw new ArgumentNullException(nameof(repository));

        public ICharacter GetHero(Episode episode) => _repository.GetHero(episode);

        public Human GetHuman(string id) => _repository.GetHuman(id);

        public Droid GetDroid(string id) => _repository.GetDroid(id);

        public IEnumerable<ICharacter> GetCharacter(string[] characterIds, IResolverContext context)
        {
            foreach (var characterId in characterIds)
            {
                var character = _repository.GetCharacter(characterId);
                if (character != null)
                {
                    yield return character;
                }
                else
                {
                    context.ReportError($"Could not resolve a character for the character-id {characterId}.");
                }
            }
        }

        public IEnumerable<object> Search(string text) => _repository.Search(text);
    }
}