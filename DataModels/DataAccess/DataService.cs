using System.Reflection;

namespace Repository.DataAccess;

public class DataService<T>
{
    private readonly Inventory _inventory;

    public DataService(Inventory inventory)
    {
        _inventory = inventory;
    }

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

    public async Task<T?> GetByIdAsync(int id)
    {
        await Task.Delay(300);

        var property = _inventory.GetType()
                                 .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                 .FirstOrDefault(p => p.PropertyType == typeof(List<T>));
        if (property != null)
        {
            var list = (List<T>)property.GetValue(_inventory)!;
            return list.FirstOrDefault(e => (int)e.GetType().GetProperty("Id")!.GetValue(e)! == id);
        }
        else
        {
            throw new InvalidOperationException($"No list found for type {typeof(T).Name} in Inventory.");
        }
        await Task.CompletedTask;

    }

    public async Task UpdateAsync(T entity)
    {
        await Task.Delay(300);

        var property = _inventory.GetType()
                                 .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                 .FirstOrDefault(p => p.PropertyType == typeof(List<T>));
        if (property != null)
        {
            var list = (List<T>)property.GetValue(_inventory)!;
            var existingEntity = list.FirstOrDefault(e => (int)e.GetType().GetProperty("Id")!.GetValue(e)! == (int)entity.GetType().GetProperty("Id")!.GetValue(entity)!);
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

    public async Task DeleteAsync(int id)
    {
        await Task.Delay(300);

        var property = _inventory.GetType()
                                 .GetProperties(BindingFlags.Public | BindingFlags.Instance)
                                 .FirstOrDefault(p => p.PropertyType == typeof(List<T>));
        if (property != null)
        {
            var list = (List<T>)property.GetValue(_inventory)!;
            var entity = list.FirstOrDefault(e => (int)e.GetType().GetProperty("Id")!.GetValue(e)! == id);
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
