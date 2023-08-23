namespace OnlineShopWebAPI.DAL
{
    public interface IRepository<T>
    {
        List<T> GetItems();
        T GetItemById(string id);
        void RemoveItem(string id);
        void AddItem(T item);
        T ModifyItem(T item);
        bool ItemExists(string id);
    }
}
