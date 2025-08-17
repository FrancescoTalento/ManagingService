using Entities;
using ServiceContracts.DTO.Person;
using ServiceContracts.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceContracts
{
    /// <summary>
    /// Represent business logic for manipulating the Person Entity
    /// </summary>
    public interface IPersonsService
    {
        /// <summary>
        /// Adds a Country object to the Table Country in the DB
        /// </summary>
        /// <param name="entity">Person object to be added</param>
        /// <returns>Returns the same person details, along with the newly generated PersonID</returns>
        PersonResponse AddEntity(PersonAddRequest? request);

        /// <summary>
        /// Returns all persons from DB
        /// </summary>
        /// <returns>Returns all persons from DB as a List of PersonResponse Type</returns>
        List<PersonResponse> GetAllEntites();


        /// <summary>
        /// Return the PersonResponse object based on the given person ID
        /// </summary>
        /// <param name="personId">Person id to be search</param>
        /// <returns>Returns matching person object</returns>
        PersonResponse? GetEntityById(Guid? personId);

        /// <summary>
        /// Retrieves all <see cref="PersonResponse"/> objects that match a filter
        /// applied to a specified property.
        /// </summary>
        /// <param name="propName">
        /// The name of the property (from <see cref="Person"/> / <see cref="PersonResponse"/>) 
        /// to filter by. Matching is case-insensitive.
        /// </param>
        /// <param name="filterValue">
        /// The value to compare against the property's value. The comparison logic depends on
        /// the property type:
        /// <list type="bullet">
        ///   <item><description><b>string</b>: case-insensitive substring match.</description></item>
        ///   <item><description><b>DateTime</b>: formatted as <c>dd MMM yyyy</c> and matched by substring.</description></item>
        ///   <item><description><b>bool</b>: supports true/false, 1/0, yes/no, sim/não.</description></item>
        ///   <item><description><b>Guid</b>: exact match if parsable; otherwise substring of string representation.</description></item>
        ///   <item><description><b>Numeric types</b>: compared by value (after parsing).</description></item>
        /// </list>
        /// </param>
        /// <returns>
        /// A list of <see cref="PersonResponse"/> instances whose property values
        /// match the specified criteria. If <paramref name="propName"/> or 
        /// <paramref name="filterValue"/> is null or empty, all persons will be returned.
        /// </returns>
        List<PersonResponse> GetFilteredEntity(string propName, string filterValue);


        /// <summary>
        /// Sorts the provided <see cref="PersonResponse"/> sequence by a key selected from each item,
        /// in the specified order, optionally placing null keys at the end.
        /// </summary>
        /// <typeparam name="TKey">
        /// The type of the key produced by <paramref name="keySelector"/>.
        /// </typeparam>
        /// <param name="entities">
        /// The list of <see cref="PersonResponse"/> to sort. The original list is not modified; a new list is returned.
        /// </param>
        /// <param name="keySelector">
        /// A function that selects the sort key from a <see cref="PersonResponse"/>.
        /// </param>
        /// <param name="sortOrder">
        /// The sort direction to apply (ascending or descending), as defined by <see cref="SortOrderOptions"/>.
        /// </param>
        /// <param name="nullLast">
        /// When <see langword="true"/> (default), items whose keys are <see langword="null"/> are ordered after non-null keys;
        /// when <see langword="false"/>, the default comparer ordering for nulls is used.
        /// </param>
        /// <returns>
        /// A new list of <see cref="PersonResponse"/> sorted according to the selected key and order.
        /// </returns>
        /// <remarks>
        /// Sorting is stable: items with equal keys preserve their relative order.
        /// </remarks>
        List<PersonResponse> GetSortedEntities<TKey>(
            List<PersonResponse> entities,
            Func<PersonResponse, TKey> keySelector,
            SortOrderOptions sortOrder,
            bool nullLast = true);

        /// <summary>
        /// Updates an existing person using the data provided in <paramref name="request"/> and
        /// returns the updated representation.
        /// </summary>
        /// <param name="request">
        /// The update payload containing the target person identifier and new values to persist.
        /// </param>
        /// <returns>
        /// A <see cref="PersonResponse"/> reflecting the state of the person after the update.
        /// </returns>
        /// <exception cref="ArgumentNullException">
        /// Thrown when <paramref name="request"/> is <see langword="null"/>.
        /// </exception>
        /// <exception cref="ArgumentException">
        /// Thrown when required fields are missing or the identifier is invalid.
        /// </exception>
        /// <exception cref="KeyNotFoundException">
        /// Thrown when the target person does not exist.
        /// </exception>
        PersonResponse UpdateEntity(PersonUpdateRequest? request);

        /// <summary>
        /// Deletes an existing person from the data store based on the specified unique identifier.
        /// </summary>
        /// <param name="personId">
        /// The unique identifier (<see cref="Guid"/>) of the person to be deleted.
        /// If <c>null</c> or <see cref="Guid.Empty"/>, no action is taken.
        /// </param>
        /// <returns>
        /// <c>true</c> if a person with the specified <paramref name="personId"/> was found and deleted;
        /// otherwise, <c>false</c>.
        /// </returns>
        bool DeleteEntity(Guid? personId);


    }
}
