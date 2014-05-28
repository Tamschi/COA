namespace COA.Core.Entities
{
    public abstract class DrawableEntity : Entity
    {
        public bool Visible { get; set; }

        public void Draw()
        {
            if (!Visible) return;
            OnDraw();
        }

        protected abstract void OnDraw();
    }
}