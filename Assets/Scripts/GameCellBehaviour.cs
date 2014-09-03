namespace Assets.Scripts
{
    public abstract class GameCellBehaviour
    {
        public GameCellObject Owner;
        public ColorData CellColorData;

        protected GameCellBehaviour(GameCellObject owner)
        {
            Owner = owner;
        }

        public abstract void OnUpdate();
    }
}