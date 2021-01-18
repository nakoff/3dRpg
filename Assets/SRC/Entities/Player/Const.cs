

namespace Entities.Player
{

    public static class Const
    {
        public enum ANIMATION { 
            IDLE, WALK_FW, WALK_BW, WALK_LEFT, WALK_RIGHT, 
            RUN_FW, RUN_BW, RUN_LEFT, RUN_RIGHT,
            ATTACK_FIREBALL_BIG, ATTACK_FIREBALL_SMALL, ATTACK_SPELL_GROUND,
        }

        public static int WalkSpeed { get; } = 100;
        public static int RunSpeed { get; } = 200;
    }
}