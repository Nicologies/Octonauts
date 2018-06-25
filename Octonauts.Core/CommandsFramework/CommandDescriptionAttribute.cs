using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace Octonauts.Core.CommandsFramework
{
    public class CommandDescriptionAttribute : DescriptionAttribute
    {
        public CommandDescriptionAttribute(string commandName, string description) : base(description)
        {
            CommandName = commandName;
        }

        public string CommandName { get; set; }
    }

    public static class CommandEnumExtensions
    {
        public static CommandDescriptionAttribute GetDescription<T>(this T enumerationValue)
            where T : struct
        {
            var type = enumerationValue.GetType();
            if (!type.IsEnum)
            {
                throw new ArgumentException($"{nameof(enumerationValue)} must be of Enum type",
                    nameof(enumerationValue));
            }

            var memberInfo = type.GetMember(enumerationValue.ToString());
            if (memberInfo.Length > 0)
            {
                var attrs = memberInfo[0].GetCustomAttributes(typeof(CommandDescriptionAttribute), false);

                if (attrs.Length > 0)
                {
                    return (CommandDescriptionAttribute)attrs[0];
                }
            }

            throw new ArgumentException(nameof(enumerationValue));
        }

        public static IEnumerable<CommandDescriptionAttribute> GetDescriptions<T>() where T : struct
        {
            return Enum.GetValues(typeof(T)).Cast<T>().Select(x => x.GetDescription());
        }
    }
}
