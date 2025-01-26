using System.Dynamic;
using System.Reflection;

namespace Sociam.Application.Helpers;

public sealed class DataShaper<T>
{
    public IEnumerable<ExpandoObject> ShapeData(IEnumerable<T> entities, string fieldsString)
    {
        var requiredProperties = GetProperties(fieldsString);
        return FetchData(entities, requiredProperties);
    }

    public ExpandoObject ShapeData(T entity, string fieldsString)
    {
        var requiredProperties = GetProperties(fieldsString);
        return FetchDataForEntity(entity, requiredProperties);
    }

    private List<PropertyInfo> GetProperties(string fieldsString)
    {
        if (string.IsNullOrWhiteSpace(fieldsString))
        {
            return typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance).ToList();
        }

        var fields = fieldsString.Split(',', StringSplitOptions.RemoveEmptyEntries);
        var propertyInfos = typeof(T)
            .GetProperties(BindingFlags.Public | BindingFlags.Instance)
            .Where(p => fields.Contains(p.Name, StringComparer.OrdinalIgnoreCase))
            .ToList();

        return propertyInfos;
    }

    private List<ExpandoObject> FetchData(IEnumerable<T> entities, List<PropertyInfo> requiredProperties)
    {
        var shapedData = new List<ExpandoObject>();

        foreach (var entity in entities)
        {
            var shapedObject = FetchDataForEntity(entity, requiredProperties);
            shapedData.Add(shapedObject);
        }

        return shapedData;
    }

    private ExpandoObject FetchDataForEntity(T entity, List<PropertyInfo> requiredProperties)
    {
        var shapedObject = new ExpandoObject();

        foreach (var property in requiredProperties)
        {
            var propertyValue = property.GetValue(entity);
            (shapedObject as IDictionary<string, object>).Add(property.Name, propertyValue!);
        }

        return shapedObject;
    }
}

