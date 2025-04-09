public interface IListItemRenderer<in T>
{
    public void BindView(T value);
}