using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Xrm.Domain.ModelBase
{
    public abstract class EntityBase : INotify
    {
        public Guid Id { get; set; }
        public string LogicalName { get; set; }
        public List<string> ModifiedFields { get; set; }
        public bool RecordChanges { get; set; }

        public EntityBase()
            : this(Guid.Empty)
        {
        }

        public EntityBase(bool recordChanges)
            : this(Guid.Empty, recordChanges)
        {
        }

        public EntityBase(Guid id, bool recordModification = true)
        {
            Id = id;
            ModifiedFields = new List<string>();
            RecordChanges = recordModification;
        }

        public override bool Equals(object entity)
        {
            if (entity == null || !(entity is EntityBase))
            {
                return false;
            }

            return (this == (EntityBase)entity);
        }

        public static bool operator ==(EntityBase entity1, EntityBase entity2)
        {
            if ((object)entity1 == null && (object)entity2 == null)
                return true;

            if ((object)entity1 == null || (object)entity2 == null)
                return false;

            if (entity1.Id != entity2.Id)
                return false;

            return true;
        }

        public static bool operator !=(EntityBase entity1, EntityBase entity2)
        {
            return !(entity1 == entity2);
        }

        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        //public void RaisePropertyChanged(string propertyName)
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, new PropertyChangedEventArgs(propertyName));

        //    // Call extension point
        //    OnPropertyChanged(propertyName);
        //}

        //public void RaisePropertyChanged<T>(Expression<Func<T>> property)
        //{
        //    if (PropertyChanged != null)
        //        PropertyChanged(this, new PropertyChangedEventArgs(property.Name));

        //    // Call extension point
        //    OnPropertyChanged(property.Body.ToString());
        //}

        public void OnPropertyChanged(string propertyName)
        {
            if(!ModifiedFields.Contains(propertyName))
                ModifiedFields.Add(propertyName);
        }
    }
}
