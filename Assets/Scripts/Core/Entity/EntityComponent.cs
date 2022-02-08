namespace cardooo.core
{
    public class EntityComponent
    {
        Entity entity = null;
        public Entity OwnEntity {
            get { return entity; }
            set { }
        }

        public EntityComponent(Entity entity)
        {
            this.entity = entity;
            Init(entity);
        }

        public virtual void Init(Entity entity) { }

        public virtual void Terminal() { }
    }
}
