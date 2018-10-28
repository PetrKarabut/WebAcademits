using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ShopEF.Model;

namespace ShopEF.Console
{
    public class ConsoleFunctions
    {
        public void Write(IEnumerable<IEntity> entities)
        {
            foreach (var entity in entities)
            {
                Write(entity);
                System.Console.WriteLine();
            }
        }

        public void Write(IEntity entity)
        {
            var info = GetPropertiesInformation(entity);
            foreach (var entityPropertyInfo in info)
            {
                System.Console.WriteLine();
                System.Console.Write(" " + entityPropertyInfo.Alias + ":");
                if (entityPropertyInfo.Value is ICollection)
                {
                    foreach (var item in (ICollection)entityPropertyInfo.Value)
                    {
                        WriteShort((IEntity)item);
                        System.Console.Write(";");
                    }
                    continue;
                }

                if (entityPropertyInfo.Value is IEntity)
                {
                    WriteShort((IEntity)entityPropertyInfo.Value);
                    continue;
                }

                System.Console.Write(" " + entityPropertyInfo.Value);

            }
        }

        public void WriteShort(IEntity entity)
        {
            var info = GetPropertiesInformation(entity).Where(x => x.UseInShortForm).ToArray();
            if (info.Length == 1)
            {
                System.Console.Write(" " + info.First().Value);
                return;
            }

            for (var index = 0; index < info.Length; index++)
            {
                var entityPropertyInfo = info[index];
                System.Console.Write(" " + entityPropertyInfo.Alias + ":");
                if (entityPropertyInfo.Value is IEntity)
                {
                    WriteShort((IEntity) entityPropertyInfo.Value);
                    continue;
                }

                System.Console.Write(" " + entityPropertyInfo.Value);
                if (index < info.Length - 1)
                {
                    System.Console.Write(",");
                }
            }
        }

        private EntityPropertyInfo[] GetPropertiesInformation(IEntity entity)
        {
            var type = entity.GetType().BaseType;
            var properties = type.GetProperties().Select(x => new
            {
                Property = x,
                PropertyAttribute =
                        (ConsoleAlias)x.GetCustomAttributes(typeof(ConsoleAlias), false).FirstOrDefault()
            }).Where(x => x.PropertyAttribute != null)
                .Select(x => new EntityPropertyInfo
                {
                    Value = x.Property.GetValue(entity),
                    Alias = x.PropertyAttribute.Alias,
                    UseInReading = x.PropertyAttribute.UseInReading,
                    UseInShortForm = x.PropertyAttribute.UseInShortForm
                }).ToArray();

            return properties;
        }
    }
}
