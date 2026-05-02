using Domain.Table.Enum;
using System;
using System.Collections.Generic;
using System.Text;

namespace Domain.Table.ValueObject
{
    public class StatusAccess
    {
        public UserId? UserId { get; }
        public StatusLocked StatusLocked { get; }

        public StatusAccess(UserId? userId, StatusLocked statusLocked)
        {
            UserId = userId;
            StatusLocked = statusLocked;
        }


        public bool IsSameUser(UserId userId) => userId == UserId;
        public bool StatusIsLocked() => StatusLocked == StatusLocked.Bloqueado;
        public bool StatusIsUnlocked() => StatusLocked == StatusLocked.Desbloqueado;

        public static StatusAccess Locked(UserId userId) => new(userId, StatusLocked.Bloqueado);
        public static StatusAccess Unlocked() => new(null, StatusLocked.Desbloqueado);

    }
}
