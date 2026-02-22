using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Framework.Domain
{
    public class Entity
    {
        public const int NullId = 0;
        public const int UnassignedId = -1;
        public const int UnsavedVersion = -1;

        public int Id { get; set; }
        public int Version { get; set; }

        public Entity()
        {
            Version = UnsavedVersion;
        }

        public bool IsSaved
        {
            get { return Id != NullId && Id != UnassignedId; }
        }

        protected bool Equals(Entity other)
        {
            return IsSaved && other.IsSaved && Id == other.Id;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj))
            {
                return false;
            }

            if (ReferenceEquals(this, obj))
            {
                return true;
            }

            if (obj.GetType() != GetType())
            {
                return false;
            }

            return Equals((Entity)obj);
        }

        public override int GetHashCode()
        {
            return Id;
        }
    }
}
