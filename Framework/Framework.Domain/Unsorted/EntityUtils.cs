using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Domain
{
    public static class EntityUtils
    {
        public static bool IsAssignedOrNull(int entityId)
        {
            return (entityId != Entity.UnassignedId);
        }

        public static bool IsNull(int entityId)
        {
            return (entityId == Entity.NullId);
        }

        public static bool IsAssigned(int entityId)
        {
            return (entityId != Entity.NullId) && (entityId != Entity.UnassignedId);
        }

        public static bool IsUnassigned(int entityId)
        {
            return entityId == Entity.UnassignedId;
        }

        public static bool AreSame(int entityId, int otherEntityId)
        {
            return IsAssignedOrNull(entityId) && (entityId == otherEntityId);
        }
    }
}
