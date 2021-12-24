using SantaClauseConsoleApp.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SantaClauseConsoleApp.Repository
{
    interface Repository<T, ID> where T : Entity<ID>
    {
        /// <summary>
        /// The function finds if there is an entity with that id
        /// </summary>
        /// <param name="id">A entity id (ID)</param>
        /// <returns>
        ///     T - if there is a entity with that id |
        ///     null - if there is no entity with that id
        /// </returns>
        T findOne(ID id);

        /// <summary>
        /// The functions provides all the entities stored
        /// </summary>
        /// <returns> IEnumerable<Child> with all the entities </returns>
        IEnumerable<T> GetAll();

        /// <summary>
        /// The function deletes a child with a certain id
        /// </summary>
        /// <param name="id">The Child's ID (int)</param>
        /// <returns></returns>
        T delete(ID id);

        /// <summary>
        /// The function saves an entity by adding them to the memory and on disk
        /// </summary>
        /// <param name="o">The entity to be saved(Child)</param>
        /// <returns> 
        ///           o - if the save failed |
        ///           null - if the entity was saved succesfully 
        /// </returns>
        T add(T o);

        /// <summary>
        /// The function updates a childs data
        /// </summary>
        /// <param name="o">A child object with the same id and different fields</param>
        /// <returns>
        ///     o - if the child coulnd't be updated
        ///     null - if the child was updated
        /// </returns>
        T update(T o);
    }
}
