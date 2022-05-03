using System;
using System.Collections.Generic;
using System.Linq;
using GameProject.Entities;

namespace GameProject.Domain
{
    internal class Game
    {
        internal Player Player{ get; }
        internal GameStage Stage { get; private set; } = GameStage.NotStarted;
        internal event Action<GameStage> StageChanged;
        internal Game(Player player)
        {
            Player = player;
        }

        private void ChangeStage(GameStage stage)
        {
            Stage = stage;
            StageChanged?.Invoke(stage);
        }
    }
}
