// Copyright (c) ppy Pty Ltd <contact@ppy.sh>. Licensed under the MIT Licence.
// See the LICENCE file in the repository root for full licence text.

using NUnit.Framework;
using osu.Game.Screens.Multi.Components;
using osu.Game.Users;

namespace osu.Game.Tests.Visual.RealtimeMultiplayer
{
    public class TestSceneRealtimeMultiplayer : RealtimeMultiplayerTestScene
    {
        public TestSceneRealtimeMultiplayer()
        {
            var multi = new TestRealtimeMultiplayer();

            AddStep("show", () => LoadScreen(multi));
            AddUntilStep("wait for loaded", () => multi.IsLoaded);
        }

        [Test]
        public void TestOneUserJoinedMultipleTimes()
        {
            var user = new User { Id = 33 };

            AddRepeatStep("add user multiple times", () => Client.AddUser(user), 3);

            AddAssert("room has 2 users", () => Client.Room?.Users.Count == 2);
        }

        [Test]
        public void TestOneUserLeftMultipleTimes()
        {
            var user = new User { Id = 44 };

            AddStep("add user", () => Client.AddUser(user));
            AddAssert("room has 2 users", () => Client.Room?.Users.Count == 2);

            AddRepeatStep("remove user multiple times", () => Client.RemoveUser(user), 3);
            AddAssert("room has 1 user", () => Client.Room?.Users.Count == 1);
        }

        private class TestRealtimeMultiplayer : Screens.Multi.RealtimeMultiplayer.RealtimeMultiplayer
        {
            protected override RoomManager CreateRoomManager() => new TestRealtimeRoomManager();
        }
    }
}
