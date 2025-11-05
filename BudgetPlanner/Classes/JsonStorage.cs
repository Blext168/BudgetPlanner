using System.Text.Json;

namespace BudgetPlanner.Classes
{
    public class JsonStorage<T>(string pFilePath)
    {
        private readonly string _filePath = pFilePath;
        private readonly JsonSerializerOptions _options = new() {WriteIndented = true};

        /// <summary>
        /// Asynchronously loads and deserializes a JSON file into a list of objects of type T.
        /// </summary>
        /// <returns>
        /// A list of objects of type T deserialized from the JSON file.
        /// If the file does not exist or deserialization fails, an empty list is returned.
        /// </returns>
        public async Task<List<T>> LoadAllAsync()
        {
            if (!File.Exists(_filePath))
                return [];

            string json = await File.ReadAllTextAsync(_filePath);
            return JsonSerializer.Deserialize<List<T>>(json, _options) ?? [];
        }

        /// <summary>
        /// Asynchronously saves a list of objects of type T to a JSON file.
        /// </summary>
        /// <param name="items">The list of objects of type T to be serialized and written to the JSON file.</param>
        /// <returns>
        /// A task that represents the asynchronous operation of saving the list to the JSON file.
        /// </returns>
        public async Task SaveAsync(IEnumerable<T> items)
        {
            string json = JsonSerializer.Serialize(items, _options);
            await File.WriteAllTextAsync(_filePath, json);
        }

        /// <summary>
        /// Asynchronously adds a single object of type T to the existing list in the JSON file.
        /// If the file does not exist, a new file is created with the given object as the first entry.
        /// </summary>
        /// <param name="item">The object of type T to be added to the JSON storage.</param>
        /// <returns>
        /// A task that represents the asynchronous operation of adding the item to the JSON storage.
        /// </returns>
        public async Task AddAsync(T item)
        {
            List<T> items = await LoadAllAsync();
            items.Add(item);
            await SaveAsync(items);
        }

        /// <summary>
        /// Asynchronously removes a single object of type T from the existing list in the JSON file.
        /// If the item is not found, the list remains unchanged.
        /// </summary>
        /// <param name="predicate"></param>
        /// <returns>
        /// A task that represents the asynchronous operation of removing the item from the JSON storage.
        /// </returns>
        public async Task RemoveAsync(Func<T, bool> predicate)
        {
            List<T> items = await LoadAllAsync();
            items.RemoveAll(x => predicate(x));
            await SaveAsync(items);
        }

        /// <summary>
        /// Asynchronously updates an existing item in the JSON data store with the provided updated item.
        /// </summary>
        /// <param name="updatedItem">
        /// The updated object of type T that will replace the existing item in the data store.
        /// </param>
        /// <returns>
        /// A task that represents the asynchronous operation. The task result is a boolean indicating whether the update was successful:
        /// true if an item with a matching ID was found and updated, otherwise false.
        /// </returns>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the type T does not have a property named "Id", which is required to identify items for updates.
        /// </exception>
        public async Task<bool> UpdateAsync(T updatedItem)
        {
            var idProperty = typeof(T).GetProperty("Id");
            if (idProperty is null)
                throw new InvalidOperationException("Typ T mus eine 'Id'-Eingenschaft besitzen.");
            
            List<T> items = await LoadAllAsync();
            var updatedId = (int?)idProperty.GetValue(updatedItem);
            
            var index = items.FindIndex(x =>
            {
                var existingId = (int?)idProperty.GetValue(x);
                return existingId == updatedId;
            });
            
            if (index == -1)
                return false; // nothing found
            
            items[index] = updatedItem;
            await SaveAsync(items);
            return true;
        }
    }
}
