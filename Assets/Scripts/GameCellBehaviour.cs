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
        public abstract void OnDraw(GameCellObject otherCell);
        public abstract void OnLeave();
    }
}