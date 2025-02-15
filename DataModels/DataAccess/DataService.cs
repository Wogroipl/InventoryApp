using System.Reflection;

namespace Repository.DataAccess;

/// <summary>
/// Generic data service for CRUD operations on entities.
/// </summary>
/// <typeparam name="T"></typeparam>
public class DataService<T>
{
    // Inventory instance to store entities injected via constructor.
    private readonly Inventory _inventory;

    /// <summary>
    /// Constructor to inject Inventory instance.
    /// </summary>
    /// <param name="inventory"></param>
    public DataService(Inventory inventory)
    {
        _inventory = inventory;
    }

    /// <summary>
    /// Add an entity to the Inventory.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task AddAsync(T entity)
    {
        await Task.Delay(300);

        var property = _inventory.GetType()
                                 .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                 .FirstOrDefault(p => p.PropertyType == typeof(List<T>));

        if (property != null)
        {
            var list = (List<T>)property.GetValue(_inventory)!;
            list.Add(entity);
        }
        else
        {
            throw new InvalidOperationException($"No list found for type {typeof(T).Name} in Inventory.");
        }

    }

    /// <summary>
    /// Get all entities from the Inventory.
    /// </summary>
    /// <returns>IEnumerable<T></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<IEnumerable<T>> GetAllAsync()
    {
        await Task.Delay(300);
        var property = _inventory.GetType()
                                 .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                 .FirstOrDefault(p => p.PropertyType == typeof(List<T>));
        if (property != null)
        {
            var list = (List<T>)property.GetValue(_inventory)!;

            foreach (var entity in list)
            {
                LoadNavigationProperties(entity);
            }

            return list;
        }
        else
        {
            throw new InvalidOperationException($"No list found for type {typeof(T).Name} in Inventory.");
        }
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task<T?> GetByIdAsync(Guid id)
    {
        await Task.Delay(300);

        var property = _inventory.GetType()
                                 .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                 .FirstOrDefault(p => p.PropertyType == typeof(List<T>));

        if (property == null)
        {
            throw new InvalidOperationException($"No list found for type {typeof(T).Name} in Inventory.");
        }

        var list = (List<T>)property.GetValue(_inventory)!;

        if (list == null)
        {
            throw new InvalidOperationException($"The list for type {typeof(T).Name} in Inventory is null");
        }
        var entity = list.FirstOrDefault(e => (Guid)e.GetType().GetProperty("Id")?.GetValue(e)! == id);
        if (entity == null)
        {
            throw new InvalidOperationException($"No entity found with id {id} in list.");
        }
        return entity;
    }

    /// <summary>
    /// Update an entity in the Inventory.
    /// </summary>
    /// <param name="entity"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task UpdateAsync(T entity)
    {
        await Task.Delay(300);

        var property = _inventory.GetType()
                                 .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                 .FirstOrDefault(p => p.PropertyType == typeof(List<T>));
        if (property != null)
        {
            var list = (List<T>)property.GetValue(_inventory)!;
            var existingEntity = list.FirstOrDefault(e => (Guid)e.GetType().GetProperty("Id")!.GetValue(e)! == (Guid)entity.GetType().GetProperty("Id")!.GetValue(entity)!);
            if (existingEntity != null)
            {
                var index = list.IndexOf(existingEntity);
                list[index] = entity;
            }
            else
            {
                throw new InvalidOperationException($"No entity found with id {entity.GetType().GetProperty("Id")!.GetValue(entity)} in list.");
            }
        }
        else
        {
            throw new InvalidOperationException($"No list found for type {typeof(T).Name} in Inventory.");
        }
    }

    /// <summary>
    /// Delete an entity from the Inventory.
    /// </summary>
    /// <param name="id"></param>
    /// <returns></returns>
    /// <exception cref="InvalidOperationException"></exception>
    public async Task DeleteAsync(Guid id)
    {
        await Task.Delay(300);

        var property = _inventory.GetType()
                                 .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                 .FirstOrDefault(p => p.PropertyType == typeof(List<T>));
        if (property != null)
        {
            var list = (List<T>)property.GetValue(_inventory)!;
            var entity = list.FirstOrDefault(e => (Guid)e.GetType().GetProperty("Id")!.GetValue(e)! == id);
            if (entity != null)
            {
                list.Remove(entity);
            }
            else
            {
                throw new InvalidOperationException($"No entity found with id {id} in list.");
            }
        }
        else
        {
            throw new InvalidOperationException($"No list found for type {typeof(T).Name} in Inventory.");
        }
    }

    /// <summary>
    /// Load navigation properties for an entity to be able to retrieve related entities.
    /// </summary>
    /// <param name="entity"></param>
    private void LoadNavigationProperties(object entity)
    {
        var properties = entity.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance)
                               .Where(p => p.PropertyType.IsClass && p.PropertyType != typeof(string));

        foreach (var property in properties)
        {
            var navigationProperty = _inventory.GetType()
                                               .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                               .FirstOrDefault(p => p.PropertyType == typeof(List<>).MakeGenericType(property.PropertyType));
            if (navigationProperty != null)
            {
                var navigationList = (IEnumerable<object>)navigationProperty.GetValue(_inventory)!;
                var foreignKeyProperty = entity.GetType().GetProperty(property.Name + "Id");
                if (foreignKeyProperty != null)
                {
                    var foreignKeyValue = foreignKeyProperty.GetValue(entity);
                    var relatedEntity = navigationList.FirstOrDefault(e => e.GetType().GetProperty("Id")!.GetValue(e)!.Equals(foreignKeyValue));
                    property.SetValue(entity, relatedEntity);
                }
            }
        }
    }

}
