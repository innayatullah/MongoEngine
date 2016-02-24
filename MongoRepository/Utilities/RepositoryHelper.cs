using System;
using System.Linq.Expressions;
using System.Reflection;
using MongoDB.Driver;
using MongoDB.Driver.Builders;

namespace MongoRepository.Utilities
{
    public static class RepositoryHelper
    {
        /// <summary>
        /// Tells if the type has a public property with the name
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="prop">Name of the property</param>
        /// <returns></returns>
        public static bool HasProperty<T>(string prop)
        {
            var propertyInfo = typeof(T).GetProperty(prop, BindingFlags.Public | BindingFlags.Instance);
            return null != propertyInfo && propertyInfo.CanWrite;
        }

        /// <summary>
        /// Sets the value of property of object
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="obj">object</param>
        /// <param name="prop">property name</param>
        /// <param name="value">value to set</param>
        public static void SetValue<T>(T obj, string prop, object value)
        {
            var propertyInfo = typeof(T).GetProperty(prop, BindingFlags.Public | BindingFlags.Instance);
            if (propertyInfo != null && propertyInfo.CanWrite)
            {
                propertyInfo.SetValue(obj, Convert.ChangeType(value, propertyInfo.PropertyType), null);
            }
        }

        /// <summary>
        /// Get the value of generic object with property name
        /// </summary>
        /// <typeparam name="T">Generic object Type</typeparam>
        /// <param name="obj">Generic object</param>
        /// <param name="prop">Name of the property</param>
        /// <returns></returns>
        public static object GetValue<T>(T obj, string prop)
        {
            // Get the propertyinformation using reflection
            var propertyInfo = typeof(T).GetProperty(prop, BindingFlags.Public | BindingFlags.Instance);
            // If the object has the said property and it has a getter the retrieve the value
            if (propertyInfo != null && propertyInfo.CanRead)
            {
                return propertyInfo.GetValue(obj);
            }
            return null;
        }

        /// <summary>
        /// Creates a MongoUpdate type using reflection for a generic item
        /// </summary>
        /// <typeparam name="T">Type to create update type</typeparam>
        /// <param name="item">object of type T to get values from</param>
        /// <returns></returns>
        public static IMongoUpdate GetMongoUpdate<T>(T item)
        {
            // Get all the properties of the object of type T
            var properties = typeof(T).GetProperties();
            // Create an UpdateBuilder to fill with updates
            var update = new UpdateBuilder<T>();
            // Loop through all the properties and set them
            foreach (var propertyInfo in properties)
            {
                // Get property type
                var type = propertyInfo.PropertyType;
                var value = Convert.ChangeType(GetValue(item, propertyInfo.Name), propertyInfo.PropertyType);
                update =(UpdateBuilder<T>) (typeof(RepositoryHelper)
                            .GetMethod("UpdateBuilder")
                            .MakeGenericMethod(typeof(T), type)
                            .Invoke(null, new[] { propertyInfo.Name, update, value }));
            }

            return update;
        }

        /// <summary>
        /// Creates a lambda expression for the property
        /// </summary>
        /// <typeparam name="T">Type of main object</typeparam>
        /// <typeparam name="TMember">Type of property</typeparam>
        /// <param name="property">Name of the property</param>
        /// <param name="update">update builder instance</param>
        /// <param name="value">value to set</param>
        /// <returns></returns>
        // ReSharper disable once UnusedMember.Local
        private static UpdateBuilder<T> UpdateBuilder<T, TMember>(string property, UpdateBuilder<T> update, TMember value)
        {
            if (property == "Id") return update;
            // Create expression parameters
            var parameter = Expression.Parameter(typeof(T), "i");
            // Create the property with the property name and the parameter (main object type)
            var memberExpression = Expression.Property(parameter, property);
            // Create a queryable type from the type of property
            var queryableType = typeof(TMember);
            
            // Create a delegate function type from the queryable type
            var delegateType = typeof(Func<,>).MakeGenericType(typeof(T), queryableType);
            // create and return Lambda expression using delegate, memberExpression and parameter
            var expression = (Expression<Func<T, TMember>>)Expression.Lambda(delegateType, memberExpression, parameter);

            update.Set(expression, value);
            return update;
        }

        /// <summary>
        /// Returns a lambda expression of the form args => args.property
        /// </summary>
        /// <typeparam name="T">Base object type whose property we want to access</typeparam>
        /// <typeparam name="TMember">Type of property</typeparam>
        /// <param name="property">Name of the property</param>
        /// <returns></returns>
        public static Expression<Func<T, TMember>> GetLambdaExpression<T, TMember>(string property)
        { 
            // Create expression parameters
            var parameter = Expression.Parameter(typeof(T), "i");
            // Create the property with the property name and the parameter (main object type)
            var memberExpression = Expression.Property(parameter, property);
            // Create a queryable type from the type of property
            var queryableType = typeof(TMember);

            // Create a delegate function type from the queryable type
            var delegateType = typeof(Func<,>).MakeGenericType(typeof(T), queryableType);
            // create and return Lambda expression using delegate, memberExpression and parameter
            return (Expression<Func<T, TMember>>)Expression.Lambda(delegateType, memberExpression, parameter);
        }
    }
}