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