using System;

namespace Core.Security
{
    [Obsolete("Устаревшее перечисление ролей")]
    public enum UserRoleType
    {
        Administrator,
        Moderator,
        User,
        Guest,
        Undefined
    }
}