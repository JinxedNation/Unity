public interface IClickable
{
    /**
     * Represents a type of click/action
     */
    public enum ClickType
    {
        Primary,
        Secondary
    }

    public void Clicked(ClickType clickType);
}