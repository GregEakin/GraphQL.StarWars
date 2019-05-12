using System.Collections.Generic;
using System.Linq;
using HotChocolate;
using StarWars.Data;
using StarWars.Models;

namespace StarWars.Resolvers
{
    public class SharedResolvers
    {
        public IEnumerable<ICharacter> GetCharacter(
            [Parent]ICharacter character,
            [Service]CharacterRepository repository) =>
            character.Friends.Select(repository.GetCharacter).Where(friend => friend != null);

        public double GetHeight(Unit? unit, [Parent]ICharacter character)
            => ConvertToUnit(character.Height, unit);

        public double GetLength(Unit? unit, [Parent]Starship starship)
            => ConvertToUnit(starship.Length, unit);

        private static double ConvertToUnit(double length, Unit? unit) => 
            unit == Unit.Foot ? length * 3.28084 : length;
    }
}
