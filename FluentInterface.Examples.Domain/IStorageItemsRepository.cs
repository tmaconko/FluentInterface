namespace FluentInterface.Examples.Domain
{
    public interface IStorageItemsRepository
    {
        StorageItem GetByProductId(int id);
        void Store(StorageItem storageItem);
    }
}