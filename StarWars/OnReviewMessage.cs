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

using HotChocolate.Language;
using HotChocolate.Subscriptions;
using StarWars.Models;

namespace StarWars
{
    public class OnReviewMessage
        : EventMessage
    {
        public OnReviewMessage(Episode episode, Review review)
            : base(CreateEventDescription(episode), review)
        {
        }

        private static EventDescription CreateEventDescription(Episode episode)
        {
            return new EventDescription("onReview",
                new ArgumentNode("episode",
                    new EnumValueNode(
                        episode.ToString().ToUpperInvariant())));
        }
    }
}