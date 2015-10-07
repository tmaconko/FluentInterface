using System.Collections.Generic;
using System.Linq;

namespace FluentInterface.Examples.Domain
{
    public class StorageItemsRepositoryInMemory : IStorageItemsRepository
    {
        public IList<StorageItem> StorageItems;

        public StorageItemsRepositoryInMemory()
        {
            StorageItems = new List<StorageItem>();
        }

        public StorageItem GetByProductId(int id)
        {
            return StorageItems.First(p => p.Product.Id == id);
        }

        public void Store(StorageItem storageItem)
        {
            var item = StorageItems.First(p => p.Id == storageItem.Id);
            StorageItems.Remove(item);
            StorageItems.Add(item);
        }
    }
}