using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tekus.Data.Entities.Base
{
    public class TypeIdentifiable<T>
    {
        public TypeIdentifiable()
        {
        }

        /// <summary>
        /// Gets or sets the Id for the entity
        /// </summary>
        public virtual T Id { get; set; }

        /// <summary>
        /// Returns the Id's hashcode value
        /// </summary>
        /// <returns>The hashcode of this object</returns>
        public override int GetHashCode()
        {
            return this.Id.GetHashCode();
        }

        /// <summary>
        /// Determines whether the specified <see cref="System.Object"></see>, is equal to this instance.
        /// </summary>
        /// <param name="obj">The <see cref="System.Object" /> to compare with this instance.</param>
        /// <returns>
        ///   <c>true</c> if the specified <see cref="System.Object" /> is equal to this instance; otherwise, <c>false</c>.
        /// </returns>
        public override bool Equals(object obj)
        {
            var entity = obj as TypeIdentifiable<T>;

            if (entity != null)
            {
                return this.Id.Equals(entity.Id);
            }

            return false;
        }
    }
}
